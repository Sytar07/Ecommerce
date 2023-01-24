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
    internal class EXAMPLE
    {
        public string cadenaConexion_BBDD { get; set; }
        
        public void ACCESOBBDD()
        {
            // declaro la conexion a BBDD
            using (SqlConnection connection = new SqlConnection("VARIABLE_CONEXSION"))
            {
                // Abro la conexion
                connection.Open();
                // Declaro una transaccion
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    // Declaro un COMANDO para ejecutar un PROCEDIMIENTO ALMACENADO
                    SqlCommand command = new SqlCommand("PROC_ALM", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // parametro SIMPLE
                    command.Parameters.Add("@param1", System.Data.SqlDbType.VarChar).Value = "INSERT";
                    
                    // CLASE DE PRUEBA (ENTIDAD)
                    CommonEntity commonEntity = new CommonEntity();
                    commonEntity.ididentifier_i = "1";

                    // parametro JSON
                    command.Parameters.Add("@param2JSON", System.Data.SqlDbType.NVarChar).Value = JsonSerializer.Serialize(commonEntity);
                    // parametro SALIDA
                    command.Parameters.Add("@SALIDA", System.Data.SqlDbType.Int).Value = 0;                   
                    command.Parameters["@SALIDA"].Direction = ParameterDirection.Output;
                    // EJECUTO EL COMANDO
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0));
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

            return newCreateReview.taskData.ID_Review;
        }
    }
}
