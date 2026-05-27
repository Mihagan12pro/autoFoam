#!/bin/bash

./Clean.sh
./ChangeParams.sh
blockMesh
pimpleFoam
