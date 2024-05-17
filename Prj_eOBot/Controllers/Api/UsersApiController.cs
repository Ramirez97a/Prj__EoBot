using Prj_AplicationCore;
using Prj_eOBot.Models;
using Prj_Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Prj_eOBot.Controllers.Api
{
    [RoutePrefix("api/Users")]
    public class UsersApiController : ApiController
    {
        [HttpGet]
        [Route("byId")]
        public async Task<IHttpActionResult> getById(int id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServicioUsers service = new ServiceUsers();

                RI_Users users = await service.GetUserByIdAsync(id);

                if (users == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Usuario no encontrado verifique el id ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Usuario encontrado";
                    response.Data = users;
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
        [Route("GetAllUser")]
        public async Task<IHttpActionResult> getAll()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServicioUsers service = new ServiceUsers();

                IEnumerable<RI_Users> Users = await service.GetUsersAsync();

                if (Users == null || !Users.Any())
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = "Usuario no encontrado verifique el id ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Usuario encontrado";
                    response.Data = Users;
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

        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RI_Users user)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                IServicioUsers service = new ServiceUsers();

                RI_Users Users = await service.SaveAsync(user);

                if (Users == null)
                {
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = "Usuario no registrado ";

                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Usuario resgistrado";                 

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

        [HttpPost]
        [Route("login")]

        public async Task<IHttpActionResult> Login(string usermail, string userPassword)
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
