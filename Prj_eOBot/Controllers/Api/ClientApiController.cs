using Prj_AplicationCore;
using Prj_AplicationCore.Service;
using Prj_eOBot.Models;
using Prj_Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Services.Description;

namespace Prj_eOBot.Controllers.Api
{
    [RoutePrefix("api/Client")]
    public class ClientApiController : ApiController
    {
        [HttpGet]
        [Route("byId")]
        public async Task<IHttpActionResult> getById(Guid? id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceClient service = new ServiceClient();

                Rl_Robot ri_robot = await service.GetRobotClientByIdAsync(id);

                if (ri_robot == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Usuario no encontrado verifique el id ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Usuario encontrado";
                    response.Data = ri_robot;
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

        [HttpGet]
        [Route("GetAllClient")]
        public async Task<IHttpActionResult> getAll(string usermail, string userPassword)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceClient service = new ServiceClient();
                IServicioUsers serviceUsers = new ServiceUsers();

                RI_Users Users = await serviceUsers.Login(usermail, userPassword);

                if (Users == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Credenciales erroneas";

                }
                else 
                {
                    IEnumerable<Rl_Robot> ri_robot = await service.GetRobotClientAsync();


                    if (ri_robot == null || !ri_robot.Any())
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Message = "Erro de data ";

                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Message = "Data encontrado";
                        response.Data = ri_robot;
                    }
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
        
        [HttpGet]
        [Route("GetByDateSubscribe")]
        public async Task<IHttpActionResult> GetClientDataByDateSubscribeAsync(string usermail, string userPassword)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServiceClient service = new ServiceClient();
                IServicioUsers serviceUsers = new ServiceUsers();

                RI_Users Users = await serviceUsers.Login(usermail, userPassword);

                if (Users == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Credenciales erroneas";

                }
                else if(Users.Role == 1)
                {
                    IEnumerable<Rl_Robot> ri_robot = await service.GetClientDataByDateSubscribeAsync();


                    if (ri_robot == null || !ri_robot.Any())
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Message = "Erro de data ";

                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Message = "Data encontrado";
                        response.Data = ri_robot;
                    }
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Usuario no autorizado";
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

        [HttpGet]
        [Route("GetClient")]
        public async Task<IHttpActionResult> getClientByUserByPass(string usermail, string userPassword)
        {
            ResponseModel response = new ResponseModel();
            try
            {

                IServiceClient service = new ServiceClient();
                IServicioUsers serviceUsers = new ServiceUsers();

                RI_Users Users = await serviceUsers.Login(usermail, userPassword);              

                if (Users == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Credenciales erroneas";

                }   
                else
                {
                    Rl_Robot ri_robot = await service.GetRobotClientByEmaildAsync(Users.Email);

                    if (ri_robot == null)
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Message = "Cliente no encontrado ";
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Message = "Cliente  encontrado";
                        response.Data = ri_robot;

                    }
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
