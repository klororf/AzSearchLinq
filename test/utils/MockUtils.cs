using Azure.Search.Documents;
using Moq;

namespace Klororf.AzSearchLinq.Test.Utils;
public static class MockUtils
{
    public static SearchClient MoqSearchClient(){
        Mock<SearchClient> mock = new Mock<SearchClient>();
        return mock.Object;
    }
}
