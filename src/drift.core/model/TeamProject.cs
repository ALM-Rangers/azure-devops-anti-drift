// -----------------------------------------------------------------------
// <copyright file="TeamProject.cs" company="ALM | DevOps Rangers">
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

    public class TeamProject
    {
        public string[] AgentPools { get; set; } = Array.Empty<string>();

        public Guid Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public IList<Pattern> Patterns { get; } = new List<Pattern>();

        public IList<Team> Teams { get; } = new List<Team>();

        public TeamProjectStatus Status { get; set; } = TeamProjectStatus.Active;

        public async virtual Task<IEnumerable<Deviation>> CollectDeviations()
        {
            var tasks = new List<Task<IEnumerable<Deviation>>>();

            foreach (var pattern in this.Patterns)
            {
                var task = pattern.CollectDeviations(this);
                tasks.Add(task);
            }

            var results = await Task.WhenAll(tasks).ConfigureAwait(false);
            return results.SelectMany(r => r);
        }
    }
}