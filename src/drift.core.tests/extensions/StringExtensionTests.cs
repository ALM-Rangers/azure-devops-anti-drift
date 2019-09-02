// -----------------------------------------------------------------------
// <copyright file="StringExtensionTests.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace Rangers.Antidrift.Drift.Core.Tests
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void Expand()
        {
            var teamProject = new TeamProject { Name = "Test" };

            var target = "We are testing team project {teamProject.Name}.";
            var actual = target.Expand(teamProject);

            actual.Should().Be("We are testing team project Test.");
        }
    }
}