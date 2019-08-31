using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Rangers.Antidrift.Drift.Core
{
    public static class StringExtensions
    {
        public static string Expand(this string target, TeamProject teamProject)
        {
            return Regex.Replace(target, @"{(?<exp>[^}]+)}", match => {
                var p = Expression.Parameter(typeof(TeamProject), "teamProject");
                var e = DynamicExpressionParser.ParseLambda(new[] { p }, null, match.Groups["exp"].Value);

                return (e.Compile().DynamicInvoke(teamProject) ?? string.Empty).ToString();
            });
        }
    }
}