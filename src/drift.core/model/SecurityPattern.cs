using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rangers.Antidrift.Drift.Core
{
    public class SecurityPattern : Pattern
    {
        public IList<ApplicationGroup> ApplicationGroups { get; set; } = new List<ApplicationGroup>();

        public async override Task<IEnumerable<Deviation>> CollectDeviations(TeamProject teamProject)
        {
            var results = await base.CollectDeviations(teamProject);

            foreach (var applicationGroup in this.ApplicationGroups)
            {
                // Check if the application group exists
                // Check if the application group contains the correct members
                // Iterate through the namespaces and check the permissions
            }

            return results;
        }
    }
}