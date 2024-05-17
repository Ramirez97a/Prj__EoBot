using prj_Infraestructure;
using prj_Infraestructure.Repositorys;
using Prj_Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_Infraestructure.Repository
{
    public class RepositoryClient : IRepositoryClient
    {
        public async Task DeleteAsync(Guid? id)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var connectionString = ctx.Database.Connection.ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand("Sp_deleteRobotClient", connection))
                        {
                            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;
                            cmd.CommandType = CommandType.StoredProcedure;

                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error al eliminar: " + dbEx.Message;
                throw new Exception(mensaje, dbEx);
            }
            catch (Exception ex)
            {
                string mensaje = "Error al eliminar: " + ex.Message;
                throw new Exception(mensaje, ex);
            }
        }

        public async Task<IEnumerable<Rl_Robot>> GetClientAsync()
        {
            List<Rl_Robot> olista = new List<Rl_Robot>();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var connectionString = ctx.Database.Connection.ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand("Sp_GetRobotClient", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    Rl_Robot oRl_Robot = new Rl_Robot();

                                    oRl_Robot.ID = (Guid)reader["ID"];
                                    oRl_Robot.UserWeb = reader["UserWeb"].ToString();
                                    oRl_Robot.PasswordWeb = reader["PasswordWeb"].ToString();
                                    oRl_Robot.UserName = reader["UserName"].ToString();
                                    oRl_Robot.Password = reader["Password"].ToString();
                                    oRl_Robot.LoginUrl = reader["LoginUrl"].ToString();
                                    oRl_Robot.NodeName = reader["NodeName"].ToString();
                                    oRl_Robot.TimeGenReports = (DateTime)reader["TimeGenReports"];
                                    oRl_Robot.TimeDownReports = (DateTime)reader["TimeDownReports"];
                                    oRl_Robot.Timespan = (int)reader["Timespan"];
                                    oRl_Robot.LocalFolder = reader["LocalFolder"].ToString();
                                    oRl_Robot.Message = reader["Message"].ToString();
                                    oRl_Robot.StatusClient = reader["StatusClient"] != DBNull.Value ? (int)reader["StatusClient"] : (int?)null;
                                    oRl_Robot.CodError = reader["CodError"] != DBNull.Value ? (int)reader["CodError"] : (int?)null;
                                    oRl_Robot.CodRequest = reader["CodRequest"] != DBNull.Value ? (int)reader["CodRequest"] : (int?)null;
                                    oRl_Robot.DateSubscribe = reader["DateSubscribe"] != DBNull.Value ? (DateTime?)reader["DateSubscribe"] : null;
                                    oRl_Robot.FechaHoraUltimoReporte = reader["FechaHoraUltimoReporte"] != DBNull.Value ? (DateTime?)reader["FechaHoraUltimoReporte"] : null;
                                    oRl_Robot.EmailNotificaciones = reader["EmailNotificaciones"].ToString();
                                    oRl_Robot.Token = reader["Token"].ToString();
                                    olista.Add(oRl_Robot);
                                }
                            }
                        }
                    }
                }
                return olista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error en la base de datos: \n" + dbEx.Message;
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "Error en el servidor: \n" + ex.Message;
                throw;
            }
        }

        public async Task<Rl_Robot> GetRobotClientByIdAsync(Guid? id)
        {
            Rl_Robot ori_robot = new Rl_Robot();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var connectionString = ctx.Database.Connection.ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlCommand cmd = new SqlCommand("Sp_Get_Rl_RobotClientByID", connection))
                        {
                            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                            {
                                while (await reader.ReadAsync().ConfigureAwait(false))
                                {
                                    ori_robot.ID = (Guid)reader["ID"];
                                    ori_robot.UserWeb = reader["UserWeb"].ToString();
                                    ori_robot.PasswordWeb = reader["PasswordWeb"].ToString();
                                    ori_robot.UserName = reader["UserName"].ToString();
                                    ori_robot.Password = reader["Password"].ToString();
                                    ori_robot.LoginUrl = reader["LoginUrl"].ToString();
                                    ori_robot.NodeName = reader["NodeName"].ToString();
                                    ori_robot.TimeGenReports = (DateTime)reader["TimeGenReports"];
                                    ori_robot.TimeDownReports = (DateTime)reader["TimeDownReports"];
                                    ori_robot.Timespan = (int)reader["Timespan"];
                                    ori_robot.LocalFolder = reader["LocalFolder"].ToString();
                                    ori_robot.Message = reader["Message"].ToString();
                                    ori_robot.StatusClient = reader["StatusClient"] != DBNull.Value ? (int)reader["StatusClient"] : (int?)null;
                                    ori_robot.CodError = reader["CodError"] != DBNull.Value ? (int)reader["CodError"] : (int?)null;
                                    ori_robot.CodRequest = reader["CodRequest"] != DBNull.Value ? (int)reader["CodRequest"] : (int?)null;
                                    ori_robot.DateSubscribe = reader["DateSubscribe"] != DBNull.Value ? (DateTime?)reader["DateSubscribe"] : null;
                                    ori_robot.FechaHoraUltimoReporte = reader["FechaHoraUltimoReporte"] != DBNull.Value ? (DateTime?)reader["FechaHoraUltimoReporte"] : null;
                                    ori_robot.EmailNotificaciones = reader["EmailNotificaciones"].ToString();
                                    ori_robot.Token = reader["Token"].ToString();
                                }
                            }
                        }
                    }
                }
                return ori_robot;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error en la base de datos" + dbEx;
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "Error al guardar" + ex;
                throw new Exception(mensaje);
            }
        }

        public async Task<Rl_Robot> GetRobotClientByEmaildAsync(string email)
        {
            Rl_Robot ori_robot = new Rl_Robot();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var connectionString = ctx.Database.Connection.ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlCommand cmd = new SqlCommand("Sp_GetRobotClientByEmail", connection))
                        {
                            cmd.Parameters.Add("@email", SqlDbType.VarChar,50).Value = email;
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                            {
                                while (await reader.ReadAsync().ConfigureAwait(false))
                                {
                                    ori_robot.ID = (Guid)reader["ID"];                                  
                                    ori_robot.UserName = reader["UserName"].ToString();
                                    ori_robot.Password = reader["Password"].ToString();
                                    ori_robot.LoginUrl = reader["LoginUrl"].ToString();
                                    ori_robot.NodeName = reader["NodeName"].ToString();
                                    ori_robot.TimeGenReports = (DateTime)reader["TimeGenReports"];
                                    ori_robot.TimeDownReports = (DateTime)reader["TimeDownReports"];
                                    ori_robot.Timespan = (int)reader["Timespan"];
                                    ori_robot.LocalFolder = reader["LocalFolder"].ToString();
                                    ori_robot.Message = reader["Message"].ToString();
                                    ori_robot.StatusClient = reader["StatusClient"] != DBNull.Value ? (int)reader["StatusClient"] : (int?)null;
                                    ori_robot.CodError = reader["CodError"] != DBNull.Value ? (int)reader["CodError"] : (int?)null;
                                    ori_robot.CodRequest = reader["CodRequest"] != DBNull.Value ? (int)reader["CodRequest"] : (int?)null;
                                    ori_robot.DateSubscribe = reader["DateSubscribe"] != DBNull.Value ? (DateTime?)reader["DateSubscribe"] : null;
                                    ori_robot.FechaHoraUltimoReporte = reader["FechaHoraUltimoReporte"] != DBNull.Value ? (DateTime?)reader["FechaHoraUltimoReporte"] : null;
                                    ori_robot.EmailNotificaciones = reader["EmailNotificaciones"].ToString();
                                    ori_robot.Token = reader["Token"].ToString();
                                }
                            }
                        }
                    }
                }
                return ori_robot;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error en la base de datos" + dbEx;
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "Error al guardar" + ex;
                throw new Exception(mensaje);
            }
        }

        public async Task<Rl_Robot> SaveAsync(Rl_Robot ri_robot)
        {
            Rl_Robot ori_robot = null;

            try
            {
               
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ori_robot = await GetRobotClientByIdAsync(ri_robot.ID);

                    var connectionString = ctx.Database.Connection.ConnectionString;
                    SqlConnection connection = new SqlConnection(connectionString);
                    await connection.OpenAsync();
                    if (ori_robot.ID == Guid.Empty)
                    {
                        using (SqlCommand cmd = new SqlCommand("Sp_Create_Rl_RobotClient", connection))
                        {
                            cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = ri_robot.UserName;
                            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = ri_robot.Password;
                            cmd.Parameters.Add("@LoginUrl", SqlDbType.VarChar, 512).Value = ri_robot.LoginUrl;
                            cmd.Parameters.Add("@NodeName", SqlDbType.VarChar, 50).Value = ri_robot.NodeName;
                            cmd.Parameters.Add("@TimeGenReports", SqlDbType.DateTime).Value = ri_robot.TimeGenReports;
                            cmd.Parameters.Add("@TimeDownReports", SqlDbType.DateTime).Value = ri_robot.TimeDownReports;
                            cmd.Parameters.Add("@Timespan", SqlDbType.Int).Value = ri_robot.Timespan;
                            cmd.Parameters.Add("@LocalFolder", SqlDbType.VarChar, 512).Value = ri_robot.LocalFolder;
                            cmd.Parameters.Add("@StatusClient", SqlDbType.Int).Value = ri_robot.StatusClient;
                            cmd.Parameters.Add("@EmailNotificaciones", SqlDbType.VarChar, 100).Value = ri_robot.EmailNotificaciones;
                            cmd.Parameters.Add("@Token", SqlDbType.VarChar, 600).Value = ri_robot.Token;
                            cmd.Parameters.Add("@DateSubscribe", SqlDbType.Date).Value = ri_robot.DateSubscribe;

                            cmd.CommandType = CommandType.StoredProcedure;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("Update_Rl_RobotClient", connection))
                        {
                            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = ri_robot.ID;
                            cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = ri_robot.UserName;
                            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = ri_robot.Password;
                            cmd.Parameters.Add("@LoginUrl", SqlDbType.VarChar, 512).Value = ri_robot.LoginUrl;
                            cmd.Parameters.Add("@NodeName", SqlDbType.VarChar, 50).Value = ri_robot.NodeName;
                            cmd.Parameters.Add("@TimeGenReports", SqlDbType.DateTime).Value = ri_robot.TimeGenReports;
                            cmd.Parameters.Add("@TimeDownReports", SqlDbType.DateTime).Value = ri_robot.TimeDownReports;
                            cmd.Parameters.Add("@Timespan", SqlDbType.Int).Value = ri_robot.Timespan;
                            cmd.Parameters.Add("@LocalFolder", SqlDbType.VarChar, 512).Value = ri_robot.LocalFolder;
                            cmd.Parameters.Add("@StatusClient", SqlDbType.Int).Value = ri_robot.StatusClient;
                            cmd.Parameters.Add("@EmailNotificaciones", SqlDbType.VarChar, 100).Value = ri_robot.EmailNotificaciones;
                            cmd.Parameters.Add("@Token", SqlDbType.VarChar, 600).Value = ri_robot.Token;
                            cmd.Parameters.Add("@DateSubscribe", SqlDbType.Date).Value = ri_robot.DateSubscribe;
                            cmd.CommandType = CommandType.StoredProcedure;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }
                return ori_robot;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error al guardar" + dbEx;
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "Error al guardar" + ex;
                throw new Exception(mensaje);
            }
        }

        public async Task<IEnumerable<Rl_Robot>> GetClientDataByDateSubscribeAsync()
        {
            List<Rl_Robot> olista = new List<Rl_Robot>();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var connectionString = ctx.Database.Connection.ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand("GetRlRobotDataByDateSubscribe", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    Rl_Robot oRl_Robot = new Rl_Robot();

                                    oRl_Robot.ID = (Guid)reader["ID"];
                                    oRl_Robot.UserWeb = reader["UserWeb"].ToString();
                                    oRl_Robot.PasswordWeb = reader["PasswordWeb"].ToString();
                                    oRl_Robot.UserName = reader["UserName"].ToString();
                                    oRl_Robot.Password = reader["Password"].ToString();
                                    oRl_Robot.LoginUrl = reader["LoginUrl"].ToString();
                                    oRl_Robot.NodeName = reader["NodeName"].ToString();
                                    oRl_Robot.TimeGenReports = (DateTime)reader["TimeGenReports"];
                                    oRl_Robot.TimeDownReports = (DateTime)reader["TimeDownReports"];
                                    oRl_Robot.Timespan = (int)reader["Timespan"];
                                    oRl_Robot.LocalFolder = reader["LocalFolder"].ToString();
                                    oRl_Robot.Message = reader["Message"].ToString();
                                    oRl_Robot.StatusClient = reader["StatusClient"] != DBNull.Value ? (int)reader["StatusClient"] : (int?)null;
                                    oRl_Robot.CodError = reader["CodError"] != DBNull.Value ? (int)reader["CodError"] : (int?)null;
                                    oRl_Robot.CodRequest = reader["CodRequest"] != DBNull.Value ? (int)reader["CodRequest"] : (int?)null;
                                    oRl_Robot.DateSubscribe = reader["DateSubscribe"] != DBNull.Value ? (DateTime?)reader["DateSubscribe"] : null;
                                    oRl_Robot.FechaHoraUltimoReporte = reader["FechaHoraUltimoReporte"] != DBNull.Value ? (DateTime?)reader["FechaHoraUltimoReporte"] : null;
                                    oRl_Robot.EmailNotificaciones = reader["EmailNotificaciones"].ToString();
                                    oRl_Robot.Token = reader["Token"].ToString();
                                    olista.Add(oRl_Robot);
                                }
                            }
                        }
                    }
                }
                return olista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error en la base de datos: \n" + dbEx.Message;
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "Error en el servidor: \n" + ex.Message;
                throw;
            }
        }


    }
}
