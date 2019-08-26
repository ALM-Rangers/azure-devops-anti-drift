// <copyright file="GraphServiceTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
    public class GraphServiceTests
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

        [TestMethod]
        public async Task GetApplicationGroups()
        {
            // Arrange
            var credentials = new VssBasicCredential(string.Empty, this.PAT);
            var url = $"https://dev.azure.com/{this.Organization}";
            var connection = new VssConnection(new Uri(url), credentials);

            var teamProject = new TeamProject { Id = new Guid("755bb057-51ac-43f6-a91a-54103f19b800"), Name = "Antidrift" };

            var expected = new List<string> { "Project Valid Users", "Project Administrators", "Contributors", "Build Administrators", "Readers", "Antidrift Team" };

            var target = new GraphService(connection);

            // Act
            var actual = await target.GetApplicationGroups(teamProject).ConfigureAwait(false);

            // Assert
            actual.Select(a => a.Name).Should().Contain(expected);

            await Task.CompletedTask;
        }

        [TestMethod]
        public async Task GetMembers()
        {
            // Arrange
            var credentials = new VssBasicCredential(string.Empty, this.PAT);
            var url = $"https://dev.azure.com/{this.Organization}";
            var connection = new VssConnection(new Uri(url), credentials);

            var applicationGroup = new ApplicationGroup { Name = "Contributors" };
            var teamProject = new TeamProject { Id = new Guid("755bb057-51ac-43f6-a91a-54103f19b800"), Name = "Antidrift" };

            var expected = new List<string> { "Antidrift Team" };

            var target = new GraphService(connection);

            // Act
            var actual = await target.GetMembers(teamProject, applicationGroup).ConfigureAwait(false);

            // Assert
            actual.Should().Contain(expected);

            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}