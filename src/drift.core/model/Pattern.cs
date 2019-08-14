using System;

namespace Rangers.Antidrift.Drift.Core
{
    public abstract class Pattern
    {
        public string Name { get; set; }

        public abstract string GenerateDriftReport(TeamProject teamProject);
    }
}