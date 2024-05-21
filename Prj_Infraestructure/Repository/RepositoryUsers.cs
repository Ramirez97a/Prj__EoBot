using Prj_Infraestructure.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prj_Infraestructure.Repositorys
{
    public class RepositoryUsers : IRepositoryUsers
    {
        public async Task<IEnumerable<RI_Users>> GetUsersAsync()
        {
            List<RI_Users> olista = new List<RI_Users>();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var connectionString = ctx.Database.Connection.ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand("Sp_GetUsers", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    RI_Users oRI_Users = new RI_Users();

                                    oRI_Users.ID = Convert.ToInt32(reader["ID"]);
                                    oRI_Users.Name = reader["Name"].ToString();
                                    oRI_Users.Surname = reader["Surname"].ToString();
                                    oRI_Users.Email = reader["Email"].ToString();
                                    oRI_Users.UserName = reader["UserName"].ToString();
                                    oRI_Users.Password = reader["Password"].ToString();
                                    oRI_Users.Role = reader["Role"] != DBNull.Value ? (int)reader["Role"] : (int?)null;
                                    oRI_Users.Status = reader["Status"] != DBNull.Value ? (int)reader["Status"] : (int?)null;
                                    oRI_Users.CustomerID = reader["CustomerID"] != DBNull.Value ? (Guid)reader["CustomerID"] : (Guid?)null;

                                    olista.Add(oRI_Users);
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
      

        public async Task<RI_Users> Login(string userName, string userPassword)
        {
            try
            {
                RI_Users user = null;

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    using (var connection = new SqlConnection(ctx.Database.Connection.ConnectionString))
                    {
                        await connection.OpenAsync();

                        using (var command = new SqlCommand("Sp_GetUserAutenticacion", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@userName", userName);
                            command.Parameters.AddWithValue("@userPassword", userPassword);

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    user = new RI_Users
                                    {
                                        ID = Convert.ToInt32(reader["ID"]),
                                        Name = reader["Name"].ToString(),
                                        Surname = reader["Surname"].ToString(),
                                        Email = reader["Email"].ToString(),
                                        UserName = reader["UserName"].ToString(),
                                        Password = reader["Password"].ToString(),
                                        Role = reader["Role"] != DBNull.Value ? Convert.ToInt32(reader["Role"]) : (int?)null,
                                        Status = reader["Status"] != DBNull.Value ? Convert.ToInt32(reader["Status"]) : (int?)null,
                                        CustomerID = reader["CustomerID"] != DBNull.Value ? (Guid)reader["CustomerID"] : (Guid?)null

                                    };
                                }
                            }
                        }
                    }
                }

                return user;
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 4060) 
            {
                throw new Exception("Error de conexión a la base de datos. Por favor, inténtalo de nuevo más tarde.");
            }
            catch (SqlException sqlEx) 
            {
                throw new Exception($"Error de SQL: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servidor: {ex.Message}");
            }
        }

        public async Task<RI_Users> SaveAsync(RI_Users ri_Users)
        {
            RI_Users ori_Users = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ori_Users = await GetUserByIdAsync(ri_Users.ID);

                    var connectionString = ctx.Database.Connection.ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        if (ori_Users.ID == 0 )
                        {
                            using (SqlCommand cmd = new SqlCommand("Sp_CreateUser", connection))
                            {
                                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = ri_Users.Name;
                                cmd.Parameters.Add("@Surname", SqlDbType.VarChar, 50).Value = ri_Users.Surname;
                                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = ri_Users.Email;
                                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = ri_Users.UserName;
                                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = ri_Users.Password;
                                cmd.Parameters.Add("@Role", SqlDbType.Int).Value = ri_Users.Role;
                                cmd.Parameters.Add("@CustomerID", SqlDbType.UniqueIdentifier).Value = ri_Users.CustomerID.HasValue ? (object)ri_Users.CustomerID.Value : DBNull.Value;
                                cmd.CommandType = CommandType.StoredProcedure;
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                        else
                        {
                            using (SqlCommand cmd = new SqlCommand("Sp_UpdateUser", connection))
                            {
                                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ri_Users.ID;
                                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = ri_Users.Name;
                                cmd.Parameters.Add("@Surname", SqlDbType.VarChar, 50).Value = ri_Users.Surname;
                                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = ri_Users.Email;
                                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = ri_Users.UserName;
                                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = ri_Users.Password;
                                cmd.Parameters.Add("@Role", SqlDbType.Int).Value = ri_Users.Role;
                                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = ri_Users.Status;
                                cmd.Parameters.Add("@CustomerID", SqlDbType.UniqueIdentifier).Value = ri_Users.CustomerID;
                                cmd.CommandType = CommandType.StoredProcedure;
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                    }
                }
                return ori_Users;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error al guardar: " + dbEx.Message;
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "Error al guardar: " + ex.Message;
                throw new Exception(mensaje);
            }
        }

        public async Task<RI_Users> GetUserByIdAsync(int? id)
        {
            RI_Users oRI_Users = new RI_Users();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var connectionString = ctx.Database.Connection.ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand("Sp_GetUserByID", connection))
                        {
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    oRI_Users.ID = Convert.ToInt32(reader["ID"]);
                                    oRI_Users.Name = reader["Name"].ToString();
                                    oRI_Users.Surname = reader["Surname"].ToString();
                                    oRI_Users.Email = reader["Email"].ToString();
                                    oRI_Users.UserName = reader["UserName"].ToString();
                                    oRI_Users.Password = reader["Password"].ToString();
                                    oRI_Users.Role = reader["Role"] != DBNull.Value ? (int)reader["Role"] : (int?)null;
                                    oRI_Users.Status = reader["Status"] != DBNull.Value ? (int)reader["Status"] : (int?)null;
                                    oRI_Users.CustomerID = reader["CustomerID"] != DBNull.Value ? (Guid)reader["CustomerID"] : (Guid?)null;

                                }
                            }
                        }
                    }
                }
                return oRI_Users;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error en la base de datos: " + dbEx.Message;
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "Error al obtener el usuario: " + ex.Message;
                throw new Exception(mensaje);
            }
        }
        public async Task<RI_Users> GetUserByCustomerAsync(Guid? customerID)
        {
            RI_Users oRI_Users = new RI_Users();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var connectionString = ctx.Database.Connection.ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand("Sp_GetUserByCustomerID", connection))
                        {
                            cmd.Parameters.Add("@CustomerID", SqlDbType.UniqueIdentifier).Value = customerID;
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    oRI_Users.ID = Convert.ToInt32(reader["ID"]);
                                    oRI_Users.Name = reader["Name"].ToString();
                                    oRI_Users.Surname = reader["Surname"].ToString();
                                    oRI_Users.Email = reader["Email"].ToString();
                                    oRI_Users.UserName = reader["UserName"].ToString();
                                    oRI_Users.Password = reader["Password"].ToString();
                                    oRI_Users.Role = reader["Role"] != DBNull.Value ? (int)reader["Role"] : (int?)null;
                                    oRI_Users.Status = reader["Status"] != DBNull.Value ? (int)reader["Status"] : (int?)null;
                                    oRI_Users.CustomerID = reader["CustomerID"] != DBNull.Value ? (Guid)reader["CustomerID"] : (Guid?)null;

                                }
                            }
                        }
                    }
                }
                return oRI_Users;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error en la base de datos: " + dbEx.Message;
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "Error al obtener el usuario: " + ex.Message;
                throw new Exception(mensaje);
            }
        }

        public async Task<IEnumerable<RI_Users>> GetUsersByCustomerAsync(Guid? customerID)
        {
            List<RI_Users> olista = new List<RI_Users>();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var connectionString = ctx.Database.Connection.ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand("Sp_GetUserByCustomerID", connection))
                        {
                            cmd.Parameters.Add("@CustomerID", SqlDbType.UniqueIdentifier).Value = customerID;
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    RI_Users oRI_Users = new RI_Users();

                                    oRI_Users.ID = Convert.ToInt32(reader["ID"]);
                                    oRI_Users.Name = reader["Name"].ToString();
                                    oRI_Users.Surname = reader["Surname"].ToString();
                                    oRI_Users.Email = reader["Email"].ToString();
                                    oRI_Users.UserName = reader["UserName"].ToString();
                                    oRI_Users.Password = reader["Password"].ToString();
                                    oRI_Users.Role = reader["Role"] != DBNull.Value ? (int)reader["Role"] : (int?)null;
                                    oRI_Users.Status = reader["Status"] != DBNull.Value ? (int)reader["Status"] : (int?)null;
                                    oRI_Users.CustomerID = reader["CustomerID"] != DBNull.Value ? (Guid)reader["CustomerID"] : (Guid?)null;

                                    olista.Add(oRI_Users);
                                }
                            }
                        }
                    }
                }
                return olista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error en la base de datos: " + dbEx.Message;
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "Error al obtener el usuario: " + ex.Message;
                throw new Exception(mensaje);
            }
        }

        public async Task DeleteAsync(int id)
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

                        using (SqlCommand cmd = new SqlCommand("Sp_DeleteUser", connection))
                        {
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                            cmd.CommandType = CommandType.StoredProcedure;
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error al eliminar: " + dbEx.Message;
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "Error al eliminar: " + ex.Message;
                throw new Exception(mensaje);
            }
        }



    }
}
