using Klororf.AzSearchLinq.Operators;
using test.Models;

namespace test;

public class LogicalOperatorsTest : BaseOperatorsTest
{
    public LogicalOperatorsTest() : base()
    {

    }

    [Fact]
    public void TestAnd()
    {
        var filter = _searchClient.Where<Product>(x => x.Description == "test" && x.Price > 20).Filter;
        Assert.True(filter == "Description eq 'test' and Price gt 20");
    }

    [Fact]
    public void TestOr()
    {
        var filter = _searchClient.Where<Product>(x => x.Description == "test" || x.Price > 20).Filter;
        Assert.True(filter == "Description eq 'test' or Price gt 20");
    }

    [Fact]
    public void TestAndOr()
    {
        var filter = _searchClient.Where<Product>(x => x.Description == "test" && x.Size=="small" || x.Price > 20).Filter;
        Assert.True(filter == "(Description eq 'test' and Size eq 'small') or Price gt 20");
    }
}
