// -----------------------------------------------------------------------
// <copyright file="GraphService.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace Rangers.Antidrift.Drift.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.Services.Graph;
    using Microsoft.VisualStudio.Services.Graph.Client;
    using Microsoft.VisualStudio.Services.WebApi;

    public class GraphService : IGraphService
    {
        private readonly VssConnection connection;

        public GraphService(VssConnection connection)
        {
            this.connection = connection;
        }

        public async Task<IEnumerable<ApplicationGroup>> GetApplicationGroups(TeamProject teamProject)
        {
            if (teamProject == null)
            {
                throw new ArgumentNullException(nameof(teamProject));
            }

            using (var client = this.connection.GetClient<GraphHttpClient>())
            {
                var descriptor = await client.GetDescriptorAsync(teamProject.Id).ConfigureAwait(false);
                var result = await client.ListGroupsAsync(descriptor.Value).ConfigureAwait(false);

                return result.GraphGroups
                    .Select(g => new ApplicationGroup { Descriptor = g.Descriptor.Identifier, Name = g.DisplayName })
                    .ToList();
            }
        }

        public async Task<IEnumerable<string>> GetMembers(TeamProject teamProject, ApplicationGroup applicationGroup)
        {
            if (teamProject == null)
            {
                throw new ArgumentNullException(nameof(teamProject));
            }

            if (applicationGroup == null)
            {
                throw new ArgumentNullException(nameof(applicationGroup));
            }

            using (var client = this.connection.GetClient<GraphHttpClient>())
            {
                var descriptor = await client.GetDescriptorAsync(teamProject.Id).ConfigureAwait(false);
                var graphGroups = await client.ListGroupsAsync(descriptor.Value).ConfigureAwait(false); // TODO: Overhead
                var group = graphGroups.GraphGroups.FirstOrDefault(g => g.DisplayName.Equals(applicationGroup.Name, StringComparison.OrdinalIgnoreCase));

                if (group == null)
                {
                    throw new InvalidOperationException($"Cannot get the members fro application group {applicationGroup.Name}. the application group is not available.");
                }

                var memberships = await client.ListMembershipsAsync(group.Descriptor.ToString(), GraphTraversalDirection.Down).ConfigureAwait(false);
                var lookupKeys = memberships
                    .Select(m => new GraphSubjectLookupKey(m.MemberDescriptor)) // TODO: not sure what to fill in subject type.
                    .ToList();

                var result = await client.LookupSubjectsAsync(new GraphSubjectLookup(lookupKeys)).ConfigureAwait(false);
                return result.Select(l => l.Value.DisplayName).ToList();
            }
        }
    }
}