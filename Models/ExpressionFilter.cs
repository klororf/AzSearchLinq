using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace Klororf.AzSearchLinq.Models;
public class ExpressionFilter<T>
{
    public string Filter { get; set; }
    public SearchClient SearchClient { get; set; }

    public SearchOptions ToSearchOptions()
    {
        return new()
        {
            Filter = Filter
        };
    }
    public SearchResults<T> Search(string searchText = "*")
    {
        return this.SearchClient.Search<T>(searchText, new()
        {
            Filter = Filter
        });
    }

    public ExpressionFilter<T> SetParenthereses(bool set)
    {
        if(set)
            return new ExpressionFilter<T>()
            {
                SearchClient=this.SearchClient,Filter = string.Format("({0})", this.Filter)};
        else
            return this;
        
    }

}