using Prj_Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_Infraestructure.Repository
{
    public interface  IRepositoryClient
    {
        Task<IEnumerable<Rl_Robot>> GetClientAsync();
        Task<IEnumerable<Rl_Robot>> GetClientDataByDateSubscribeAsync();
        Task<Rl_Robot> SaveAsync(Rl_Robot ri_robot);
        Task<Rl_Robot> GetRobotClientByIdAsync(Guid? id);
        Task<Rl_Robot> GetRobotClientByEmaildAsync(string email);
        Task DeleteAsync(Guid? id);
    }
}
