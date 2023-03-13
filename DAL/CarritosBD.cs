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
    public class CarritosBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=localhost\\sqlexpress;Initial Catalog=BASEDATOS;Persist Security Info=False;User ID=USER;Password=CLAVE;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;\"";
        
        public EntityCarritos GETALLCARRITOS()
        {
            EntityCarritos entityCarritos = new EntityCarritos();

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
                    SqlCommand command = new SqlCommand("GETALLCARRITOS", connection);
                    command.Transaction = sqlTransaction; // LE pasamos la transaccion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // parametro SIMPLE
                    command.Parameters.Add("@action", System.Data.SqlDbType.VarChar).Value = "GET";                    
                    // EJECUTO EL COMANDO
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado las líneas de carrito encontrados a la lista de entidades
                        entityCarritos.lista.Add(new EntityCarrito
                        {
                            ididentifier_i = (int)reader["ID_Carrito"],
                            ID_usuario_i = (int)reader["ID_USUARIO"], 
                            ID_Producto_i = (int)reader["ID_PRODUCTO"],
                            nombre_nv = (string)reader["NOMBRE"],
                            Owner_nv = (string)reader["OWNER"],
                            Cantidad_i = (int)reader["CANTIDAD"],
                            direccion_nv = (string)reader["DIRECCION"],
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

            return entityCarritos;
        }
    }
}
