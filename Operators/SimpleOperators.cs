using System.Linq.Expressions;
using Azure.Search.Documents;
using Klororf.AzSearchLinq.Utils;
using System.Linq.Expressions;
using Azure.Search.Documents.Models;
using Klororf.AzSearchLinq.Models;
namespace Klororf.AzSearchLinq.Operators;

public static class SimpleOperators
{

    public static ExpressionFilter<T> Where<T>(this SearchClient searchClient, Expression<Func<T, bool>> predicate)
    {
        ExpressionType expressionType = predicate.Body.NodeType;
        if (expressionType == ExpressionType.Equal || expressionType == ExpressionType.NotEqual)
        {
            BinaryExpression binaryExpression = (BinaryExpression)predicate.Body;
            return searchClient.WhereBinary<T>(binaryExpression, expressionType);
        }
        return null;
    }
    public static ExpressionFilter<T> Where<T>(this ExpressionFilter<T> searchClient, Expression<Func<T, bool>> predicate)
    {
        ExpressionType expressionType = predicate.Body.NodeType;
        if (expressionType == ExpressionType.Equal || expressionType == ExpressionType.NotEqual)
        {
            BinaryExpression binaryExpression = (BinaryExpression)predicate.Body;
            return searchClient.WhereBinary<T>(binaryExpression, expressionType);
        }
        return null;
    }

    private static ExpressionFilter<T> WhereBinary<T>(this SearchClient searchClient, BinaryExpression binaryExpression, ExpressionType expressionType)
    {
        MemberExpression left = (MemberExpression)binaryExpression.Left;
        ConstantExpression right = (ConstantExpression)binaryExpression.Right;
        return new ExpressionFilter<T>()
        {
            SearchClient = searchClient,
            Filter = string.Format("{0} {1} '{2}'", left.Member.Name, OdataOperatorsUtils.OdataOperator[expressionType], right.Value)
        };
    }
    private static ExpressionFilter<T> WhereBinary<T>(this ExpressionFilter<T> expressionFilter, BinaryExpression binaryExpression, ExpressionType expressionType)
    {
        MemberExpression left = (MemberExpression)binaryExpression.Left;
        ConstantExpression right = (ConstantExpression)binaryExpression.Right;
        var filter = string.Format("{0} {1} '{2}'", left.Member.Name, OdataOperatorsUtils.OdataOperator[expressionType], right.Value);
        return new ExpressionFilter<T>()
        {
            SearchClient = expressionFilter.SearchClient,
            Filter = string.Format("{0} {1} {2}", expressionFilter.Filter, "and", filter)
        };
    }
}
