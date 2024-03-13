using System.Linq.Expressions;

namespace Klororf.AzSearchLinq.Utils;
internal static class OdataOperatorsUtils{
    internal static Dictionary<ExpressionType, string>  OdataOperator = new Dictionary<ExpressionType,string>(){
       {ExpressionType.Equal,"eq"},
       {ExpressionType.NotEqual, "ne"},
       {ExpressionType.LessThanOrEqual,"le"},
       {ExpressionType.LessThan,"lt"},
       {ExpressionType.GreaterThan,"gt"},
       {ExpressionType.GreaterThanOrEqual,"ge"}
       
    };
}