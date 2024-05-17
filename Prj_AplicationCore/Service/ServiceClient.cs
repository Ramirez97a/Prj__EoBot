using prj_Infraestructure.Repositorys;
using Prj_Infraestructure.Models;
using Prj_Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_AplicationCore.Service
{
    public class ServiceClient : IServiceClient
    {
        private IRepositoryClient repository;
        public ServiceClient()
        {
            repository = new RepositoryClient();
        }
        public async Task DeleteAsync(Guid? id)
        {
            await repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Rl_Robot>> GetRobotClientAsync()
        {
            return await repository.GetClientAsync();
        }
        public async Task<IEnumerable<Rl_Robot>> GetClientDataByDateSubscribeAsync()
        {
            return await repository.GetClientDataByDateSubscribeAsync();
        }

        public async Task<Rl_Robot> GetRobotClientByIdAsync(Guid? id)
        {
            return await repository.GetRobotClientByIdAsync(id);
        }
        public async Task<Rl_Robot> GetRobotClientByEmaildAsync(string email)
        {
            return await repository.GetRobotClientByEmaildAsync(email);
        }
        public async Task<Rl_Robot> SaveAsync(Rl_Robot ri_Robot)
        {
            return await repository.SaveAsync(ri_Robot);
        }

    }
}
