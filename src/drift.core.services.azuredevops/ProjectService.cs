// -----------------------------------------------------------------------
// <copyright file="ProjectService.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace Rangers.Antidrift.Drift.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.TeamFoundation.Core.WebApi;
    using Microsoft.VisualStudio.Services.WebApi;

    public class ProjectService
    {
        private readonly VssConnection connection;

        public ProjectService(VssConnection connection)
        {
            this.connection = connection;
        }

        public async Task<IEnumerable<Core.TeamProject>> GetProjects()
        {
            var client = this.connection.GetClient<ProjectHttpClient>();

            var projectsInPage = await client.GetProjects(top: 250).ConfigureAwait(false);
            var projects = projectsInPage.ToList();

            while (!string.IsNullOrWhiteSpace(projectsInPage.ContinuationToken))
            {
                projectsInPage = await client.GetProjects(continuationToken: projectsInPage.ContinuationToken).ConfigureAwait(false);
                projects.AddRange(projectsInPage.ToArray());
            }

            return projects.Select(tp => new Core.TeamProject { Id = tp.Id, Name = tp.Name });
        }
    }
}
