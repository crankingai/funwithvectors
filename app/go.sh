#!/bin/bash

npm run build 
npm run lint
tsc --noEmit

# swa start http://localhost:5173 --api http://localhost:7071

