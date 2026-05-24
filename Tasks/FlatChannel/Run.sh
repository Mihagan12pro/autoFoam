#!/bin/bash
source /opt/openfoam13/etc/bashrc
./Clean.sh
./ChangeParams.sh
blockMesh
pimpleFoam
paraFoam
