using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ECOMMERCE.CORE
{
    public class EntityConexion : ICommonEntity
    {
        public int ididentifier_i {
            get{
                // Comprobaciones tipo/null/etc..
                if (_ididentifier_i == -1)
                {
                    return 0;
                }
                else
                {
                    return _ididentifier_i;
                }
            }
            set{
                _ididentifier_i=value;
            } 
        }

        public string ID_Carrito_nv { 
            get{
                // Comprobaciones tipo/null/etc..
                return _ID_Carrito_nv;
            }
            set{
                _ID_Carrito_nv = value;
            } 
        }

     
        public DateTime? FechaInicio_dt
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _FechaInicio_dt;
            }
            set
            {
                _FechaInicio_dt = value;
            }
        }

        public bool delete
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _delete_b;
            }
            set
            {
                _delete_b = value;
            }
        }


        private int _ididentifier_i;
        public int _ID_Carrito_i;
        public DateTime fecha_inicio;

        public EntityConexion()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            Id_Carrito_nv = "";
            FechaInicio_dt = DateTime.MinValue;
        }

    }
    public class EntityConexiones
    {
        public IList<EntityConexion> lista;
       
        public EntityConexiones()
        {
            if (lista == null)
            {
                lista = new List<EntityConexion>();
            }
        }
    }

    
}