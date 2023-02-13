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
    internal class DireccionesBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=localhost\\sqlexpress;Initial Catalog=BASEDATOS;Persist Security Info=False;User ID=USER;Password=CLAVE;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;\"";
        
        public EntityDirecciones GETALLDIRECCIONES()
        {
            EntityDirecciones entityDirecciones = new EntityDirecciones();

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
                    SqlCommand command = new SqlCommand("GETALLDIRECCIONES", connection);
                    command.Transaction = sqlTransaction; // Le pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // parametro SIMPLE
                    command.Parameters.Add("@action", System.Data.SqlDbType.VarChar).Value = "GET";                    
                    // EJECUTO EL COMANDO
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado las direcciones encontradas a la lista de entidades
                        entityDirecciones.entityDirecciones.Add(new EntityDirecciones
                        {
                            ididentifier_i = (int)reader["ID"],
                            id_direccion = (int)reader["ID_Direccion"],
                            calle_nv = (string)reader["Calle"],
                            numero_nv = (int)reader["Numero"],
                            puerta_nv = (string)reader["Puerta"],
                            ciudad_nv = (string)reader["Ciudad"],
                            pais_nv = (string)reader["Pais"],
                            owner_nv = (string)reader["Owner"],
                            fecha_creacion_nv = (DateTime)reader["Fecha_Creacion"],
                            fecha_modificacion_nv = (DateTime)reader["Fecha_Modificacion"],
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

            return entityDirecciones;
        }
    }
}
