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
             try
             {
                _unitOfWork.Repository<Agent>().Add(entity);
            
                var result = await _unitOfWork.Complete();

                if (result <= 0) return null;
           
                return entity;
             }
             catch (Exception ex)
             {
                throw;
             }

        }

        public async Task<Agent> DeleteAgent(int id)
        {
             try
             {
                var result = await _unitOfWork.Repository<Agent>().GetByIdAsync(id);

                _unitOfWork.Repository<Agent>().Delete(result);
                var deletedFromDb = await _unitOfWork.Complete();

                if (deletedFromDb <= 0) return null;
                return result;
             }
             catch (Exception ex)
             {
                 throw;
             }

        }

        public async Task<Agent> GetAgentByIdAsync(int id)
        {
             try
             {
                var result = await _unitOfWork.Repository<Agent>().GetByIdAsync(id);
                return result;
             }
             catch (Exception ex)
             {
                 throw;
             }

        }

        public async Task<IReadOnlyList<Agent>> ListAllAgentsAsync()
        {
             try
             {
                var result = await _unitOfWork.Repository<Agent>().ListAllAsync();
                return result;
             }
             catch (Exception ex)
             {
                 throw;
             }

        }

        public async Task<Agent> UpdateAgent(Agent entity)
        {
             try
             {
                _unitOfWork.Repository<Agent>().Update(entity);
                var result = await _unitOfWork.Complete();
                if (result <= 0) return null;
                return entity;
             }
             catch (Exception ex)
             {
                 throw;
             }

        }
    }
}