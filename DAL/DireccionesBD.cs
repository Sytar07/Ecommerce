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
    public class DireccionesBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=ROCINANTE\\SQLEXPRESS;Initial Catalog=DEV_MARKET_3;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public EntityDirecciones GETALLDIRECCIONES(int id_user)
        {
            EntityDirecciones entityDirecciones = new EntityDirecciones();

            // declaro la conexion a BBDD
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                // Abro la conexion
                connection.Open();
                // Declaro una transaccion
                try
                {
                    // Declaro un COMANDO para ejecutar un PROCEDIMIENTO ALMACENADO
                    SqlCommand command = new SqlCommand("GET_DIRECCIONES", connection);
                    command.Parameters.Add("@ID_USER", System.Data.SqlDbType.Int).Value = id_user;

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado las direcciones encontradas a la lista de entidades
                        entityDirecciones.lista.Add(new EntityDireccion
                        {
                            ididentifier_i = (int)reader["ID_DIRECCION"],
                            direccion_nv = (string)reader["DIRECCION"],
                            numero_i = (int)reader["NUMERO"],
                            user_i = (int)reader["ID_USER"],
                            puerta_nv = (string)reader["PUERTA"],
                            ciudad_nv = (string)reader["CIUDAD"],
                            pais_i = (int)reader["PAIS"],

                        });

                        Console.WriteLine((int)reader["ID_DIRECCION"]);
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

            return entityDirecciones;
        }
        public EntityDireccion GETDIRECCION(int id_direccion)
        {
            EntityDireccion entityDireccion = new EntityDireccion();

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
                    SqlCommand command = new SqlCommand("GET_DIRECCION", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id_direccion;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // parametro SIMPLE
                    //command.Parameters.Add("@action", System.Data.SqlDbType.VarChar).Value = "GET";                    
                    // EJECUTO EL COMANDO
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado las direcciones encontradas a la lista de entidades
                        entityDireccion.ididentifier_i = (int)reader["ID_DIRECCION"];
                        entityDireccion.direccion_nv= (string)reader["DIRECCION"];
                        entityDireccion.numero_i = (int)reader["NUMERO"];
                        entityDireccion.user_i= (int)reader["ID_USER"];
                        entityDireccion.puerta_nv = (string)reader["PUERTA"];
                        entityDireccion.ciudad_nv = (string)reader["CIUDAD"];
                        entityDireccion.pais_i = (int)reader["PAIS"];
                        // Aqui falta el correo y los campos adicionales.

                        Console.WriteLine((int)reader["ID_DIRECCION"]);
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

            return entityDireccion;
        }
        public int DELETEDIRECCION(EntityDireccion entityDireccion)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();

                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_DIRECCIONES", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = entityDireccion.ididentifier_i;
                    command.Parameters.Add("@DIRECCION", System.Data.SqlDbType.NVarChar).Value = entityDireccion.direccion_nv;
                    command.Parameters.Add("@NUMERO", System.Data.SqlDbType.Int).Value = entityDireccion.numero_i;
                    command.Parameters.Add("@PUERTA", System.Data.SqlDbType.NVarChar).Value = entityDireccion.puerta_nv;
                    command.Parameters.Add("@USUARIO", System.Data.SqlDbType.NVarChar).Value = entityDireccion.user_i;
                    command.Parameters.Add("@CIUDAD", System.Data.SqlDbType.NVarChar).Value = entityDireccion.ciudad_nv;
                    command.Parameters.Add("@PAIS", System.Data.SqlDbType.NVarChar).Value = entityDireccion.pais_i;


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
        public int INSERTDIRECCION(EntityDireccion entityDireccion)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_DIRECCIONES", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = 0;
                    command.Parameters.Add("@DIRECCION", System.Data.SqlDbType.NVarChar).Value = entityDireccion.direccion_nv;
                    command.Parameters.Add("@NUMERO", System.Data.SqlDbType.Int).Value = entityDireccion.numero_i;
                    command.Parameters.Add("@PUERTA", System.Data.SqlDbType.NVarChar).Value = entityDireccion.puerta_nv;
                    command.Parameters.Add("@USUARIO", System.Data.SqlDbType.NVarChar).Value = entityDireccion.user_i;
                    command.Parameters.Add("@CIUDAD", System.Data.SqlDbType.NVarChar).Value = entityDireccion.ciudad_nv;
                    command.Parameters.Add("@PAIS", System.Data.SqlDbType.NVarChar).Value = entityDireccion.pais_i;


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
        public int UPDATEDIRECCION(EntityDireccion entityDireccion)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_DIRECCIONES", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = entityDireccion.ididentifier_i;
                    command.Parameters.Add("@DIRECCION", System.Data.SqlDbType.NVarChar).Value = entityDireccion.direccion_nv;
                    command.Parameters.Add("@USUARIO", System.Data.SqlDbType.NVarChar).Value = entityDireccion.user_i;
                    command.Parameters.Add("@NUMERO", System.Data.SqlDbType.Int).Value = entityDireccion.numero_i;
                    command.Parameters.Add("@PUERTA", System.Data.SqlDbType.NVarChar).Value = entityDireccion.puerta_nv;
                    command.Parameters.Add("@CIUDAD", System.Data.SqlDbType.NVarChar).Value = entityDireccion.ciudad_nv;
                    command.Parameters.Add("@PAIS", System.Data.SqlDbType.NVarChar).Value = entityDireccion.pais_i;

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
