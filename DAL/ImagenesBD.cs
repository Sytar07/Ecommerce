﻿using Microsoft.Data.SqlClient;
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
        public string cadenaConexion_BBDD { get; set; } = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DEV_MARKET_3;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public EntityImagenes GETALLIMAGENES(int id_producto)
        {
            EntityImagenes entityImagenes = new EntityImagenes();

            // declaro la conexion a BBDD
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                // Abro la conexion
                connection.Open();
                // Declaro una transaccion
                try
                {
                    SqlCommand command = new SqlCommand("GET_IMAGENES", connection);
                    command.Parameters.Add("@ID_PRODUCTO", System.Data.SqlDbType.Int).Value = id_producto;

                    command.CommandType = System.Data.CommandType.StoredProcedure;
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
                            id_producto = (int)reader["ID_PRODUCTO"],

                        });

                        Console.WriteLine((int)reader["ID_IMAGEN"]);
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

            return entityImagenes;
        }
        public EntityImagen GETIMAGEN(int id_imagen)
        {
            EntityImagen entityImagen = new EntityImagen();

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
                    SqlCommand command = new SqlCommand("GET_IMAGEN", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id_imagen;
                    
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // parametro SIMPLE
                    //command.Parameters.Add("@action", System.Data.SqlDbType.VarChar).Value = "GET";                    
                    // EJECUTO EL COMANDO
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado los usuarios encontrados a la lista de entidades
                        entityImagen.ididentifier_i = (int)reader["ID_IMAGEN"];
                        entityImagen.path_nv = (string)reader["PATH"];
                        entityImagen.id_producto= (int)reader["ID_PRODUCTO"];
                        entityImagen.tipo_nv = (string)reader["TIPO"];
                        
                        Console.WriteLine((int)reader["ID_IMAGEN"]);
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

            return entityImagen;
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
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = entityImagen.ididentifier_i;
                    command.Parameters.Add("@PATH", System.Data.SqlDbType.NVarChar).Value = entityImagen.path_nv;
                    command.Parameters.Add("@ID_PRODUCTO", System.Data.SqlDbType.Int).Value = entityImagen.id_producto;
                    command.Parameters.Add("@TIPO", System.Data.SqlDbType.NVarChar).Value = entityImagen.tipo_nv;
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
                    //@ID as int, @Path as nvarchar(50), @Tipo as nvarchar(50),  @Fecha_Creacion as datetime, @Fecha_Modificacion as datetime,  @delete as smallint, @ID_RETURN INT OUTPUT
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = 0;
                    command.Parameters.Add("@ID_PRODUCTO", System.Data.SqlDbType.Int).Value = entityImagen.id_producto;
                    command.Parameters.Add("@PATH", System.Data.SqlDbType.NVarChar).Value = entityImagen.path_nv;
                    command.Parameters.Add("@TIPO", System.Data.SqlDbType.NVarChar).Value = entityImagen.tipo_nv;


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
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = entityImagen.ididentifier_i;
                    command.Parameters.Add("@PATH", System.Data.SqlDbType.NVarChar).Value = entityImagen.path_nv;
                    command.Parameters.Add("@ID_PRODUCTO", System.Data.SqlDbType.NVarChar).Value = entityImagen.id_producto;
                    command.Parameters.Add("@TIPO", System.Data.SqlDbType.NVarChar).Value = entityImagen.tipo_nv;


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
