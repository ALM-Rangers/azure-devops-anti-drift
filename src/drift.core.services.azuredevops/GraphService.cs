using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.Graph;
using Microsoft.VisualStudio.Services.Graph.Client;
using Microsoft.VisualStudio.Services.WebApi;

namespace Rangers.Antidrift.Drift.Core.Services
{
    public class GraphService : IGraphService
    {
        private readonly VssConnection connection;

        public GraphService(VssConnection connection)
        {
            this.connection = connection;
        }

        public async Task<IEnumerable<ApplicationGroup>> GetApplicationGroups(TeamProject teamProject)
        {
            using(var client = this.connection.GetClient<GraphHttpClient>())
            {
                var descriptor = await client.GetDescriptorAsync(teamProject.Id);
                var result = await client.ListGroupsAsync(descriptor.Value);

                return result.GraphGroups
                    .Select(g => new ApplicationGroup { Descriptor = g.Descriptor.Identifier, Name = g.DisplayName })
                    .ToList();
            }
        }

        public async Task<IEnumerable<string>> GetMembers(TeamProject teamProject, ApplicationGroup applicationGroup)
        {
            using(var client = this.connection.GetClient<GraphHttpClient>())
            {
                var descriptor = await client.GetDescriptorAsync(teamProject.Id);
                var graphGroups = await client.ListGroupsAsync(descriptor.Value); // TODO: Overhead
                var group = graphGroups.GraphGroups.FirstOrDefault(g => g.DisplayName.Equals(applicationGroup.Name, StringComparison.OrdinalIgnoreCase));

                if(group == null)
                {
                    throw new Exception($"Cannot get the members fro application group {applicationGroup.Name}. the application group is not available.");
                }

                var memberships = await client.ListMembershipsAsync(group.Descriptor.ToString(), GraphTraversalDirection.Down);
                var lookupKeys = memberships
                    .Select(m => new GraphSubjectLookupKey(m.MemberDescriptor)) // TODO: not sure what to fill in subject type.
                    .ToList(); 

                var result = await client.LookupSubjectsAsync(new GraphSubjectLookup(lookupKeys));
                return result.Select(l => l.Value.DisplayName).ToList();
            }
        }
    }
}