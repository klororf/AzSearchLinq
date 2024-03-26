using System.Linq.Expressions;
using Azure.Search.Documents;
using Klororf.AzSearchLinq.Utils;
using System.Linq.Expressions;
using Azure.Search.Documents.Models;
using Klororf.AzSearchLinq.Models;
using Klororf.AzSearchLinq.Extensions;
namespace Klororf.AzSearchLinq.Operators;

public static class SimpleOperators
{

    public static ExpressionFilter<T> Where<T>(this SearchClient searchClient, Expression<Func<T, bool>> predicate)
    {
        ExpressionType expressionType = predicate.Body.NodeType;
        if (expressionType.IsComparingOperator())
        {
            BinaryExpression binaryExpression = (BinaryExpression)predicate.Body;
            return searchClient.WhereBinary<T>(binaryExpression, expressionType);
        }
        else if (expressionType.IsLogicalOperator())
        {
            BinaryExpression binaryExpression = (BinaryExpression)predicate.Body;
            return searchClient.WhereSimpleBinary<T>(binaryExpression, expressionType);
        }
        return null;
    }
    public static ExpressionFilter<T> Where<T>(this ExpressionFilter<T> searchClient, Expression<Func<T, bool>> predicate)
    {
        ExpressionType expressionType = predicate.Body.NodeType;
        if (expressionType.IsComparingOperator())
        {
            BinaryExpression binaryExpression = (BinaryExpression)predicate.Body;
            return searchClient.WhereBinary<T>(binaryExpression, expressionType);
        }
        return null;
    }


}
