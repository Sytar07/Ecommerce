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
    internal class ProductosBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=localhost\\sqlexpress;Initial Catalog=BASEDATOS;Persist Security Info=False;User ID=USER;Password=CLAVE;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;\"";
        
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
                        entityProductos.entityProductos.Add(new EntityProductos
                        {
                            ididentifier_i = (int)reader["ID"],
                            id_producto = (int)reader["ID_Imagen"],
                            nombre = (string)reader["ID_Imagen"],
                            stock = (int)reader["Owner"],
                            descripcion = (string)reader["Fecha_Creacion"],
                            precio = (float)reader["Precio"],
                            owner = (string)reader["Owner"],
                            fecha_creacion = (DateTime)reader["Fecha_Creacion"],
                            fecha_modificacion = (DateTime)reader["Fecha_Modificacion"],
                            

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
    }
}
