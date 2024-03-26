namespace Klororf.AzSearchLinq;

public static class StringExtensions
{
    public static bool HaveParentheses(this string @string, bool fromExpression= true){
        string search = fromExpression ? "))" : ")";
        return @string.EndsWith(search);
    }
}
