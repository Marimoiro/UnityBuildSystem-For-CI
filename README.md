﻿# UnityBuildSystem-For-CI

The project is simple Unity Build System for CI.

# Usage

##QuickStart

1. Open Inspector of [Assets/Editor/BuildSystemBuildParameter/Sample.asset](https://github.com/Marimoiro/UnityBuildSystem-For-CI/blob/master/Assets/Editor/BuildSystem/BuildParameters/Sample.asset)
2. Click with this parameter
3. you can find builded artifacts in Build directory

## Unity BatchMode

``` 

-projectPath "${WORKSPACE}" -batchmode -quit -buildTarget ${BuildTarget} -executeMethod BuildSystem.BatchMode.Build -parameter ${ParameteName} -logFile "${WORKSPACE}/editor.log"

```

### For sample 

Android

``` 

-projectPath "${WORKSPACE}" -batchmode -quit -buildTarget android -executeMethod BuildSystem.BatchMode.Build -parameter Sample -logFile "${WORKSPACE}/editor.log"

```

iOS

``` 

-projectPath "${WORKSPACE}" -batchmode -quit -buildTarget ios -executeMethod BuildSystem.BatchMode.Build -parameter Sample -logFile "${WORKSPACE}/editor.log"

```

Win64

``` 

-projectPath "${WORKSPACE}" -batchmode -quit -buildTarget win64 -executeMethod BuildSystem.BatchMode.Build -parameter Sample -logFile "${WORKSPACE}/editor.log"

```
