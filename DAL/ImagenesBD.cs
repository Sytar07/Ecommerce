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
    public class ImagenesBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=ROCINANTE\\SQLEXPRESS;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public EntityImagenes GETALLIMAGENES()
        {
            EntityImagenes entityImagenes = new EntityImagenes();

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
                    SqlCommand command = new SqlCommand("GETALLIMAGENES", connection);
                    command.Transaction = sqlTransaction; // Le pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // parametro SIMPLE
                    command.Parameters.Add("@action", System.Data.SqlDbType.VarChar).Value = "GET";                    
                    // EJECUTO EL COMANDO
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado las Imagenes encontradas a la lista de entidades
                        entityImagenes.lista.Add(new EntityImagen
                        {
                            ididentifier_i = (int)reader["ID_IMAGEN"],
                            path_nv = (string)reader["PATH"],
                            tipo_nv = (string)reader["TIPO"],
                            Owner_nv = (string)reader["OWNER"],
                            FechaCreacion_dt= (DateTime)reader["FECHA_CREACION"],
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
                    // SI funciona la transaccion le damos adelante. COMMIT
                    sqlTransaction.Commit();
                }

            }

            return entityImagenes;
        }

        public int DELETEIMAGEN(EntityImagen entityImagen)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();

                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_IMAGEN", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_IMAGEN", System.Data.SqlDbType.Int).Value = entityImagen.id_imagen_nv;
                    command.Parameters.Add("@PATH", System.Data.SqlDbType.NVarChar).Value = entityImagen.path_nv;
                    command.Parameters.Add("@TIPO", System.Data.SqlDbType.NVarChar).Value = entityImagen.tipo_nv;
                    command.Parameters.Add("@OWNER", System.Data.SqlDbType.Int).Value = entityImagen.Owner_nv;
                    command.Parameters.Add("@FECHA_CREACION", System.Data.SqlDbType.Int).Value = entityImagen.FechaCreacion_dt;
                    command.Parameters.Add("@FECHA_MODIFICACION", System.Data.SqlDbType.Int).Value = entityImagen.FechaModificacion_dt;


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
        public int INSERTIMAGEN(EntityImagen entityImagen)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_IMAGEN", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_IMAGEN", System.Data.SqlDbType.Int).Value = entityImagen.id_imagen_nv;
                    command.Parameters.Add("@PATH", System.Data.SqlDbType.NVarChar).Value = entityImagen.path_nv;
                    command.Parameters.Add("@TIPO", System.Data.SqlDbType.NVarChar).Value = entityImagen.tipo_nv;
                    command.Parameters.Add("@OWNER", System.Data.SqlDbType.Int).Value = entityImagen.Owner_nv;
                    command.Parameters.Add("@FECHA_CREACION", System.Data.SqlDbType.Int).Value = entityImagen.FechaCreacion_dt;
                    command.Parameters.Add("@FECHA_MODIFICACION", System.Data.SqlDbType.Int).Value = entityImagen.FechaModificacion_dt;


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
        public int UPDATEIMAGEN(EntityImagen entityImagen)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_IMAGEN", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_IMAGEN", System.Data.SqlDbType.Int).Value = entityImagen.id_imagen_nv;
                    command.Parameters.Add("@PATH", System.Data.SqlDbType.NVarChar).Value = entityImagen.path_nv;
                    command.Parameters.Add("@TIPO", System.Data.SqlDbType.NVarChar).Value = entityImagen.tipo_nv;
                    command.Parameters.Add("@OWNER", System.Data.SqlDbType.Int).Value = entityImagen.Owner_nv;
                    command.Parameters.Add("@FECHA_CREACION", System.Data.SqlDbType.Int).Value = entityImagen.FechaCreacion_dt;
                    command.Parameters.Add("@FECHA_MODIFICACION", System.Data.SqlDbType.Int).Value = entityImagen.FechaModificacion_dt;

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
