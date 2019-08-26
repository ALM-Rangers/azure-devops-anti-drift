namespace Rangers.Antidrift.Drift.Core.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class OrganizationTests
    {
        [TestMethod]
        public async Task CollectDeviations()
        {
            // Arrange
            var teamProject1 = new Mock<TeamProject>();
            var teamProject2 = new Mock<TeamProject>();

            var deviation1 = new Deviation();
            var deviation2 = new Deviation();

            teamProject1.Setup(t => t.CollectDeviations()).ReturnsAsync(new List<Deviation> { deviation1 });
            teamProject2.Setup(t => t.CollectDeviations()).ReturnsAsync(new List<Deviation> { deviation2 });

            var target = new Organization();
            target.TeamProjects.Add(teamProject1.Object);
            target.TeamProjects.Add(teamProject2.Object);

            // Act
            var actual = await target.CollectDeviations().ConfigureAwait(false);

            // Assert
            teamProject1.VerifyAll();
            teamProject2.VerifyAll();

            actual.Should()
                  .Contain(deviation1)
                  .And
                  .Contain(deviation2);


            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
