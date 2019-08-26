namespace Rangers.Antidrift.Drift.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Pattern
    {
        public string Name { get; set; }

        public async virtual Task<IEnumerable<Deviation>> CollectDeviations(TeamProject teamProject)
        {
            return await Task.FromResult(new List<Deviation>()).ConfigureAwait(false);
        }
    }
}