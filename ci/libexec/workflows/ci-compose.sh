#!/usr/bin/env bash
# shellcheck disable=SC2155

###
# Compose the project's artifacts, e.g., compiled binaries, Docker images.
#   This workflow performs all steps required to create the project's output.
#
#   This script expects a CICEE CI library environment (which is provided when using 'cicee lib exec').
#   For CI library environment details, see: https://github.com/JeremiahSanders/cicee/blob/main/docs/use/ci-library.md
#
# How to use:
#   Modify the "ci-compose" function, below, to execute the steps required to produce the project's artifacts. 
###

set -o errexit  # Fail or exit immediately if there is an error.
set -o nounset  # Fail if an unset variable is used.
set -o pipefail # Fail pipelines if any command errors, not just the last one.

function ci-compose() {
  printf "Composing build artifacts...\n\n"

  function generateDocs(){
    local srcDir="${1}"
    local dllName="${2}"
    local namespace="${3}"
    local framework="${4}"
    local gitSrcRootLink="${5}"

    local tempDir="${BUILD_DOCS}/xml-doc-temp"
    local sourcePath="${tempDir}/${dllName}"
    local outputPath="${BUILD_DOCS}/md"

    cd "${PROJECT_ROOT}" &&
      dotnet tool restore &&
      mkdir -p "${tempDir}" &&
      dotnet publish "${srcDir}" \
        --configuration Release \
        --output "${tempDir}" \
        --framework "${framework}" \
        -p:Version="${PROJECT_VERSION_DIST}" \
        -p:GenerateDocumentationFile=true &&
      dotnet xmldocmd "${sourcePath}" "${outputPath}" \
        --namespace "${namespace}" \
        --source "${gitSrcRootLink}" \
        --newline lf \
        --visibility protected &&
      rm -rf "${tempDir}"
  }
  
  function createDocs() {
    # Use of .net6 needed due to xmldocmd only supporting through net6
    generateDocs "src" "LanguageExt.Extras.dll" "Jds.LanguageExt.Extras" "net6.0" "https://github.com/JeremiahSanders/languageext-extras/tree/main/src"
  }

  ci-dotnet-publish \
    --framework "net10.0" \
    -p:GenerateDocumentationFile=true &&
    ci-dotnet-publish \
        --framework "net8.0" \
        -p:GenerateDocumentationFile=true &&
    ci-dotnet-publish \
        --framework "net6.0" \
        -p:GenerateDocumentationFile=true &&
    ci-dotnet-pack \
      -p:GenerateDocumentationFile=true &&
    createDocs &&
    printf "Composition complete.\n"
}

export -f ci-compose
