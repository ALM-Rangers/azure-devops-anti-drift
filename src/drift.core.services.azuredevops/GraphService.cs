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
        private readonly Uri baseUri;
        private readonly VssCredentials credentials;

        public GraphService(Uri baseUri, VssCredentials credentials)
        {
            this.baseUri = baseUri;
            this.credentials = credentials;
        }

        public async Task<IEnumerable<ApplicationGroup>> GetApplicationGroups(TeamProject teamProject)
        {
            using(var client = new GraphHttpClient(this.baseUri, this.credentials))
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
            using(var client = new GraphHttpClient(this.baseUri, this.credentials))
            {
                var descriptor = await client.GetDescriptorAsync(teamProject.Id);
                var graphGroups = await client.ListGroupsAsync(descriptor.Value); // TODO: Overhead
                var group = graphGroups.GraphGroups.FirstOrDefault(g => g.DisplayName.Equals(applicationGroup.Name, StringComparison.OrdinalIgnoreCase));

                if(group == null)
                {
                    throw new Exception($"Cannot get the members fro application group {applicationGroup.Name}. the application group is not available.");
                }

                var memberships = await client.ListMembershipsAsync(group.Descriptor.Identifier, GraphTraversalDirection.Down);
                var lookupKeys = memberships
                    .Select(m => new GraphSubjectLookupKey(new SubjectDescriptor(string.Empty, m.MemberDescriptor))) // TODO: not sure what to fill in subject type.
                    .ToList(); 

                var result = await client.LookupSubjectsAsync(new GraphSubjectLookup(lookupKeys));
                return result.Select(l => l.Value.DisplayName).ToList();
            }
        }
    }
}