using Prj_Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_Infraestructure.Repository
{
    public interface IRepositoryRole
    {
        Task<IEnumerable<RI_Role>> GetRoleAsync();

    }
}
