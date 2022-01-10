using Portfolio.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Application.Contracts.Data
{
    public interface ISkillsRepository : IAsyncRepository<Skill>
    {
        Task<List<int>> GetSkillPriorities(bool update);
        Task UpdateAsync(Skill entity, int oldPriority);
        Task DeleteAsync(Skill entity, int oldPriority);
    }
}
