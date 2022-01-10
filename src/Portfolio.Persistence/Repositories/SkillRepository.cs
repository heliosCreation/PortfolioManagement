using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Contracts.Data;
using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Persistence.Repositories
{
    public class SkillRepository : BaseRepository<Skill>, ISkillsRepository
    {
        public SkillRepository(PortfolioDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<int>> GetSkillPriorities(bool update)
        {
            var capslock = update ? 1 : 2;
            if (_dbContext.Skills.Any())
            {
                var max = await _dbContext.Skills.AsNoTracking().MaxAsync(s => s.Priority);
                var range = new List<int>();
                for (int i = 1; i < max + capslock; i++)
                {
                    range.Add(i);
                }

                return range;
            }
            return new List<int> { 1 };
        }
        public override async Task<IEnumerable<Skill>> ListAllAsync()
        {
            var skills = await base.ListAllAsync() as IEnumerable<Skill>;
            var ordered = skills.OrderBy(s => s.Priority);
            return ordered;
        }

        public override async Task<Skill> AddAsync(Skill entity)
        {
            var priorities = await GetSkillPriorities(false);
            var priority = entity.Priority;
            if (priority == priorities.Last())
            {
                return await base.AddAsync(entity);
            }
            else
            {
                await SetPriorities(priority, true);
                return await base.AddAsync(entity);
            }
        }

        public async Task UpdateAsync(Skill entity, int oldPriority)
        {
            var priorities = await GetSkillPriorities(true);
            var priority = entity.Priority;

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            await SetUpdatePriorities(priority, oldPriority, entity.Id);
            await _dbContext.SaveChangesAsync();


        }

        public async Task DeleteAsync(Skill entity, int oldPriority)
        {
            var priorities = await GetSkillPriorities(false);
            var priority = entity.Priority;
            await base.DeleteAsync(entity);

            await SetPriorities(priority, false);
            await _dbContext.SaveChangesAsync();
        }
        private async Task SetUpdatePriorities(int priority, int? oldPriority, Guid? Id)
        {
            //If the value gets higher
            if (priority > oldPriority)
            {
                //All the smaller priority shoud get minus 1
                var skillsToRenew = await _dbContext.Skills
                    .Where(s => s.Priority <= priority && s.Priority >= oldPriority)
                    .ToListAsync();
                foreach (var skill in skillsToRenew)
                {
                    if (skill.Id != Id)
                    {
                        skill.Priority -= 1;
                    }
                }
            }
            else
            {
                //Do the opposite
                var skillsToRenew = await _dbContext.Skills
                    .Where(s => s.Priority >= priority && s.Priority <= oldPriority)
                    .ToListAsync();
                foreach (var skill in skillsToRenew)
                {
                    if (skill.Id != Id)
                    {
                        skill.Priority += 1;
                    }
                }
            }

        }

        private async Task SetPriorities(int priority, bool isCreated)
        {
            var skillsToRenew = await _dbContext.Skills
                .Where(s => s.Priority >= priority)
                .ToListAsync();
            if (isCreated)
            {
                foreach (var skill in skillsToRenew)
                {
                    skill.Priority += 1;
                }
            }
            else
            {
                foreach (var skill in skillsToRenew)
                {
                    skill.Priority -= 1;
                }

            }

        }

    }
}
