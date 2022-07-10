#!/usr/bin/env bash
# shellcheck disable=SC2155

###
# Publish the project's artifact composition.
#   This workflow assumes that, prior to execution, an artifact composition is created. E.g., ci-compose was executed.
#
#   This script expects a CICEE CI library environment (which is provided when using 'cicee lib exec').
#   For CI library environment details, see: https://github.com/JeremiahSanders/cicee/blob/main/docs/use/ci-library.md
#
# How to use:
#   Modify the "ci-publish" function, below, to execute the steps required to publish the project's artifacts.
###

set -o errexit  # Fail or exit immediately if there is an error.
set -o nounset  # Fail if an unset variable is used.
set -o pipefail # Fail pipelines if any command errors, not just the last one.

function ci-publish() {
  printf "Publishing composed artifacts...\n\n"

  ci-dotnet-nuget-push

  printf "Publishing complete.\n\n"
}

export -f ci-publish
