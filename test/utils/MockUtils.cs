using Azure.Search.Documents;
using Moq;

namespace test;

public static class MockUtils
{
    public static SearchClient MoqSearchClient(){
        Mock<SearchClient> mock = new Mock<SearchClient>();
        return mock.Object;
    }
}
