#!/usr/bin/env bash
# shellcheck disable=SC2155

###
# Validate the project's source, e.g. run tests, linting.
#
#   This script expects a CICEE CI library environment (which is provided when using 'cicee lib exec').
#   For CI library environment details, see: https://github.com/JeremiahSanders/cicee/blob/main/docs/use/ci-library.md
#
# How to use:
#   Modify the "ci-validate" function, below, to execute the steps required to produce the project's artifacts.
###

set -o errexit  # Fail or exit immediately if there is an error.
set -o nounset  # Fail if an unset variable is used.
set -o pipefail # Fail pipelines if any command errors, not just the last one.

function ci-validate() {
  printf "Beginning validation...\n\n"

  # First build performs general linting. Second build, ci-dotnet-build, builds the solution.
  # Code style validate on build reference: https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview#enable-on-build
  # CS1591 is only generated when documentation file is output. See: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs1591
  ci-dotnet-restore &&
    dotnet build "${PROJECT_ROOT}/src" \
      -p:EnforceCodeStyleInBuild=true \
      -p:GenerateDocumentationFile=true \
      -p:TreatWarningsAsErrors=true &&
    ci-dotnet-build &&
    ci-dotnet-test &&
    printf "Validation complete!\n\n"
}

export -f ci-validate
