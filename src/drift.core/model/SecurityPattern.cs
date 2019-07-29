using System.Collections.Generic;

namespace Rangers.Antidrift.Drift.Core
{
    public class SecurityPattern : Pattern
    {
        public IList<ApplicationGroup> ApplicationGroups { get; set; } = new List<ApplicationGroup>();
    }
}