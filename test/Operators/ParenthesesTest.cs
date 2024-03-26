using Klororf.AzSearchLinq.Operators;
using test.Models;

namespace test;

public class ParenthesesTest: BaseOperatorsTest
{
    public ParenthesesTest(): base(){

    }

    [Fact]
    public void SimpleParentheses(){
        var filter = _searchClient.Where<Product>(x=>x.Description == "test" && (x.Size=="small" || x.Size == "medium")).Filter;
        Assert.True(filter == "Description eq 'test' and ( Size eq 'small' or Size eq 'medium')");
    }
    [Fact]
    public void ComplexParentheses(){
        var filter = _searchClient.Where<Product>(x=>x.Description == "test" && (x.Size=="small" || (x.Size == "medium" && x.Price>20))).Filter;
        Assert.True(filter == "Description eq 'test' and ( Size eq 'small' or ( Size eq 'medium' and Price gt 20))");
    }
}
