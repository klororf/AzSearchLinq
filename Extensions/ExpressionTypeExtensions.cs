using System.Linq.Expressions;

namespace Klororf.AzSearchLinq.Extensions;

public static class ExpressionTypeExtensions{
    public static bool IsComparingOperator(this ExpressionType expressionType){
        return expressionType switch
        {
            ExpressionType.Equal or ExpressionType.NotEqual or ExpressionType.GreaterThanOrEqual or ExpressionType.GreaterThan or ExpressionType.LessThanOrEqual or ExpressionType.LessThan => true,
            _ => false,
        };
    }
}