// -----------------------------------------------------------------------
// <copyright file="Pattern.cs" company="ALM | DevOps Rangers">
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
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Pattern
    {
        public string Name { get; set; }

        public async virtual Task<IEnumerable<Deviation>> CollectDeviations(TeamProject teamProject)
        {
            return await Task.FromResult(new List<Deviation>()).ConfigureAwait(false);
        }

        public virtual Pattern Expand(TeamProject teamProject)
        {
            return this.Expand(new Pattern(), teamProject);
        }

        public virtual Pattern Expand(Pattern pattern, TeamProject teamProject)
        {   
            pattern.Name = this.Name.Expand(teamProject);
            return pattern;
        }
    }
}