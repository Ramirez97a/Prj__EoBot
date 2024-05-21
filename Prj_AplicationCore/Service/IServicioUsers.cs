using Prj_Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_AplicationCore
{
    public interface IServicioUsers
    {
        Task<RI_Users> Login(string usermail, string userPassword);        
        Task<IEnumerable<RI_Users>> GetUsersAsync();
        Task<RI_Users> SaveAsync(RI_Users ri_Users);
        Task<RI_Users> GetUserByIdAsync(int id);
        Task<RI_Users> GetUserByCustomerAsync(Guid? customerID);
        Task DeleteAsync(int id);
        Task<IEnumerable<RI_Users>> GetUsersByCustomerAsync(Guid? customerID);
    }
}
