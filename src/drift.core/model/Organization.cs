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