// -----------------------------------------------------------------------
// <copyright file="Namespace.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;

namespace Rangers.Antidrift.Drift.Core
{
    public class Namespace
    {
        public string[] Allow { get; set; }

        public string[] Deny { get; set; }

        public string Name { get; set; }

        public Namespace Expand(TeamProject teamProject)
        {
            return new Namespace
            {
                Allow = this.Allow.Select(a => a.Expand(teamProject)).ToArray(),
                Deny = this.Deny.Select(d => d.Expand(teamProject)).ToArray(),
                Name = this.Name.Expand(teamProject)
            };
        }
    }
}