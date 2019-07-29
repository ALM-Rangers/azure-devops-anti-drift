using System;
using System.Collections.Generic;

namespace Rangers.Antidrift.Drift
{
    public class Organization
    {
        public IDictionary<string, Guid> Mappings = new Dictionary<string, Guid>();

        public IList<Pattern> Patterns { get; set; } = new List<Pattern>();

        public IList<TeamProject> TeamProjects { get; set; } = new List<TeamProject>();

        public IList<Team> Teams { get; set; } = new List<Team>();
    }
}