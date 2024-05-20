using Prj_AplicationCore.Service;
using Prj_Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Prj_eOBot.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> ListRole()
        {
            try
            {
                IServiceRole _servicioRole = new ServiceRole();
                IEnumerable<RI_Role> olista = null;
                olista = await _servicioRole.GetRolesAsync();
              

                var jsonData = new
                {
                    success = true,
                    message = "Data encontrada",
                    Data = olista
                };

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
