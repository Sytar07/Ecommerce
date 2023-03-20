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
using System.IO;

namespace DAL
{
    public class ConexionesBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=ROCINANTE\\SQLEXPRESS;Initial Catalog=DEV_MARKET_3;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        public EntityConexiones GETALLCONEXIONES()
        {
            EntityConexiones entityConexiones = new EntityConexiones();

            // declaro la conexion a BBDD
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                // Abro la conexion
                connection.Open();
                // Declaro una transaccion
                try
                {
                    SqlCommand command = new SqlCommand("GET_CONEXIONES", connection);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        entityConexiones.lista.Add(new EntityConexion
                        {
                            ididentifier_i = (int)reader["ID_USER"],
                            ID_Carrito_i = (int)reader["ID_Carrito"],
                            FechaInicio_dt = (DateTime)reader["Fecha_Inicio"],
                            ip = reader["ip"].ToString(),
                        });

                        Console.WriteLine((int)reader["ID_CONEXION"]);
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

            return entityConexiones;
        }
        public EntityConexion GETCONEXION(int id_conexion)
        {
            EntityConexion entityConexion = new EntityConexion();

            // declaro la conexion a BBDD
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {

                // Abro la conexion
                connection.Open();
                // Declaro una transaccion
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    // Declaro un COMANDO para ejecutar un PROCEDIMIENTO ALMACENADO
                    SqlCommand command = new SqlCommand("GET_CONEXION", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id_conexion;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // parametro SIMPLE
                    //command.Parameters.Add("@action", System.Data.SqlDbType.VarChar).Value = "GET";                    
                    // EJECUTO EL COMANDO
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado los usuarios encontrados a la lista de entidades
                        entityConexion.ididentifier_i = (int)reader["ID_USER"];
                        entityConexion.ID_Carrito_i = (int)reader["ID_CARRITO"];
                        entityConexion.FechaInicio_dt = (DateTime)reader["FECHA_INICIO"];
                        entityConexion.ip = reader["IP"].ToString();

                     // Aqui falta el correo y los campos adicionales.

                        Console.WriteLine((int)reader["ID_CONEXION"]);
                    }
                    // Cierro el READER
                    reader.Close();


                }
                catch (Exception ex)
                {
                    // Ha fallado ROLLBACK a la transaccion
                    sqlTransaction.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    // SI funciona la transaccion le damos adelante. COMMIT
                    sqlTransaction.Commit();
                }

            }

            return entityConexion;
        }
        public int DELETECONEXION(EntityConexion entityConexion)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();

                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_CONEXION", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_USER", System.Data.SqlDbType.Int).Value = entityConexion.ididentifier_i;
                    command.Parameters.Add("@ID_CARRITO", System.Data.SqlDbType.NVarChar).Value = entityConexion.ID_Carrito_i;
                    command.Parameters.Add("@FECHA_INICIO", System.Data.SqlDbType.NVarChar).Value = entityConexion.FechaInicio_dt;
                    command.Parameters.Add("@IP", System.Data.SqlDbType.Int).Value = entityConexion.ip;

                    command.Parameters.Add("@delete", System.Data.SqlDbType.SmallInt).Value = 1;

                    command.Parameters.Add("@ID_RETURN", System.Data.SqlDbType.Int).Value = 0;
                    command.Parameters["@ID_RETURN"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    salida = int.Parse(command.Parameters["@ID_RETURN"].Value.ToString());

                }

                catch (Exception ex)
                {
                    // Ha fallado ROLLBACK a la transaccion
                    sqlTransaction.Rollback();
                    throw new Exception(ex.Message);
                    return -1;
                }
                finally
                {
                    // SI funciona la transaccion le damos adelante. COMMIT
                    sqlTransaction.Commit();
                }
            }

            return salida;
        }
        public int INSERTCONEXION(EntityConexion entityConexion)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_CONEXION", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_USER", System.Data.SqlDbType.Int).Value = entityConexion.ididentifier_i;
                    command.Parameters.Add("@ID_CARRITO", System.Data.SqlDbType.NVarChar).Value = entityConexion.ID_Carrito_i;
                    command.Parameters.Add("@FECHA_INICIO", System.Data.SqlDbType.NVarChar).Value = entityConexion.FechaInicio_dt;
                    command.Parameters.Add("@IP", System.Data.SqlDbType.Int).Value = entityConexion.ip;

                    command.Parameters.Add("@delete", System.Data.SqlDbType.SmallInt).Value = 0;

                    command.Parameters.Add("@ID_RETURN", System.Data.SqlDbType.Int).Value = 0;
                    command.Parameters["@ID_RETURN"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    salida = int.Parse(command.Parameters["@ID_RETURN"].Value.ToString());

                }

                catch (Exception ex)
                {
                    // Ha fallado ROLLBACK a la transaccion
                    sqlTransaction.Rollback();
                    throw new Exception(ex.Message);
                    return -1;
                }
                finally
                {
                    // SI funciona la transaccion le damos adelante. COMMIT
                    sqlTransaction.Commit();
                }
            }

            return salida;
        }
        public int UPDATECONEXION(EntityConexion entityConexion)
        {
            
            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_CONEXION", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_USER", System.Data.SqlDbType.Int).Value = entityConexion.ididentifier_i;
                    command.Parameters.Add("@ID_CARRITO", System.Data.SqlDbType.NVarChar).Value = entityConexion.ID_Carrito_i;
                    command.Parameters.Add("@FECHA_INICIO", System.Data.SqlDbType.NVarChar).Value = entityConexion.FechaInicio_dt;
                    command.Parameters.Add("@IP", System.Data.SqlDbType.Int).Value = entityConexion.ip;

                    command.Parameters.Add("@delete", System.Data.SqlDbType.SmallInt).Value = 0;

                    command.Parameters.Add("@ID_RETURN", System.Data.SqlDbType.Int).Value = 0;
                    command.Parameters["@ID_RETURN"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    salida = int.Parse(command.Parameters["@ID_RETURN"].Value.ToString());

                }

                catch (Exception ex)
                {
                    // Ha fallado ROLLBACK a la transaccion
                    sqlTransaction.Rollback();
                    throw new Exception(ex.Message);
                    return -1;
                }
                finally
                {
                    // SI funciona la transaccion le damos adelante. COMMIT
                    sqlTransaction.Commit();
                }
            }
           
            return salida;
        }
    }
}
