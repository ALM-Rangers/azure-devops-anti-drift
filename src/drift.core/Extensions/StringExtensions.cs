// -----------------------------------------------------------------------
// <copyright file="OrganizationTests.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace Rangers.Antidrift.Drift.Core
{    
    using System.Linq.Dynamic.Core;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;

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