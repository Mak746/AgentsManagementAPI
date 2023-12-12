using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class AgentService : IAgentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AgentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<Agent> AddAgent(Agent entity)
        {

            _unitOfWork.Repository<Agent>().Add(entity);
            // TODO: save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;
            // return agent
            return entity;
        }

        public async Task<Agent> DeleteAgent(Agent entity)
        {
            _unitOfWork.Repository<Agent>().Delete(entity);
            // TODO: save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;
            // return agent
            return entity;
        }

        public async Task<Agent> GetAgentByIdAsync(int id)
        {
            var result = await _unitOfWork.Repository<Agent>().GetByIdAsync(id);
            return result;
        }

        public async Task<IReadOnlyList<Agent>> ListAllAgentsAsync()
        {
            var result = await _unitOfWork.Repository<Agent>().ListAllAsync();
            // TODO: save to db
            // var result = await _unitOfWork.Complete();

            // if (result <= 0) return null;
            // return agent
            return result;
        }

        public async Task<Agent> UpdateAgent(Agent entity)
        {
            _unitOfWork.Repository<Agent>().Update(entity);
            // TODO: save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;
            // return agent
            return entity;
        }
    }
}