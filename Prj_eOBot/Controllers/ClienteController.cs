using Prj_AplicationCore;
using Prj_AplicationCore.Service;
using Prj_Infraestructure.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Prj_eOBot.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        public ActionResult Create()
        {
            RI_Users user = (RI_Users)Session["User"];

            ViewBag.Role = user.Role;
            return View();
        }
        public async Task<ActionResult> List()
        {
            IEnumerable<Rl_Robot> olista = null;
            try
            {
                IServiceClient _servicioClient = new ServiceClient();

                RI_Users user = (RI_Users)Session["User"];

                if (user.Role == 1 || user.Role == 2)
                {
                    // *Usuario Administrador General
                    // Usuario General  
                    olista = await _servicioClient.GetRobotClientAsync();
                }
                else if (user.Role == 3 || user.Role == 4)
                {
                    // Usuario Administrador Cliente
                    // Usuario Cliente
                    var robot = await _servicioClient.GetRobotClientByIdAsync(user.CustomerID);
                    olista = new List<Rl_Robot> { robot };
                }
                ViewBag.Role = user.Role;

                return View(olista);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<JsonResult> ListClient()
        {            
            try
            {
                IServiceClient _servicioClient = new ServiceClient();

                RI_Users user = (RI_Users)Session["User"];

                IEnumerable<Rl_Robot> olista = null;

                if (user.Role == 1 || user.Role ==2)
                {
                    // *Usuario Administrador General
                    // Usuario General  
                    olista = await _servicioClient.GetRobotClientAsync();
                }
                else if (user.Role == 3 || user.Role == 4)
                {
                    // Usuario Administrador Cliente
                    // Usuario Cliente
                    var robot = await _servicioClient.GetRobotClientByIdAsync(user.CustomerID);
                    olista = new List<Rl_Robot> { robot };
                }
             
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
        public async Task<ActionResult> Edit(Guid? id)
        {
            try
            {
                IServiceClient _servicioClient = new ServiceClient();
                RI_Users user = (RI_Users)Session["User"];
                Rl_Robot ri_robot = null;
                if (user.Role == 1)
                {
                     ri_robot = await _servicioClient.GetRobotClientByIdAsync(id);
                }               

                ViewBag.Role = user.Role;
                return View(new List<Rl_Robot> { ri_robot });
            }
            catch (Exception ex)
            {
                string mensaje = "Error al listar" + ex;
                throw new Exception(mensaje);
            }
        }
        public async Task<JsonResult> Save(Rl_Robot ri_robot)
        {
            IServiceClient _servicioClient = new ServiceClient();

            try
            {          

                await _servicioClient.SaveAsync(ri_robot);
                return Json(new { success = true, message = "Registro guardado con éxito" });
            }
            catch (Exception ex)
            {
                string errorMessage = "Error al guardar el registro: " + ex.Message;
                return Json(new { success = false, message = errorMessage });
            }

        }
    }
}