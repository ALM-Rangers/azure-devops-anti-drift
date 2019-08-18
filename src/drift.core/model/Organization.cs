using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rangers.Antidrift.Drift.Core
{
    public class Organization
    {
        public IDictionary<string, Guid> Mappings = new Dictionary<string, Guid>();

        public IList<Pattern> Patterns { get; set; } = new List<Pattern>();

        public IList<TeamProject> TeamProjects { get; set; } = new List<TeamProject>();

        public IList<Team> Teams { get; set; } = new List<Team>();

        public async Task<IEnumerable<Deviation>> CollectDeviations()
        {
            var tasks = new List<Task<IEnumerable<Deviation>>>();

            foreach (var teamProject in this.TeamProjects)
            {
                 var task = teamProject.CollectDeviations();
                 tasks.Add(task);
            }

            var results = await Task.WhenAll(tasks);
            return results.SelectMany(r => r);
        }
    }
}