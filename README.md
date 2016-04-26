# scorelines-app-test-helpers

Collection of projects containing test helpers for the scorelines-app-* collection of repositories.

## Prerequisites

After cloning this repository:

- Install [NodeJS](http://nodejs.org/).

- Install local npm packages.

  ```
  npm install --no-optional
  ```

## Tasks

Use Gulp to run any of the following tasks:

- `gulp build` - Compile, test & create NuGet package.
- `gulp publish --bump [<newversion> | major | minor | patch | premajor | preminor | prepatch | prerelease]` - bump the version number, run the build task and publish the NuGet package. See [npm version](https://docs.npmjs.com/cli/version) for details on <newversion> argument.
