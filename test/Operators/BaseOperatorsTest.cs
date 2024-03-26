using Azure.Search.Documents;

namespace test;

public abstract class BaseOperatorsTest
{
    protected readonly SearchClient _searchClient;
    public BaseOperatorsTest()
    {
        this._searchClient = MockUtils.MoqSearchClient();
    }
}
