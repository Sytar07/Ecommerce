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
    internal class PaisesBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=localhost\\sqlexpress;Initial Catalog=BASEDATOS;Persist Security Info=False;User ID=USER;Password=CLAVE;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;\"";
        
        public EntityPaises GETALLPAISES()
        {
            EntityPaises entity = new EntityPaises();

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
                        // Añado los usuarios encontrados a la lista de entidades
                        entity.list.Add(new EntityPais((int)reader["ID"], (string)reader["Nombre"]));

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

            return entity;
        }
    }
}
