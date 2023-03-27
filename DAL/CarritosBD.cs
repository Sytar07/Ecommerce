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
        public string cadenaConexion_BBDD { get; set; } = "Data Source=ROCINANTE\\SQLEXPRESS;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public EntityCarritos GETALLCARRITOS()
        {
            EntityCarritos entityCarritos = new EntityCarritos();

            // declaro la conexion a BBDD
            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                // Abro la conexion
                connection.Open();
                // Declaro una transaccion
                try
                {
                    SqlCommand command = new SqlCommand("GET_CARRITOS", connection);
                
                    command.CommandType = System.Data.CommandType.StoredProcedure;
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
                            
                            Cantidad_i = (int)reader["CANTIDAD"],
                            direccion_nv = (string)reader["DIRECCION"],
                            FechaCreacion_dt = (DateTime)reader["FECHA_CREACION"],
                            FechaModificacion_dt = (DateTime)reader["FECHA_MODIFICACION"],
                    });

                        Console.WriteLine((int)reader["ID_CARRITO"]);
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

            return entityCarritos;
        }
    }
}
