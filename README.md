# Authorization Server: OpenIddict
Authorization Server implemented with [OpenIddict 3](https://github.com/openiddict/openiddict-core).

This repository contains an authorization server that is being built on top of examples from a blog series from Robin van der Knaap: [Setting up an authorization server with OpenIddict](https://dev.to/robinvanderknaap/setting-up-an-authorization-server-with-openiddict-part-i-introduction-4jid).

## Prerequisites
- [.NET 5](https://dotnet.microsoft.com/)

## Getting started
Open up a terminal in the root folder

    dotnet run --project AuthorizationServer

Visit [https://localhost:5001](https://localhost:5001). In case of certificate errors run the following command:

    dotnet dev-certs https --trust
