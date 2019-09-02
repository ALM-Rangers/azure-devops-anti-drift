// -----------------------------------------------------------------------
// <copyright file="ApplicationGroup.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace Rangers.Antidrift.Drift.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class ApplicationGroup
    {
        public string Descriptor { get; set; }

        public bool IsSpecial { get; set; }

        public string Name { get; set; }

        public IList<Namespace> Namespaces { get; set; } = new List<Namespace>();

        public string[] Members { get; set; } = System.Array.Empty<string>();

        public ApplicationGroup Expand(TeamProject teamProject)
        {
            var result = new ApplicationGroup();
            result.Name = this.Name.Expand(teamProject);
            result.Namespaces = this.Namespaces.Select(n => n.Expand(teamProject)).ToList();
            result.Members = this.Members.Select(m => m.Expand(teamProject)).ToArray();

            return result;
        }
    }
}