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
    public class ServiceRole : IServiceRole
    {
        private IRepositoryRole repository;
        public ServiceRole()
        {
            repository = new RepositoryRole();
        }

        public async Task<IEnumerable<RI_Role>> GetRolesAsync()
        {
            return await repository.GetRoleAsync();
        }
    }
}
