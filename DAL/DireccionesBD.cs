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
    public class DireccionesBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=ROCINANTE\\SQLEXPRESS;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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
                        entityDirecciones.lista.Add(new EntityDireccion
                        {
                            ididentifier_i = (int)reader["ID_DIRECCION"],
                            direccion_nv = (string)reader["DIRECCION"],
                            calle_nv = (string)reader["CALLE"],
                            numero_i = (int)reader["NUMERO"],
                            puerta_nv = (string)reader["PUERTA"],
                            ciudad_nv = (string)reader["CIUDAD"],
                            pais_nv = (string)reader["PAIS"],
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

            return entityDirecciones;
        }
    }
}
