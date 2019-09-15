// -----------------------------------------------------------------------
// <copyright file="Team.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;

namespace Rangers.Antidrift.Drift.Core
{
    public class Team
    {
        public string[] Members { get; set; }

        public string Name { get; set; }

        public Team Expand(TeamProject teamProject)
        {
            return new Team 
            {
                Name = this.Name.Expand(teamProject),
                Members = this.Members.Select(m => m.Expand(teamProject)).ToArray()
            };
        }
    }
}