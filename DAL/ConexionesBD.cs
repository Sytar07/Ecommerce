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
using System.IO;

namespace DAL
{
    public class ConexionesBD
    {
        public string cadenaConexion_BBDD { get; set; } = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DEV_MARKET_3;User ID=sa;Password=sa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        public EntityConexion GETCONEXION(string IP, string USER, int conexion)
        {
            EntityConexion entityConexion = new EntityConexion();
            int salida = 0;

            using (SqlConnection connection = new SqlConnection(cadenaConexion_BBDD))
            {
                
                connection.Open();
                try
                {
                   
                    SqlCommand command = new SqlCommand("GET_CONEXION", connection);

                    command.Parameters.Add("@IP", System.Data.SqlDbType.NVarChar).Value = IP.ToUpper();
                    command.Parameters.Add("@User", System.Data.SqlDbType.NVarChar).Value = USER.ToUpper();
                    command.Parameters.Add("@Conexion", System.Data.SqlDbType.Int).Value = conexion;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    // LO LEO. 
                    while (reader.Read())
                    {
                        // Añado las formas de pago encontradas a la lista de entidades
                        entityConexion.ididentifier_i = (int)reader["ID"];
                        entityConexion.iduser = (int)reader["ID_USER"];
                        entityConexion.ip= (string)reader["IP"];                        
                    }
                }
                catch (Exception ex)
                {
                    // Ha fallado ROLLBACK a la transaccion

                    throw new Exception(ex.Message);
                }
                finally
                {

                }

            }

            return entityConexion;
        }
    }
}
