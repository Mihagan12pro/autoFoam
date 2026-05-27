#!/bin/bash
cd "$(dirname "$0")"
echo "$(dirname "$0")"
./Clean.sh
./ChangeParams.sh
blockMesh
pimpleFoam
