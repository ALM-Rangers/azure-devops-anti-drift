// -----------------------------------------------------------------------
// <copyright file="Organization.cs" company="ALM | DevOps Rangers">
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
    using System.Linq;
    using System.Threading.Tasks;

    public class Organization
    {
        public IDictionary<string, Guid> Mappings { get; } = new Dictionary<string, Guid>();

        public IList<Pattern> Patterns { get; } = new List<Pattern>();

        public IList<TeamProject> TeamProjects { get; } = new List<TeamProject>();

        public IList<Team> Teams { get; } = new List<Team>();

        public void Expand()
        {
            foreach (var teamProject in this.TeamProjects)
            {
                for (int i = 0; i < teamProject.Patterns.Count; i++)
                {
                    var pattern = this.Patterns.FirstOrDefault(p => p.Name.Equals(teamProject.Patterns[i].Name, StringComparison.OrdinalIgnoreCase));
                    teamProject.Patterns[i] = pattern.Expand(teamProject);
                }
            }
        }

        public async Task<IEnumerable<Deviation>> CollectDeviations()
        {
            var tasks = new List<Task<IEnumerable<Deviation>>>();

            foreach (var teamProject in this.TeamProjects)
            {
                var task = teamProject.CollectDeviations();
                tasks.Add(task);
            }

            var results = await Task.WhenAll(tasks).ConfigureAwait(false);
            return results.SelectMany(r => r);
        }
    }
}