using System.Collections.Generic;

namespace Rangers.Antidrift.Drift.Core
{
    public class SecurityPattern : Pattern
    {
        public IList<ApplicationGroup> ApplicationGroups { get; set; } = new List<ApplicationGroup>();

        public override string GenerateDriftReport(TeamProject teamProject)
        {
            var result = string.Empty;

            foreach (var applicationGroup in this.ApplicationGroups)
            {
                
            }

            return result;
        }
    }
}