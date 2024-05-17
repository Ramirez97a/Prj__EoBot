using ApplicationCore.Services;
using prj_Infraestructure.Repositorys;
using Prj_Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_AplicationCore
{
    public class ServiceUsers : IServicioUsers
    {
        private IRepositoryUsers repository;
        public ServiceUsers()
        {
            repository = new RepositoryUsers();
        }
        public async Task<RI_Users> Login(string usermail, string userPassword)
        {
            //string codifiedPass = Security.EncrypthAES(userPassword);
            return await repository.Login(usermail, userPassword);
        }

        public async Task<IEnumerable<RI_Users>> GetUsersAsync()
        {
            return await repository.GetUsersAsync();
        }

        public async Task<RI_Users> SaveAsync(RI_Users ri_Users)
        {
            return await repository.SaveAsync(ri_Users);
        }

        public async Task<RI_Users> GetUserByIdAsync(int id)
        {
            return await repository.GetUserByIdAsync(id);
        }
        public async Task<RI_Users> GetUserByCustomerAsync(Guid? customerID)
        {
            return await repository.GetUserByCustomerAsync(customerID);
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync(id);
        }

       
    }
}
