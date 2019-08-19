using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rangers.Antidrift.Drift.Core
{
    public class TeamProject
    {
        public string[] AgentPools { get; set; } = new string[0];

        public Guid Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public IList<Pattern> Patterns { get; set; } = new List<Pattern>();

        public IList<Team> Teams { get; set; } = new List<Team>();

        public TeamProjectStatus Status { get; set; } = TeamProjectStatus.Active;

        public async virtual Task<IEnumerable<Deviation>> CollectDeviations()
        {
            var tasks = new List<Task<IEnumerable<Deviation>>>();

            foreach (var pattern in this.Patterns)
            {
                 var task = pattern.CollectDeviations(this);
                 tasks.Add(task);
            }

            var results = await Task.WhenAll(tasks);
            return results.SelectMany(r => r);
        }
    }
}