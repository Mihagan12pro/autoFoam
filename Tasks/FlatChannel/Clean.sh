#!/bin/bash
cd "$(dirname "$0")"
echo "$(dirname "$0")"
find . -maxdepth 1 -type d -regextype posix-extended -regex './[0-9]+(\.[0-9]+)?' ! -name "0" -exec rm -rf {} \;

echo "Cleanup done. Folder 0 preserved."
