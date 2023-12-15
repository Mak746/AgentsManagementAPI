using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IAgentService
    {
        Task<Agent> GetAgentByIdAsync(Guid id);
        Task<IReadOnlyList<Agent>> ListAllAgentsAsync();
        Task<Agent> AddAgent(Agent entity);
        Task<Agent> UpdateAgent(Agent entity);
        Task<Agent> DeleteAgent(Guid id);
    }
}