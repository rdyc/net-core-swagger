# XComm #15 - Documenting REST APIs
With Swagger (ASP Net Core)

## Section I : The Background

### Why documentation matters
A [survey by ProgrammableWeb] (https://www.programmableweb.com/news/api-consumers-want-reliability-documentation-and-community/2013/01/07) found that API consumers consider complete and accurate documentation as the biggest factor in their API decision making, even outweighing price and API performance.

Good documentation accelerates development and consumption, and reduces the money and time that would otherwise be spent answering support calls. Documentation is part of the overall user experience, and is one of the biggest factors for increased API growth and usage.

### Why use OpenAPI for API documentation
The Swagger Specification, which was renamed to the OpenAPI Specification (OAS), after the Swagger team joined SmartBear and the specification was donated to the OpenAPI Initiative in 2015, has become the de factor standard for defining RESTful APIs.

(Note: We will be using the term OpenAPI and OAS throughout this resource. This is meant to reference the Specification.)

OAS defines an API’s contract, allowing all the API’s stakeholders, be it your development team, or your end consumers, to understand what the API does and interact with its various resources, without having to integrate it into their own application. This contract is language agnostic and human readable, allowing both machines and humans to parse and understand what the API is supposed to do.

### How does OAS help with documentation?

* Help internal team members understand the API and agree on its attributes
* Help external folks understand the API and what it can do
* Create tests for your API
* Generate implementation code and SDKs

### Documenting the API from the OAS Definition
Documentation from the generated contact would mean adding meaningful, understandable information that your end consumers can use to achieve API success. OAS lets you describe important details, including:

* Media types
* Endpoints/Resources
* Request bodies
* Responses
* Examples
* Authentication


## Section II : OAS Implementation

### Common OAS samples
Most common standart OAS samples can be found on [Swagger Petstore] (https://petstore.swagger.io)

### Requirement
1. [.Net Core SDK 2.2] (https://dotnet.microsoft.com/download/dotnet-core/2.2)
2. Visual Studio Code
3. A new or exsisting Web API project

### Using OAS packages
There are two most popular nuget packages that can help us to build OAS Documentation:

1. [Swashbuckle] (https://github.com/domaindrivendev/Swashbuckle) by **Richard Moris** (domaindrivendev)
2. [NSwag] (https://github.com/RicoSuter/NSwag) by **Rico Sutter** (RicoSuter)

At this time, we will use Swashbuckle packages
```
$ Install-Package Swashbuckle.Core
```

### Startup Configuration

### Configure Media Types

### Writing Endpoints/Resources

### Define Request Bodies

### Define Responses

### Define Examples

### Integrate Authentication

### Release






