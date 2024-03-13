using System.Linq.Expressions;
using Azure.Search.Documents;
using Klororf.AzSearchLinq.Utils;
using System.Linq.Expressions;
using Azure.Search.Documents.Models;
using Klororf.AzSearchLinq.Models;
using Klororf.AzSearchLinq.Extensions;
namespace Klororf.AzSearchLinq.Operators;

public static class SimpleOperatorsInternal
{
    internal static ExpressionFilter<T> WhereBinary<T>(this SearchClient searchClient, BinaryExpression binaryExpression, ExpressionType expressionType)
    {
        MemberExpression left = (MemberExpression)binaryExpression.Left;
        ConstantExpression right = (ConstantExpression)binaryExpression.Right;
        return new ExpressionFilter<T>()
        {
            SearchClient = searchClient,
            Filter = string.Format("{0} {1} {2}", left.Member.Name, OdataOperatorsUtils.OdataOperator[expressionType], GetObjectFormated(right))
        };
    }
    internal static ExpressionFilter<T> WhereBinary<T>(this ExpressionFilter<T> expressionFilter, BinaryExpression binaryExpression, ExpressionType expressionType)
    {
        MemberExpression left = (MemberExpression)binaryExpression.Left;
        ConstantExpression right = (ConstantExpression)binaryExpression.Right;
        var filter = string.Format("{0} {1} {2}", left.Member.Name, OdataOperatorsUtils.OdataOperator[expressionType], GetObjectFormated(right));
        return new ExpressionFilter<T>()
        {
            SearchClient = expressionFilter.SearchClient,
            Filter = string.Format("{0} {1} {2}", expressionFilter.Filter, "and", filter)
        };
    }

    internal static string? GetObjectFormated(ConstantExpression constantExpression)
    {
        if (constantExpression.Type == typeof(string))
        {
            return string.Format("'{0}'", constantExpression.Value);
        }
        else
        {
            return constantExpression.Value?.ToString();
        }
    }
}