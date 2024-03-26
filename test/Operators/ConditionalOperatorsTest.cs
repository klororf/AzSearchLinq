using Azure.Search.Documents;
using Klororf.AzSearchLinq.Operators;
using test.Models;

namespace test;

public class ConditionalOperatorsTest: BaseOperatorsTest
{
    public ConditionalOperatorsTest(): base()
    {
    }

    [Fact]
    public void TestGreater(){
        var filter = _searchClient.Where<Product>(x=>x.Price > 20).Filter;
        Assert.True(filter == "Price gt 20");
    }

    [Fact]
    public void TestGreaterEquals(){
        var filter = _searchClient.Where<Product>(x=>x.Price >= 20).Filter;
        Assert.True(filter == "Price ge 20");
    }
    
    [Fact]
    public void TestLower(){
        var filter = _searchClient.Where<Product>(x=>x.Price < 20).Filter;
        Assert.True(filter == "Price lt 20");
    }

    [Fact]
    public void TestLowerEquals(){
        var filter = _searchClient.Where<Product>(x=>x.Price <= 20).Filter;
        Assert.True(filter == "Price le 20");
    }

    [Fact]
    public void TestEquals(){
        var filter = _searchClient.Where<Product>(x=>x.Price == 20).Filter;
        Assert.True(filter == "Price eq 20");
    }

    [Fact]
    public void TestNotEquals(){
        var filter = _searchClient.Where<Product>(x=>x.Price != 20).Filter;
        Assert.True(filter == "Price ne 20");
    }
}
