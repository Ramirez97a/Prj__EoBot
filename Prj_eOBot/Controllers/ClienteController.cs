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
            RI_Users user = (RI_Users)Session["User"];
            if (user != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Login");
            }

          
        }
        public ActionResult Create()
        {
            RI_Users user = (RI_Users)Session["User"];
            if (user != null)
            {
                ViewBag.Role = user.Role;
                return View();
            }
            else
            {
                return RedirectToAction("NoAutorizado", "Login");
            }
          
        }
        public async Task<ActionResult> List()
        {
            IEnumerable<Rl_Robot> olista = null;
            try
            {
                IServiceClient _servicioClient = new ServiceClient();

                RI_Users user = (RI_Users)Session["User"];
                if (user != null)
                {
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
                else
                {
                    return RedirectToAction("NoAutorizado", "Login");
                }               

            }
            catch (Exception)
            {

                return RedirectToAction("NotFound", "Error");
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

                if (user != null)
                {
                    Rl_Robot ri_robot = null;

                    ri_robot = await _servicioClient.GetRobotClientByIdAsync(id);
                    if (ri_robot != null)
                    {
                        ViewBag.Role = user.Role;
                        return View(new List<Rl_Robot> { ri_robot });
                    }
                    else
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                }
                else
                {
                    return RedirectToAction("NoAutorizado", "Login");
                }

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
        public async Task<ActionResult> Delete(Guid? id)
        {
            try
            {
                IServiceClient _servicioClient = new ServiceClient();
                await _servicioClient.DeleteAsync(id);               
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                string mensaje = "Error al listar" + ex;
                throw new Exception(mensaje);
            }
        }
    }
}