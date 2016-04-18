﻿# MyApp-sandbox 

Solution of cross-platform .NET apps, built with Xamarin, that share common business logic, using the MVVM and service patterns.

## Purpose

This project serves as a sandbox in which to explore different native app platforms and their respective API:s.

### Overview

#### Mobile apps

This solution contains native apps for:

* Android (Xamarin.Android)
* iOS (Xamarin.iOS)
* Universal Windows Platform (UWP)
* Windows Presentation Foundation (WPF)

These apps share a common business layer with shared view models and services.

#### Web app

The solution also contains an ASP.NET Core Web app as a back-end. This web app exposes a Web API that gets and saves data from/to a database.

## Requirements

To build this solution you will need the programs:

* Visual Studio 2015 (or later)
* Xamarin Tools for Visual Studio (can be installed with VS installer)
* The ASP.NET Command-line tools (DNX) (Download from http://get.asp.net/)
* SQL Server (LocalDB is included with VS 2015)

You will need a Mac to build for iOS.

Note that Xamarin is now free of charge after Microsoft acquired Xamarin.

## Running the Web app

The apps communicate with an ASP.NET Core Web API.

This app can run on Windows, Mac OS X, and Linux. 
On Linux and Mac OS X, you might, for the time being, have to switch to another supported database server, like PostgreSQL. This is because SQL Server for Linux has not been made public yet.

When in the directory "src/MyApp.Web", open a Command Prompt/Powershell/Terminal.

Type this to restore the project depencencies:

```sh
	dnu restore
```

Type this to start the app:

```sh
	dnx web --ASPNET_ENV=Development --server.urls http://192.168.1.100:5000
```

You can change the URL to one that is appropriate for you. This will require change in ViewModelLocator in MyApp.Shared.
Make sure that the apps are on the same network as the server, and that the server port is forwarded in you firewall. Usually you get prompted first time you run by the Windows Firewall.

You are now good to go!