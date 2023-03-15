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

        public int ID_Carrito_i { 
            get{
                // Comprobaciones tipo/null/etc..
                return _ID_Carrito_i;
            }
            set{
                _ID_Carrito_i = value;
            } 
        }

        public int ip
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _ip;
            }
            set
            {
                _ip = value;
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

        public DateTime? FechaCreacion_dt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? FechaModificacion_dt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Owner_nv { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private int _ididentifier_i;
        public int _ID_Carrito_i;
        public bool _delete_b;
        public DateTime? _FechaInicio_dt;
        private int _ip;

        public EntityConexion()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            ID_Carrito_i = 0;
            FechaInicio_dt = DateTime.MinValue;
            ip = 0;
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