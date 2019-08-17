using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rangers.Antidrift.Drift.Core
{
    public abstract class Pattern
    {
        public string Name { get; set; }

        public abstract Task<IEnumerable<Deviation>> CollectDeviations(TeamProject teamProject);
    }
}