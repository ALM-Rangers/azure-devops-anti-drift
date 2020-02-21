// -----------------------------------------------------------------------
// <copyright file="ProjectCollectionServiceTests.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace Rangers.Antidrift.Drift.Core.Services.AzureDevOps.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.VisualStudio.Services.Common;
    using Microsoft.VisualStudio.Services.WebApi;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProjectServiceTests
    {
        public TestContext TestContext { get; set; }

        public string Organization
        {
            get { return this.TestContext.Properties["organization"].ToString(); }
        }

        public string PAT
        {
            get { return this.TestContext.Properties["pat"].ToString(); }
        }

        private Guid Antidrift => new Guid(this.TestContext.Properties["antidriftprojectguid"].ToString());

        [TestMethod]
        public async Task GetProjects()
        {
            // Arrange
            var credentials = new VssBasicCredential(string.Empty, this.PAT);
            var url = $"https://dev.azure.com/{this.Organization}";
            var connection = new VssConnection(new Uri(url), credentials);

            var expected = new TeamProject { Id = this.Antidrift, Name = "Antidrift" };

            var target = new ProjectService(connection);

            // Act
            var actual = await target.GetProjects().ConfigureAwait(false);

            // Assert
            actual.Should().ContainEquivalentOf(expected);

            connection.Dispose();
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
