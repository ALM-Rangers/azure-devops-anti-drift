using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rangers.Antidrift.Drift.Core
{
    public class SecurityPattern : Pattern
    {
        private readonly IGraphService graphService;

        public SecurityPattern(IGraphService graphService)
        {
            this.graphService = graphService;
        }

        public IList<ApplicationGroup> ApplicationGroups { get; set; } = new List<ApplicationGroup>();

        public async override Task<IEnumerable<Deviation>> CollectDeviations(TeamProject teamProject)
        {
            var results = (await base.CollectDeviations(teamProject)).ToList();
            var current = await this.graphService.GetApplicationGroups(teamProject);

            foreach (var applicationGroup in this.ApplicationGroups)
            {
                // Check if the application group exists
                if(current.All(c => !c.Name.Equals(applicationGroup.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    var deviation = new ApplicationGroupDeviation{ ApplicationGroup = applicationGroup, TeamProject = teamProject };
                    results.Add(deviation);
                    continue;
                }

                // Check if the application group contains the correct members
                var currentMembers = await this.graphService.GetMembers(teamProject, applicationGroup);

                foreach (var member in applicationGroup.Members)
                {
                    if(currentMembers.All(c => !c.Equals(member, StringComparison.OrdinalIgnoreCase)))
                    {
                        var deviation = new ApplicationGroupMemberDeviation { ApplicationGroup = applicationGroup, Member = member, TeamProject = teamProject, Type = DeviationType.Missing };
                        results.Add(deviation);
                    }
                }

                foreach (var member in currentMembers)
                {
                    if(applicationGroup.Members.All(c => !c.Equals(member, StringComparison.OrdinalIgnoreCase)))
                    {
                        var deviation = new ApplicationGroupMemberDeviation { ApplicationGroup = applicationGroup, Member = member, TeamProject = teamProject, Type = DeviationType.Obsolete };
                        results.Add(deviation);
                    }
                }
                
                // Iterate through the namespaces and check the permissions
            }

            return results;
        }
    }
}