# AzSearchLinq

AzSearchLinq is a .Net package for query azure search with LINQ sintax translating the sintax to odata language

## Usage

```csharp
SearchClient srchclient = new SearchClient(serviceEndpoint, indexName, credential);
var res = srchclient.Where<Product>(x =>x.ProductName != "Mr").Search();

```
