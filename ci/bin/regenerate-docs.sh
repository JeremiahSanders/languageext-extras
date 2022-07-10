#!/usr/bin/env bash
# shellcheck disable=SC2155

###
# Build and validate the project's source.
#
# How to use:
#   Customize the "ci-validate" and "ci-compose" workflows (functions) defined in ci/libexec/workflows/.
###

set -o errexit  # Fail or exit immediately if there is an error.
set -o nounset  # Fail if an unset variable is used.
set -o pipefail # Fail pipelines if any command errors, not just the last one.

declare SCRIPT_LOCATION="$(dirname "${BASH_SOURCE[0]}")"
declare PROJECT_ROOT="${PROJECT_ROOT:-$(cd "${SCRIPT_LOCATION}/../.." && pwd)}"

if [[ -z "$(command -v cicee)" ]]; then
  # Install CICEE, to add the CI shell library.
  dotnet tool install -g cicee
else
  # Ensure we're using the latest CI shell library scripts.
  dotnet tool update -g cicee
fi

#--
# Use 'cicee lib exec' to run our validation workflow and perform a dry-run output composition.
#   All .sh scripts in ci/libexec/workflows/ are sourced by CICEE's library.
#   Below we only need to execute the workflow Bash shell functions.
#--
cicee lib exec --project-root "${PROJECT_ROOT}" --command "regenerate-docs"

__initialize &&
  printf "Beginning document regeneration...\n\n" &&
  regenerate-docs &&
  printf "Document regeneration complete.\n\n"
