# Cloud Accelerator
ASP.NET MVC Web Application that can be cloud hosted and act as a Reverse Proxy Cache.

## Benefits
* Fetches, caches and serves web content from the domain names you allow.
* Enforces compression if client supports it and doesn't skip compressing under load as IIS does by default.
* Deploy it as an Azure Web App and scale to what is needed.

## Setup
Grab the source code and look for the TODO-comments.
1. In \App_Start\RouteConfig.cs - Add patterns and point to wanted proxy controller
2. In \Controllers\ProxyController.cs - Add the domain names you allow to fetch from
3. Edit \Views\Proxy\Index.cshtml