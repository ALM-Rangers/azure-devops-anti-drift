// -----------------------------------------------------------------------
// <copyright file="ApplicationGroupMemberDeviation.cs" company="ALM | DevOps Rangers">
//    This code is licensed under the MIT License.
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF
//    ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//    TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR
//    A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// </copyright>
// -----------------------------------------------------------------------

namespace Rangers.Antidrift.Drift.Core
{
    public class ApplicationGroupMemberDeviation : ApplicationGroupDeviation
    {
        public string Member { get; set; }

        public override string ToString()
        {
            return $"Member {this.Member} of {this.ApplicationGroup.Name} is {this.Type} in Team Project {this.TeamProject.Name}.";
        }
    }
}