using Prj_Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_AplicationCore.Service
{
    public interface IServiceClient
    {
        Task<IEnumerable<Rl_Robot>> GetRobotClientAsync(); 
        Task<IEnumerable<Rl_Robot>> GetClientDataByDateSubscribeAsync();
        Task<Rl_Robot> SaveAsync(Rl_Robot ri_Robot);
        Task<Rl_Robot> GetRobotClientByIdAsync(Guid? id);
        Task<Rl_Robot> GetRobotClientByEmaildAsync(string email);
        Task DeleteAsync(Guid? id);
    }
}
