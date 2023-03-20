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
    public class ProductosBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=ROCINANTE\\SQLEXPRESS;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public EntityProductos GETALLPRODUCTOS()
        {
            EntityProductos entityProductos = new EntityProductos();

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
                    SqlCommand command = new SqlCommand("GETALLPRODUCTOS", connection);
                    command.Transaction = sqlTransaction; // Le pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // parametro SIMPLE
                    command.Parameters.Add("@action", System.Data.SqlDbType.VarChar).Value = "GET";                    
                    // EJECUTO EL COMANDO
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado las Productos encontrados a la lista de entidades
                        entityProductos.lista.Add(new EntityProducto
                        {
                            ididentifier_i = (int)reader["ID_PRODUCTO"],
                            nombre_nv = (string)reader["NOMBRE"],
                            stock_f = (int)reader["STOCK"],
                            descripcion_nv = (string)reader["DESCRIPCION"],
                            precio_f = (float)reader["PRECIO"],
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
                    // SI funciona la transaccion le damos adelante. COMMIT
                    sqlTransaction.Commit();
                }

            }

            return entityProductos;
        }

        public EntityProducto GETPRODUCTO(int id)
        {
            EntityProducto entityProducto = new EntityProducto();

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
                    SqlCommand command = new SqlCommand("GETPRODUCTO", connection);
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id;
                    command.Transaction = sqlTransaction; // Le pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // parametro SIMPLE
                    command.Parameters.Add("@action", System.Data.SqlDbType.VarChar).Value = "GET";
                    // EJECUTO EL COMANDO
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado las Productos encontrados a la lista de entidades
                        entityProducto= new EntityProducto
                        {
                            ididentifier_i = (int)reader["ID_PRODUCTO"],
                            nombre_nv = (string)reader["NOMBRE"],
                            stock_f = (int)reader["STOCK"],
                            descripcion_nv = (string)reader["DESCRIPCION"],
                            precio_f = (float)reader["PRECIO"],
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
                    // SI funciona la transaccion le damos adelante. COMMIT
                    sqlTransaction.Commit();
                }

            }

            return entityProducto;
        }

        public int DELETEPRODUCTO(EntityProducto entityProducto)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();

                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_PRODUCTO", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_PRODUCTO", System.Data.SqlDbType.Int).Value = entityProducto.id_producto_nv;
                    command.Parameters.Add("@NOMBRE", System.Data.SqlDbType.NVarChar).Value = entityProducto.nombre_nv;
                    command.Parameters.Add("@STOCK", System.Data.SqlDbType.NVarChar).Value = entityProducto.stock_f;
                    command.Parameters.Add("@DESCRIPCION", System.Data.SqlDbType.Int).Value = entityProducto.descripcion_nv;
                    command.Parameters.Add("@PRECIO", System.Data.SqlDbType.Int).Value = entityProducto.precio_f;
                    command.Parameters.Add("@OWNER", System.Data.SqlDbType.Int).Value = entityProducto.Owner_nv;
                    command.Parameters.Add("@FECHA_CREACION", System.Data.SqlDbType.Int).Value = entityProducto.FechaCreacion_dt;
                    command.Parameters.Add("@FECHA_MODIFICACION", System.Data.SqlDbType.Int).Value = entityProducto.FechaModificacion_dt;

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
  
        public int INSERTPRODUCTO(EntityProducto entityProducto)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_PRODUCTO", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_PRODUCTO", System.Data.SqlDbType.Int).Value = entityProducto.id_producto_nv;
                    command.Parameters.Add("@NOMBRE", System.Data.SqlDbType.NVarChar).Value = entityProducto.nombre_nv;
                    command.Parameters.Add("@STOCK", System.Data.SqlDbType.NVarChar).Value = entityProducto.stock_f;
                    command.Parameters.Add("@DESCRIPCION", System.Data.SqlDbType.Int).Value = entityProducto.descripcion_nv;
                    command.Parameters.Add("@PRECIO", System.Data.SqlDbType.Int).Value = entityProducto.precio_f;
                    command.Parameters.Add("@OWNER", System.Data.SqlDbType.Int).Value = entityProducto.Owner_nv;
                    command.Parameters.Add("@FECHA_CREACION", System.Data.SqlDbType.Int).Value = entityProducto.FechaCreacion_dt;
                    command.Parameters.Add("@FECHA_MODIFICACION", System.Data.SqlDbType.Int).Value = entityProducto.FechaModificacion_dt;

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
        public int UPDATEPRODUCTO(EntityProducto entityProducto)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_PRODUCTO", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_PRODUCTO", System.Data.SqlDbType.Int).Value = entityProducto.id_producto_nv;
                    command.Parameters.Add("@NOMBRE", System.Data.SqlDbType.NVarChar).Value = entityProducto.nombre_nv;
                    command.Parameters.Add("@STOCK", System.Data.SqlDbType.NVarChar).Value = entityProducto.stock_f;
                    command.Parameters.Add("@DESCRIPCION", System.Data.SqlDbType.Int).Value = entityProducto.descripcion_nv;
                    command.Parameters.Add("@PRECIO", System.Data.SqlDbType.Int).Value = entityProducto.precio_f;
                    command.Parameters.Add("@OWNER", System.Data.SqlDbType.Int).Value = entityProducto.Owner_nv;
                    command.Parameters.Add("@FECHA_CREACION", System.Data.SqlDbType.Int).Value = entityProducto.FechaCreacion_dt;
                    command.Parameters.Add("@FECHA_MODIFICACION", System.Data.SqlDbType.Int).Value = entityProducto.FechaModificacion_dt;

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
