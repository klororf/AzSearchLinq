using System.Linq.Expressions;
using Azure.Search.Documents;
using Klororf.AzSearchLinq.Utils;
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

    internal static ExpressionFilter<T> WhereSimpleBinary<T>(this SearchClient searchClient, BinaryExpression binaryExpression, ExpressionType expressionType)
    {
        if (expressionType.IsLogicalOperator())
        {

            BinaryExpression binaryExpressionLeft = (BinaryExpression)binaryExpression.Left;
            BinaryExpression binaryExpressionRight = (BinaryExpression)binaryExpression.Right;
            var leftHasParentheses = binaryExpressionLeft.ToString().EndsWith("))");
            var rightHasParentheses = binaryExpressionRight.ToString().EndsWith("))");
            ExpressionFilter<T> expressionFilterLeft = searchClient.WhereSimpleBinary<T>(binaryExpressionLeft, binaryExpressionLeft.NodeType);
            if (leftHasParentheses)
            {
                expressionFilterLeft.AddParentheses();
            }
            ExpressionFilter<T> expressionFilterRight = expressionFilterLeft.FromEmpty().WhereSimpleBinary<T>(binaryExpressionRight, binaryExpressionRight.NodeType);
            if (rightHasParentheses)
            {
                expressionFilterRight.AddParentheses();
            }
            return expressionFilterLeft.ConcatWithOperator(expressionFilterRight,expressionType);
        }
        else
        {
            return searchClient.WhereBinary<T>(binaryExpression, binaryExpression.NodeType);
        }
    }

    internal static ExpressionFilter<T> WhereSimpleBinary<T>(this ExpressionFilter<T> expressionFilter, BinaryExpression binaryExpression, ExpressionType expressionType)
    {
        if (expressionType.IsLogicalOperator())
        {
            BinaryExpression binaryExpressionLeft = (BinaryExpression)binaryExpression.Left;
            BinaryExpression binaryExpressionRight = (BinaryExpression)binaryExpression.Right;
            var leftHasParentheses = binaryExpressionLeft.ToString().EndsWith("))");
            var rightHasParentheses = binaryExpressionRight.ToString().EndsWith("))");
            ExpressionFilter<T> expressionFilterLeft = expressionFilter.FromEmpty().WhereSimpleBinary<T>(binaryExpressionLeft, binaryExpressionLeft.NodeType);
            if (leftHasParentheses)
            {
                expressionFilterLeft.AddParentheses();
            }
            ExpressionFilter<T> expressionFilterRight = expressionFilter.FromEmpty().WhereSimpleBinary<T>(binaryExpressionRight, binaryExpressionRight.NodeType);
            if (rightHasParentheses)
            {
                expressionFilterRight.AddParentheses();
            }
            return expressionFilter.Concat(expressionFilterLeft.ConcatWithOperator(expressionFilterRight,expressionType));
        }
        else
        {
            return expressionFilter.WhereBinary<T>(binaryExpression, binaryExpression.NodeType);
        }
    }
    internal static ExpressionFilter<T> WhereBinary<T>(this ExpressionFilter<T> expressionFilter, BinaryExpression binaryExpression, ExpressionType expressionType)
    {
        MemberExpression left = (MemberExpression)binaryExpression.Left;
        ConstantExpression right = (ConstantExpression)binaryExpression.Right;
        return new ExpressionFilter<T>()
        {
            SearchClient = expressionFilter.SearchClient,
            Filter = string.Format("{0} {1} {2}", left.Member.Name, OdataOperatorsUtils.OdataOperator[expressionType], GetObjectFormated(right))
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