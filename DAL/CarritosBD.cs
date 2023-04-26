using Microsoft.Data.SqlClient;
using Microsoft;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ECOMMERCE.CORE;

namespace DAL
{
    public class CarritoBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=ROCINANTE\\SQLEXPRESS;Initial Catalog=DEV_MARKET_3;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

      

        public IEnumerable<EntityCarrito> GETCARRITO(int idconexion)
        {
            List<EntityCarrito> entityCarritos = new List<EntityCarrito>();

            // declaro la conexion a BBDD
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                // Abro la conexion
                connection.Open();
                // Declaro una transaccion
                try
                {
                    SqlCommand command = new SqlCommand("GET_CARRITO", connection);
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = idconexion;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado las líneas de carrito encontrados a la lista de entidades
                        entityCarritos.Add(new EntityCarrito
                        {
                            idconexion = (int)reader["ID_CONEXION"],
                            idproducto = (int)reader["ID_PRODUCTO"],
                            nombre_nv = (string)reader["NOMBRE_PRODUCTO"],

                            cantidad_i = (int)reader["CANTIDAD"],
                            precio_f = (decimal)reader["PRECIO"],

                        });

                        
                    }
                    // Cierro el READER
                    reader.Close();


                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {

                }

            }

            return entityCarritos;
        }
        public int INSERT_PRODUCTO(int idconexion, int idproducto, int cantidad)
        {
            List<EntityCarrito> entityCarritos = new List<EntityCarrito>();

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                // Abro la conexion
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("AGREGAR_CARRITO", connection);
                    command.Transaction = sqlTransaction;

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = idconexion;
                    command.Parameters.Add("@ID_PRODUCTO", System.Data.SqlDbType.NVarChar).Value = idproducto;
                    command.Parameters.Add("@Cantidad", System.Data.SqlDbType.SmallInt).Value = cantidad;

                    command.Parameters.Add("@ID_RETURN", System.Data.SqlDbType.Int).Value = 0;
                    command.Parameters["@ID_RETURN"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    salida = int.Parse(command.Parameters["@ID_RETURN"].Value.ToString());

                }

                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    throw new Exception(ex.Message);

                }
                finally
                {
                    sqlTransaction.Commit();
                }
                return salida;
            }
    }
        public int DELETE_PRODUCTO(int idconexion, int idproducto, int cantidad)
        {
            List<EntityCarrito> entityCarritos = new List<EntityCarrito>();

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                // Abro la conexion
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("QUITAR_CARRITO", connection);
                    command.Transaction = sqlTransaction;

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = idconexion;
                    command.Parameters.Add("@ID_PRODUCTO", System.Data.SqlDbType.NVarChar).Value = idproducto;

                    command.Parameters.Add("@ID_RETURN", System.Data.SqlDbType.Int).Value = 0;
                    command.Parameters["@ID_RETURN"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    salida = int.Parse(command.Parameters["@ID_RETURN"].Value.ToString());

                }

                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    throw new Exception(ex.Message);

                }
                finally
                {
                    sqlTransaction.Commit();
                }
                return salida;
            }

            
        }
    }
}
