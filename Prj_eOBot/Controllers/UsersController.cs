using Prj_AplicationCore;
using Prj_AplicationCore.Service;
using Prj_Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Prj_eOBot.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
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
        public async Task<ActionResult> Edit(int id)
        {
            RI_Users userInSession = (RI_Users)Session["User"];
            try
            {              
                IServicioUsers _servicioUsers = new ServiceUsers();
                IServiceClient _serviceClient = new ServiceClient();

                RI_Users user =  await _servicioUsers.GetUserByIdAsync(id);
                Rl_Robot robot = null;

                if (user.CustomerID != null) 
                { 
                    robot = await _serviceClient.GetRobotClientByIdAsync(user.CustomerID);
                    // Envía una lista que contenga solo el usuario a la vista
                    ViewBag.UserName = robot.UserName;                
                }
                ViewBag.Role = userInSession.Role;
                return View(new List<RI_Users> { user });
            }
            catch (Exception ex)
            {
                string mensaje = "Error al listar" + ex;
                throw new Exception(mensaje);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            RI_Users userInSession = (RI_Users)Session["User"];
            ViewBag.Role = userInSession.Role;
            try
            {
                IServicioUsers _servicioUsers = new ServiceUsers();
                if (userInSession.Role != 1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "No tiene permiso para realizar esta acción");
                }

                RI_Users user = await _servicioUsers.GetUserByIdAsync(id);
                   
                // Envía una lista que contenga solo el usuario a la vista
                return View(new List<RI_Users> { user });
            }
            catch (Exception ex)
            {
                string mensaje = "Error al listar" + ex;
                throw new Exception(mensaje);
            }
        }

        public async Task<ActionResult> list()
        {
            RI_Users user = (RI_Users)Session["User"];

            IEnumerable<RI_Users> olista = null;
            try
            {
                IServicioUsers _servicioUsers = new ServiceUsers();
                IServiceClient _serviceClient = new ServiceClient();
                if (user.Role == 1 || user.Role == 2)
                {
                    olista = await _servicioUsers.GetUsersAsync();

                }
                else
                {
                    if (user.Role == 3 )
                    {

                        olista = await _servicioUsers.GetUsersByCustomerAsync(user.CustomerID);

                    }
                    else if(user.Role==4)
                    {
                        var userResult= await _servicioUsers.GetUserByIdAsync(user.ID);
                        Rl_Robot robot = null;
                        robot = await _serviceClient.GetRobotClientByIdAsync(user.CustomerID);
                        ViewBag.UserName = robot.UserName;
                        olista  = new List<RI_Users> {userResult}; 
                        //return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "No tiene permiso para realizar esta acción");
                    }
                 
                }

                ViewBag.Role = user.Role;
                return View(olista);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<JsonResult> Save(RI_Users ri_users)
        {
            IServicioUsers _servicioUsers = new ServiceUsers();      

            try
            {
                RI_Users user = (RI_Users)Session["User"];
               
                if (user.Role == 1 || user.Role == 2 || user.Role == 3)
                {
                    
                     await _servicioUsers.SaveAsync(ri_users);
                    return Json(new { success = true, message = "Registro guardado con éxito" });
                }
                else
                {
                    return Json(new { success = false, message = "No tienes permisos para realizar esta acción" });
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error al guardar el registro: " + ex.Message;
                return Json(new { success = false, message = errorMessage });
            }

        }
        public async Task<JsonResult> DeleteUser(int id)
        {

            IServicioUsers _servicioUsers = new ServiceUsers();
            RI_Users userInSession = (RI_Users)Session["User"];
            ViewBag.Role = userInSession.Role;
            try
            {
                if(userInSession.Role != 1)
                {
                    return Json(new { success = false, message = "No tienes permisos para realizar esta acción" });
                }
                await _servicioUsers.DeleteAsync(id);
                return Json(new { success = true, message = "Eliminado correctamente" });
            }
            catch (Exception ex)
            {
                string errorMessage = "Error al eliminar: " + ex.Message;
                return Json(new { success = false, message = errorMessage });
            }

        }
    }
}