using Azure.Search.Documents;
using Klororf.AzSearchLinq.Test.Utils;

namespace Klororf.AzSearchLinq.Test.Operators;

public abstract class BaseOperatorsTest
{
    protected readonly SearchClient _searchClient;
    public BaseOperatorsTest()
    {
        this._searchClient = MockUtils.MoqSearchClient();
    }
}
