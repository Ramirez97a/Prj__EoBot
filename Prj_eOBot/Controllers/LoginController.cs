using Prj_AplicationCore;
using Prj_eOBot.Models;
using Prj_Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Prj_eOBot.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }       

        public async Task<ActionResult> LoginUser(string usermail, string userPassword)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServicioUsers service = new ServiceUsers();

                RI_Users Users = await service.Login(usermail, userPassword);

                if (Users == null)
                {
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = "Usuario no autorizado ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Usuario autorizado";

                    Session["User"] = Users;
                    ViewBag.Role = Users.Role;
                }

                return Json(response);
            }
            catch (Exception e)
            {

                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = e.Message;

                return Json(response);
            }

        }
    }
}
