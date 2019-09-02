// -----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace Rangers.Antidrift.Drift.Core
{
    using System;
    using System.Linq.Dynamic.Core;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Helpful string extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Expands <see cref="TeamProject"/> property values in a string.
        /// </summary>
        /// <param name="target">The <see cref="string"/> where to expand property values in.</param>
        /// <param name="teamProject">The <see cref="TeamProject"/> instance which property values should be added to the <paramref name="target"/>.</param>
        /// <returns>The <see cref="string"/> which contains expanded property values.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="target"/> or <paramref name="teamProject"/> is null.</exception>
        public static string Expand(this string target, TeamProject teamProject)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (teamProject == null)
            {
                throw new ArgumentNullException(nameof(teamProject));
            }

            return Regex.Replace(target, @"{(?<exp>[^}]+)}", match =>
            {
                var p = Expression.Parameter(typeof(TeamProject), "teamProject");
                var e = DynamicExpressionParser.ParseLambda(new[] { p }, null, match.Groups["exp"].Value);

                return (e.Compile().DynamicInvoke(teamProject) ?? string.Empty).ToString();
            });
        }
    }
}