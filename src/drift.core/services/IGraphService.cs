namespace Rangers.Antidrift.Drift.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGraphService
    {
        Task<IEnumerable<ApplicationGroup>> GetApplicationGroups(TeamProject teamProject);

        Task<IEnumerable<string>> GetMembers(TeamProject teamProject, ApplicationGroup applicationGroup);
    }
}