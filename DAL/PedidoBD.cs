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

    /// <summary>
    /// 
    /// </summary>
    public class PedidosBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DEV_MARKET_3;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private DireccionesBD DireccionesBD;
        private FormasPagoBD FormasPagoBD;
        private UsersBD UsersBD;

        public List<EntityPedido> GETALLPedidos()
        {
            List<EntityPedido> entityPedidos = new List<EntityPedido>();

            // declaro la conexion a BBDD
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                // Abro la conexion
                connection.Open();
                // Declaro una transaccion
                try
                {
                
                    SqlCommand command = new SqlCommand("GET_Pedidos", connection);
                
                    command.CommandType = System.Data.CommandType.StoredProcedure;                                 
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado los Pedidos encontrados a la lista de entidades

                        entityPedidos.Add(new EntityPedido
                        {
                            ididentifier_i = (int)reader["ID_PEDIDO"],

                            Fecha_Pedido = (DateTime)reader["FECHA_PEDIDO"],
                            Fecha_Envio = (DateTime?)reader["FECHA_ENVIO"],
                            SubTotal = (decimal)reader["SUBTOTAL"],
                            Total = (decimal)reader["TOTAL"],
                            Iva = (decimal)reader["IVA"],
                            Estado = reader["ESTADO"].ToString(),

                            Direccion = DireccionesBD.GETDIRECCION((int)reader["ID_DIRECCION"]),
                            FormaPago = FormasPagoBD.GETFORMAPAGO((int)reader["ID_FORMA_PAGO"]),
                            Usuario = UsersBD.GETUSER((int)reader["ID_USUARIO"]),

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

            return entityPedidos;
        }
        public EntityPedido GETPedido(int id_Pedido)
        {
            EntityPedido EntityPedido = new EntityPedido();

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
                    SqlCommand command = new SqlCommand("GET_Pedido", connection);

                    command.Transaction = sqlTransaction; // LE pasamos la transaccion
                    command.Parameters.Add("@ID_PEDIDO", System.Data.SqlDbType.Int).Value = id_Pedido;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var reader = command.ExecuteReader();
                  
                    while (reader.Read())
                    {
                        // Añado los Pedidos encontrados a la lista de entidades
                        EntityPedido.ididentifier_i = (int)reader["ID_PEDIDO"];

                        EntityPedido.Fecha_Pedido = (DateTime)reader["FECHA_PEDIDO"];
                        EntityPedido.Fecha_Envio = (DateTime?)reader["FECHA_ENVIO"];
                        EntityPedido.SubTotal = (decimal)reader["SUBTOTAL"];
                        EntityPedido.Total = (decimal)reader["TOTAL"];
                        EntityPedido.Iva = (decimal)reader["IVA"];
                        EntityPedido.Estado = reader["ESTADO"].ToString();

                        EntityPedido.Direccion = DireccionesBD.GETDIRECCION((int)reader["ID_DIRECCION"]);
                        EntityPedido.FormaPago = FormasPagoBD.GETFORMAPAGO((int)reader["ID_FORMA_PAGO"]);
                        EntityPedido.Usuario= UsersBD.GETUSER((int)reader["ID_USUARIO"]);

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

            return EntityPedido;
        }
  
        public int INSERTPedido(EntityPedido entityPedido)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CREAR_PEDIDO", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID_CONEXION", System.Data.SqlDbType.Int).Value = entityPedido.conexion;
                    command.Parameters.Add("@ID_FPA", System.Data.SqlDbType.NVarChar).Value = entityPedido.FormaPago.ididentifier_i;
                    command.Parameters.Add("@ID_DIRECCION", System.Data.SqlDbType.NVarChar).Value = entityPedido.Direccion.ididentifier_i;
                    command.Parameters.Add("@id_user", System.Data.SqlDbType.NVarChar).Value = entityPedido.Direccion.user_i;
                    // Los datos de la tarjeta no se guardan en BBDD 

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
        public int UPDATEEstadoPedido(EntityPedido entityPedido)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("SET_ENVIADO_PEDIDO", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = entityPedido.ididentifier_i;

                    command.Parameters.Add("@ESTADO", System.Data.SqlDbType.SmallInt).Value = "ENVIADO";

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