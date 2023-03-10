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

    /// <summary>
    /// 
    /// </summary>
    public class PaisesBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=ROCINANTE\\SQLEXPRESS;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public EntityPaises GETALLPAISES()
        {
            EntityPaises entityPaises = new EntityPaises();

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
                    SqlCommand command = new SqlCommand("GETPAISES", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;                                 
                    // EJECUTO EL COMANDO
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado los paises encontrados a la lista de entidades
                        entityPaises.lista.Add(new EntityPais

                        {
                            ididentifier_i = (int)reader["ID_COUNTRY"],
                            name_nv = (string)reader["NAME"],
                            Owner_nv = (string)reader["OWNER"],
                            FechaCreacion_dt = (DateTime)reader["FECHA_CREACION"],
                            FechaModificacion_dt = (DateTime)reader["FECHA_MODIFICACION"],
                        });
                        Console.WriteLine((int)reader["ID"]);
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
                    // SI chuta le damos adelante. COMMIT
                    sqlTransaction.Commit();
                }

            }

            return entityPaises;
        }

        public int DELETEPAIS(EntityPais entityPais)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();

                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_PAIS", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_COUNTRY", System.Data.SqlDbType.Int).Value = entityPais.Id_Country_nv;
                    command.Parameters.Add("@NAME_NV", System.Data.SqlDbType.NVarChar).Value = entityPais.name_nv;
                    command.Parameters.Add("@OWNER_NV", System.Data.SqlDbType.NVarChar).Value = entityPais.Owner_nv;
                    command.Parameters.Add("@FECHA_CREACION_DT", System.Data.SqlDbType.Int).Value = entityPais.FechaCreacion_dt;
                    command.Parameters.Add("@FECHA_MODIFICACION_DT", System.Data.SqlDbType.Int).Value = entityPais.FechaModificacion_dt;

                    command.Parameters.Add("@delete", System.Data.SqlDbType.SmallInt).Value = 1;

                    command.Parameters.Add("@ID_RETURN", System.Data.SqlDbType.Int).Value = 0;
                    command.Parameters["@ID_RETURN"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    salida = int.Parse(command.Parameters["@ID_RETURN"].Value.ToString());

                }

                catch (Exception ex)
                {
                    // Ha fallado ROLLBACK a la transaccion
                    salida = -1;
                    sqlTransaction.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    // SI funciona la transaccion le damos adelante. COMMIT
                    sqlTransaction.Commit();
                }
            }

            return salida;
        }
        public int INSERTPAIS(EntityPais entityPais)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_PAIS", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_COUNTRY", System.Data.SqlDbType.Int).Value = entityPais.Id_Country_nv;
                    command.Parameters.Add("@NAME_NV", System.Data.SqlDbType.NVarChar).Value = entityPais.name_nv;
                    command.Parameters.Add("@OWNER_NV", System.Data.SqlDbType.NVarChar).Value = entityPais.Owner_nv;
                    command.Parameters.Add("@FECHA_CREACION_DT", System.Data.SqlDbType.Int).Value = entityPais.FechaCreacion_dt;
                    command.Parameters.Add("@FECHA_MODIFICACION_DT", System.Data.SqlDbType.Int).Value = entityPais.FechaModificacion_dt;

                    command.Parameters.Add("@delete", System.Data.SqlDbType.SmallInt).Value = 0;

                    command.Parameters.Add("@ID_RETURN", System.Data.SqlDbType.Int).Value = 0;
                    command.Parameters["@ID_RETURN"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    salida = int.Parse(command.Parameters["@ID_RETURN"].Value.ToString());

                }

                catch (Exception ex)
                {
                    // Ha fallado ROLLBACK a la transaccion
                    salida = -1;
                    sqlTransaction.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    // SI funciona la transaccion le damos adelante. COMMIT
                    sqlTransaction.Commit();
                }
            }

            return salida;
        }
        public int UPDATEPAIS(EntityPais entityPais)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_PAIS", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_COUNTRY", System.Data.SqlDbType.Int).Value = entityPais.Id_Country_nv;
                    command.Parameters.Add("@NAME_NV", System.Data.SqlDbType.NVarChar).Value = entityPais.name_nv;
                    command.Parameters.Add("@OWNER_NV", System.Data.SqlDbType.NVarChar).Value = entityPais.Owner_nv;
                    command.Parameters.Add("@FECHA_CREACION_DT", System.Data.SqlDbType.Int).Value = entityPais.FechaCreacion_dt;
                    command.Parameters.Add("@FECHA_MODIFICACION_DT", System.Data.SqlDbType.Int).Value = entityPais.FechaModificacion_dt;

                    command.Parameters.Add("@delete", System.Data.SqlDbType.SmallInt).Value = 0;

                    command.Parameters.Add("@ID_RETURN", System.Data.SqlDbType.Int).Value = 0;
                    command.Parameters["@ID_RETURN"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    salida = int.Parse(command.Parameters["@ID_RETURN"].Value.ToString());

                }

                catch (Exception ex)
                {
                    // Ha fallado ROLLBACK a la transaccion
                    salida = -1;
                    sqlTransaction.Rollback();
                    throw new Exception(ex.Message);
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
