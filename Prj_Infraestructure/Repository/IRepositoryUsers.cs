
using Prj_Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_Infraestructure.Repositorys
{
    public interface  IRepositoryUsers
    {
        Task<RI_Users> Login(string userName, string userPassword);
        Task<IEnumerable<RI_Users>> GetUsersAsync();
        Task<RI_Users> SaveAsync(RI_Users ri_Users);
        Task<RI_Users> GetUserByIdAsync(int? id);
        Task<RI_Users> GetUserByCustomerAsync(Guid? customerID);
        Task DeleteAsync(int id);

    }
}
