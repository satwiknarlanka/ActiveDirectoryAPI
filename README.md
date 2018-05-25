# ActiveDirectoryAPI

## Overview

Active directory can be overwhelming for many developers. Sometimes all we need is to read user information from active directory or get members from an AD group. This project provides a REST api for that.

## Prerequisites

This project assumes you have a dotnet core SDK installed on your system. This project has a **Microsoft.Windows.Compatibility** nuget package to enable usage of  *System.DirectoryServices.AccountManagement* library. Currently this project only works on windows.

## Usage

    dotnet build

    dotnet run --project ADWrapper/
