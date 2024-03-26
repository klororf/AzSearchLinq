using System.Linq.Expressions;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Klororf.AzSearchLinq.Utils;

namespace Klororf.AzSearchLinq.Models;
public class ExpressionFilter<T>
{
    public string Filter { get; set; }
    public SearchClient SearchClient { get; set; }

    public ExpressionFilter<T> Concat(ExpressionFilter<T> expressionFilter)
    {
        this.Filter = string.Format("{0} {1}", this.Filter, expressionFilter.Filter);
        return this;
    }
      public ExpressionFilter<T> ConcatWithOperator(ExpressionFilter<T> expressionFilter, ExpressionType expressionType)
    {
        this.Filter = string.Format("{0} {1} {2}", this.Filter, OdataOperatorsUtils.OdataOperator[expressionType] ,expressionFilter.Filter);
        return this;
    }
    public void AddParentheses()
    {
        this.Filter = string.Format("({0})", this.Filter);
    }
    public ExpressionFilter<T> FromEmpty()
    {
        return new ExpressionFilter<T>()
        {
            SearchClient = this.SearchClient
        };
    }
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
        if (set)
            return new ExpressionFilter<T>()
            {
                SearchClient = this.SearchClient,
                Filter = string.Format("({0})", this.Filter)
            };
        else
            return this;

    }

}