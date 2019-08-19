using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Rangers.Antidrift.Drift.Core.Tests
{
    [TestClass]
    public class SecurityPatternTests
    {
        [TestMethod]
        public async Task CollectDeviations()
        {
            // Arrange
            var applicationGroup = new ApplicationGroup { Name = "ApplicationGroup" };
            var graphService = new Mock<IGraphService>();
            var teamProject = new TeamProject();

            graphService.Setup(s => s.GetApplicationGroups(teamProject)).ReturnsAsync(new List<ApplicationGroup>());

            var target = new SecurityPattern(graphService.Object);
            target.ApplicationGroups.Add(applicationGroup);

            // Act
            var actual = await target.CollectDeviations(teamProject);

            // Assert
            actual
                .Should()
                .SatisfyRespectively(
                    first => 
                    {
                        first.Should().BeOfType<ApplicationGroupDeviation>();
                        ((ApplicationGroupDeviation)first).ApplicationGroup.Should().Be(applicationGroup);
                    });

            await Task.CompletedTask;
        }

        [TestMethod]
        public async Task CollectDeviations_MissingMembers()
        {
            // Arrange
            var applicationGroup = new ApplicationGroup { Name = "ApplicationGroup", Members = new[] { "Member 1" } };
            var current = new List<ApplicationGroup>{ new ApplicationGroup { Name = "ApplicationGroup" }};
            var graphService = new Mock<IGraphService>();
            var teamProject = new TeamProject();

            graphService.Setup(s => s.GetApplicationGroups(teamProject)).ReturnsAsync(current);
            graphService.Setup(s => s.GetMembers(teamProject, applicationGroup)).ReturnsAsync(new List<string>{ "Member 2" });

            var target = new SecurityPattern(graphService.Object);
            target.ApplicationGroups.Add(applicationGroup);

            // Act
            var actual = await target.CollectDeviations(teamProject);

            // Assert
            actual
                .Should()
                .SatisfyRespectively(
                    first => 
                    {
                        first.Should().BeOfType<ApplicationGroupMemberDeviation>();
                        ((ApplicationGroupMemberDeviation)first).ApplicationGroup.Should().Be(applicationGroup);
                        ((ApplicationGroupMemberDeviation)first).Member.Should().Be("Member 1");
                        ((ApplicationGroupMemberDeviation)first).Type.Should().Be(DeviationType.Missing);
                    },
                    second => 
                    {
                        second.Should().BeOfType<ApplicationGroupMemberDeviation>();
                        ((ApplicationGroupMemberDeviation)second).ApplicationGroup.Should().Be(applicationGroup);
                        ((ApplicationGroupMemberDeviation)second).Member.Should().Be("Member 2");
                        ((ApplicationGroupMemberDeviation)second).Type.Should().Be(DeviationType.Obsolete);
                    });

            await Task.CompletedTask;
        }
    }
}