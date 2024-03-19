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

    internal static ExpressionFilter<T> WhereSimpleBinary<T>(this SearchClient searchClient, BinaryExpression binaryExpression, ExpressionType expressionType,bool rightHasParentheses = false)
    {
        if (binaryExpression.NodeType.IsLogicalOperator())
        {
            
            BinaryExpression binaryExpressionLeft = (BinaryExpression)binaryExpression.Left;
            BinaryExpression binaryExpressionRight = (BinaryExpression)binaryExpression.Right;
            ExpressionFilter<T> expressionFilterLeft = searchClient.WhereSimpleBinary<T>(binaryExpressionLeft, binaryExpressionLeft.NodeType);
            ExpressionFilter<T> expressionFull = expressionFilterLeft.WhereSimpleBinary<T>(binaryExpressionRight, binaryExpressionRight.NodeType);
            return expressionFull;
        }
        else
        {
            return searchClient.WhereBinary<T>(binaryExpression, binaryExpression.NodeType);
        }
    }

    internal static ExpressionFilter<T> WhereSimpleBinary<T>(this ExpressionFilter<T> expressionFilter, BinaryExpression binaryExpression, ExpressionType expressionType)
    {
        if (binaryExpression.NodeType.IsLogicalOperator())
        {
            BinaryExpression binaryExpressionLeft = (BinaryExpression)binaryExpression.Left;
            BinaryExpression binaryExpressionRight = (BinaryExpression)binaryExpression.Right;
            ExpressionFilter<T> expressionFilterLeft = expressionFilter.WhereSimpleBinary<T>(binaryExpressionLeft, binaryExpressionLeft.NodeType);
            ExpressionFilter<T> expressionFull = expressionFilterLeft.WhereSimpleBinary<T>(binaryExpressionRight, binaryExpressionRight.NodeType);
            return expressionFull;
        }
        else
        {
            return expressionFilter.WhereBinary<T>(binaryExpression, binaryExpression.NodeType);
        }
        // BinaryExpression left = (BinaryExpression)binaryExpression.Left;
        // ConstantExpression right = (ConstantExpression)binaryExpression.Right;
        // return new ExpressionFilter<T>()
        // {
        //     SearchClient = searchClient,
        //     Filter = string.Format("{0} {1} {2}", left.Member.Name, OdataOperatorsUtils.OdataOperator[expressionType], GetObjectFormated(right))
        // };
    }
    internal static ExpressionFilter<T> WhereBinary<T>(this ExpressionFilter<T> expressionFilter, BinaryExpression binaryExpression, ExpressionType expressionType)
    {
        string formatBase = "{0} {1} {2}";
    
        MemberExpression left = (MemberExpression)binaryExpression.Left;
        ConstantExpression right = (ConstantExpression)binaryExpression.Right;
        var filter = string.Format("{0} {1} {2}", left.Member.Name, OdataOperatorsUtils.OdataOperator[expressionType], GetObjectFormated(right));
        return new ExpressionFilter<T>()
        {
            SearchClient = expressionFilter.SearchClient,
            Filter = string.Format(formatBase, expressionFilter.Filter, "and", filter)
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