using System.Collections.Generic;

namespace Rangers.Antidrift.Drift.Core
{
    public class TeamProject
    {
        public string[] AgentPools { get; set; } = new string[0];

        public string Key { get; set; }

        public string Name { get; set; }

        public IList<Pattern> Patterns { get; set; } = new List<Pattern>();

        public IList<Team> Teams { get; set; } = new List<Team>();

        public TeamProjectStatus Status { get; set; } = TeamProjectStatus.Active;
    }
}