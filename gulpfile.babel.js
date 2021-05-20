import gulp from 'gulp';
import del from 'del';
import fs from 'fs';
import log from 'gulplog';
import mkdirp from 'mkdirp';
import msbuild from 'npm-msbuild';
import nuget from 'npm-nuget';
import shell from 'shelljs';
import yargs from 'yargs';

// Configuration
const config = {
    directories: {
        artifacts: './artifacts'
    },
    files: {
        scorelines_test_helpers: {
            package: './artifacts/Scorelines.TestHelpers.{version}.nupkg',
            project: './source/Scorelines.TestHelpers/Scorelines.TestHelpers.csproj'
        },
        scorelines_test_helpers_selenium: {
            package: './artifacts/Scorelines.TestHelpers.Selenium.{version}.nupkg',
            project: './source/Scorelines.TestHelpers.Selenium/Scorelines.TestHelpers.Selenium.csproj'
        }
    },
    msbuild: {
        configuration: 'Release'
    },
    nuget: {
        source: 'nuget_timmurphy_it'
    }
};

// Tasks
export const build = gulp.series(clean, compileSolution, createNuGetPackages);
export const publish = gulp.series(bumpVersion, build, pushGitRepository, pushNuGetPackages);

// Private methods

// Uses 'npm version <newversion>' to increase the package version number. See https://docs.npmjs.com/cli/version
function bumpVersion(cb) {
    const bump = yargs.argv.bump;
    const validBump = '--bump can be major | minor | patch | premajor | preminor | prepatch | prerelease'

    if (!bump) {
        throw `--bump is required. ${validBump}`;
    }
    if (bump !== 'major' && bump !== 'minor' && bump !== 'patch' && bump !== 'premajor' && bump !== 'preminor' && bump !== 'prepatch' && bump !== 'prerelease') {
        throw `--bump cannot be '${bump}'. ${validBump}`
    }

    runShellCommand(`npm version ${bump}`);
    cb();
}

// Delete and then create ./artifacts directory
function clean(cb) {
    const path = config.directories.artifacts

    log.info(`Deleting '${path}'`);
    del.sync(path);

    log.info(`Creating '${path}'`);
    mkdirp.sync(path);

    cb();
}

// Compile Scorelines.TestHelpers.sln
function compileSolution(cb) {
    msbuild.exec(`/Target:Clean;ReBuild /Property:Configuration=${config.msbuild.configuration} /Verbosity:Minimal /NoLogo`);
    cb();
}

// Create nuget packages and write to ./artifacts directory
function createNuGetPackages(cb) {
    createNuGetPackage(config.files.scorelines_test_helpers.project)
    createNuGetPackage(config.files.scorelines_test_helpers_selenium.project);
    cb();
}

function createNuGetPackage(project) {
    nuget.exec(`pack "${project}" -Properties Configuration=${config.msbuild.configuration} -Exclude FodyWeavers.xml -Version ${getPackageVersion()} -OutputDirectory "${config.directories.artifacts}"`);
}

// Get version number from package.json
function getPackageVersion() {
    return JSON.parse(fs.readFileSync('./package.json')).version;
}

function pushGitRepository(cb) {
    runShellCommand('git push --follow-tags');
    cb();
}

export function pushNuGetPackages(cb) {
    pushNuGetPackage(config.files.scorelines_test_helpers.package);
    pushNuGetPackage(config.files.scorelines_test_helpers_selenium.package);
    cb();
}

function pushNuGetPackage(packagePathTemplate) {
    const packagePath = packagePathTemplate.replace('{version}', getPackageVersion());

    nuget.exec(`push ${packagePath} -Source ${config.nuget.source}`);
}

function runShellCommand(cmd) {
    var result = shell.exec(cmd).code;

    if (result === 0) {
        return;
    }
    throw `Shell command '${cmd}' returned error code '${result}'.`;
}