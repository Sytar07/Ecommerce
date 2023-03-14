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
    public class UsersBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=ROCINANTE\\SQLEXPRESS;Initial Catalog=DEV_MARKET_3;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        public EntityUsers GETALLUSERS()
        {
            EntityUsers entityUsers = new EntityUsers();

            // declaro la conexion a BBDD
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                // Abro la conexion
                connection.Open();
                // Declaro una transaccion
                try
                {
                    SqlCommand command = new SqlCommand("GET_USERS", connection);

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        entityUsers.lista.Add(new EntityUser
                        {
                            ididentifier_i = (int)reader["ID_USER"],
                            name_nv = (string)reader["FULLNAME_NV"],
                            email_nv = (string)reader["EMAIL_NV"],
                            rol_i= (int)reader["ROL"],
                        });

                        Console.WriteLine((int)reader["ID_USER"]);
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

            return entityUsers;
        }
        public EntityUser GETUSER(int id_user)
        {
            EntityUser entityUser = new EntityUser();

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
                    SqlCommand command = new SqlCommand("GET_USER", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id_user;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // parametro SIMPLE
                    //command.Parameters.Add("@action", System.Data.SqlDbType.VarChar).Value = "GET";                    
                    // EJECUTO EL COMANDO
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado los usuarios encontrados a la lista de entidades
                        entityUser.ididentifier_i = (int)reader["ID_USER"];
                        entityUser.email_nv = (string)reader["EMAIL_NV"];
                        entityUser.name_nv = (string)reader["FULLNAME_NV"];
                        // Aqui falta el correo y los campos adicionales.

                        Console.WriteLine((int)reader["ID_USER"]);
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

            return entityUser;
        }
        public int DELETEUSER(EntityUser entityUser)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();

                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_USER", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = entityUser.ididentifier_i;
                    command.Parameters.Add("@FULLNAME_NV", System.Data.SqlDbType.NVarChar).Value = entityUser.name_nv;
                    command.Parameters.Add("@EMAIL_NV", System.Data.SqlDbType.NVarChar).Value = entityUser.email_nv;
                    command.Parameters.Add("@ROL", System.Data.SqlDbType.Int).Value = entityUser.rol_i;

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
        public int INSERTUSER(EntityUser entityUser)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_USER", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value =0;
                    command.Parameters.Add("@FULLNAME_NV", System.Data.SqlDbType.NVarChar).Value = entityUser.name_nv;
                    command.Parameters.Add("@EMAIL_NV", System.Data.SqlDbType.NVarChar).Value = entityUser.email_nv;
                    command.Parameters.Add("@ROL", System.Data.SqlDbType.Int).Value = entityUser.rol_i;

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
        public int UPDATEUSER(EntityUser entityUser)
        {
            
            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_USER", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = entityUser.ididentifier_i;
                    command.Parameters.Add("@FULLNAME_NV", System.Data.SqlDbType.NVarChar).Value = entityUser.name_nv;
                    command.Parameters.Add("@EMAIL_NV", System.Data.SqlDbType.NVarChar).Value = entityUser.email_nv;
                    command.Parameters.Add("@ROL", System.Data.SqlDbType.Int).Value = entityUser.rol_i;

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
