// -----------------------------------------------------------------------
// <copyright file="TeamProjectTests.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace Rangers.Antidrift.Drift.Core.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class TeamProjectTests
    {
        [TestMethod]
        public async Task CollectDeviations()
        {
            // Arrange
            var pattern1 = new Mock<Pattern>();
            var pattern2 = new Mock<Pattern>();

            var deviation1 = new Deviation();
            var deviation2 = new Deviation();

            var target = new TeamProject();
            target.Patterns.Add(pattern1.Object);
            target.Patterns.Add(pattern2.Object);

            pattern1.Setup(t => t.CollectDeviations(target)).ReturnsAsync(new List<Deviation> { deviation1 });
            pattern2.Setup(t => t.CollectDeviations(target)).ReturnsAsync(new List<Deviation> { deviation2 });

            // Act
            var actual = await target.CollectDeviations().ConfigureAwait(false);

            // Assert
            pattern1.VerifyAll();
            pattern2.VerifyAll();

            actual.Should()
                  .Contain(deviation1)
                  .And
                  .Contain(deviation2);

            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
