# Cloud Accelerator
ASP.NET MVC Web Application that can be cloud hosted and act as a Reverse Proxy Cache.

## Features
* A very simple application that fetches, caches and serves web content from the remote domain names you allow.
* Enforces optimal compression if the client supports it and doesn't skip compressing under load as IIS does by default.
* Cookie is stripped so Response is cookie free.
* Deploy it as an Azure Web Site and scale to what is needed.

## Setup
Grab the source code and Run the app, browse the root and look at the example URLs. Look for the TODO-comments and adjust the following to fit your setup:

1. In \App_Start\RouteConfig.cs - Add patterns and point to wanted proxy controller
2. In \Controllers\ProxyController.cs - Add the domain names you allow to fetch from and adjust duration and parameters
3. Possibly edit \Views\Proxy\Index.cshtml
