#!/usr/bin/env bash
# shellcheck disable=SC2155

###
# Build and publish the project's artifact composition.
#
# How to use:
#   Customize the "ci-compose" and "ci-publish" workflows (functions) defined in ci/libexec/workflows/.
###

set -o errexit  # Fail or exit immediately if there is an error.
set -o nounset  # Fail if an unset variable is used.
set -o pipefail # Fail pipelines if any command errors, not just the last one.

declare SCRIPT_LOCATION="$(dirname "${BASH_SOURCE[0]}")"
declare PROJECT_ROOT="${PROJECT_ROOT:-$(cd "${SCRIPT_LOCATION}/../.." && pwd)}"

if [[ -z "$(command -v cicee)" ]]; then
  # Install CICEE, to add the CI shell library.
  dotnet tool install -g cicee || echo -e "\nFailed to install CICEE.\n  Unexpected errors may occur.\n\n"
else
  # Ensure we're using the latest CI shell library scripts.
  dotnet tool update -g cicee || echo -e "\nFailed to update CICEE.\n  Unexpected errors may occur.\n  Current CICEE version: $(cicee --version)\n\n"
fi

#--
# Use 'cicee lib exec' to run our output composition and publish workflows.
#   All .sh scripts in ci/libexec/workflows/ are sourced by CICEE's library.
#   Below we only need to execute the workflow Bash shell functions.
#--
cicee lib exec --project-root "${PROJECT_ROOT}" --command "ci-compose \&\& ci-publish"
