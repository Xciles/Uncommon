Xciles.Uncommon
===============
This is still a work in progress, dll's etc will be made available soon :)

The project is a Portable Class Library (profile 158). This means that it is compatible and can be used with Xamarin (and mono) based projects.

Currently the project contains:
* RestRequest implementation with various options and features!
* A RestRequestHelper, which is a wrapper arround the RestRequest to make your life even better!
* Retry logic, inspired by [LBushkin] (http://stackoverflow.com/a/1563234/1434515)

The VS solution contains 2 local WebBased test projects which will (and can) be used to test the functionality, as well as give a basic idea whats possible. And a UnitTest project for UnitTests using [Fakes and Shims] (http://msdn.microsoft.com/en-us/library/hh549175.aspx).

Documentation and Examples
==========================
#### RestRequestHelper Basic usage
``` C#
var resultObject = await RestRequestHelper.ProcessGetRequest<MyClass>("http://www.example.com/api/mycall");
```

More to come :)

Licensing
=========
This project is developed and distributed under MIT License



