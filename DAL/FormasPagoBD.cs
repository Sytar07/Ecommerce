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
    public class FormasPagoBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DEV_MARKET_3;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public EntityFormasPago GETALLFORMASPAGO()
        {
            EntityFormasPago entityFormasPago = new EntityFormasPago();

            // declaro la conexion a BBDD
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                // Abro la conexion
                connection.Open();
                // Declaro una transaccion
                try
                {
                    SqlCommand command = new SqlCommand("GET_FORMASPAGO", connection);
                
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado las formas de pago encontradas a la lista de entidades
                        entityFormasPago.lista.Add(new EntityFormaPago
                        {
                            ididentifier_i = (int)reader["ID_FPA"],
                            name_nv = (string)reader["NAME"],
                            type_i = (short)reader["TYPE"],
            

                    });

                        Console.WriteLine((int)reader["ID_FPA"]);
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

            return entityFormasPago;
        }
        public EntityFormaPago GETFORMAPAGO(int id_fpa)
        {
            EntityFormaPago entityFormaPago = new EntityFormaPago();

            // declaro la conexion a BBDD
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {

                // Abro la conexion
                connection.Open();
                // Declaro una transaccion

                try
                {
                    // Declaro un COMANDO para ejecutar un PROCEDIMIENTO ALMACENADO
                    SqlCommand command = new SqlCommand("GET_FORMAPAGO", connection);

                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id_fpa;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Añado las formas de pago encontradas a la lista de entidades
                        entityFormaPago.ididentifier_i = (int)reader["ID_FPA"];
                        entityFormaPago.name_nv = (string)reader["NAME"];
                        entityFormaPago.type_i = (short)reader["TYPE"];


                        Console.WriteLine((int)reader["ID_FPA"]);
                    }
                    // Cierro el READER
                    reader.Close();


                }
                catch (Exception ex)
                {
                    // Ha fallado ROLLBACK a la transaccion
                    
                    throw new Exception(ex.Message);
                }
                finally
                {
                    // SI funciona la transaccion le damos adelante. COMMIT
                    
                }

            }

            return entityFormaPago;
        }
        public int DELETEFORMAPAGO(EntityFormaPago entityFormaPago)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();

                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_FORMASPAGO", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = entityFormaPago.ididentifier_i;
                    command.Parameters.Add("@NAME", System.Data.SqlDbType.NVarChar).Value = entityFormaPago.name_nv;
                    command.Parameters.Add("@TYPE", System.Data.SqlDbType.SmallInt).Value = entityFormaPago.type_i;


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
        public int INSERTFORMAPAGO(EntityFormaPago entityFormaPago)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_FORMASPAGO", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = 0;
                    command.Parameters.Add("@NAME", System.Data.SqlDbType.NVarChar).Value = entityFormaPago.name_nv;
                    command.Parameters.Add("@TYPE", System.Data.SqlDbType.SmallInt).Value = entityFormaPago.type_i;


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
        public int UPDATEFORMAPAGO(EntityFormaPago entityFormaPago)
        {

            int salida = 0;
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand("CUD_FORMASPAGO", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = entityFormaPago.ididentifier_i;
                    command.Parameters.Add("@NAME", System.Data.SqlDbType.NVarChar).Value = entityFormaPago.name_nv;
                    command.Parameters.Add("@TYPE", System.Data.SqlDbType.SmallInt).Value = entityFormaPago.type_i;

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
