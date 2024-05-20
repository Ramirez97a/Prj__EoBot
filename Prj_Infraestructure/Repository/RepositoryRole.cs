using prj_Infraestructure;
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
    public class RepositoryRole : IRepositoryRole
    {
        public async Task<IEnumerable<RI_Role>> GetRoleAsync()
        {
            List<RI_Role> olista = new List<RI_Role>();
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var connectionString = ctx.Database.Connection.ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand("Sp_GetRoles", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    RI_Role oRI_Role = new RI_Role();

                                    oRI_Role.ID = Convert.ToInt32(reader["ID"]);
                                    oRI_Role.Description = reader["Description"].ToString();
                                    
                                    olista.Add(oRI_Role);
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
