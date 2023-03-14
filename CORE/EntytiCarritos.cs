using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ECOMMERCE.CORE
{
    public class EntityCarrito : ICommonEntity
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

        public string nombre_nv { 
            get{
                // Comprobaciones tipo/null/etc..
                return _nombre_nv;
            }
            set{
                _nombre_nv = value;
            } 
        }

        public int ID_usuario_i
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _ID_Usuario_i;
            }
            set
            {
                _ID_Usuario_i = value;
            }
        }
        public string direccion_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _direccion_nv;
            }
            set
            {
                _direccion_nv = value;
            }
        }
        public DateTime? FechaCreacion_dt
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _FechaCreacion_dt;
            }
            set
            {
                _FechaCreacion_dt = value;
            }
        }
        public DateTime? FechaModificacion_dt
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _FechaModificacion_dt;
            }
            set
            {
                _FechaModificacion_dt = value;
            }
        }

        public string Owner_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _Owner_nv;
            }
            set
            {
                _Owner_nv = value;
            }
        }
        public int ID_Producto_i
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _ID_Producto_i;
            }
            set
            {
                _ID_Producto_i = value;
            }
        }
        public int Cantidad_i
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _Cantidad_i;
            }
            set
            {
                _Cantidad_i = value;
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
        private int _ID_Usuario_i;
        private int _ID_Producto_i;
        private string _nombre_nv;
        private string _Owner_nv;
        private int _Cantidad_i;
        private string _direccion_nv;
        private DateTime? _FechaCreacion_dt;
        private DateTime? _FechaModificacion_dt;
        
        private bool _delete_b;
        
        public EntityCarrito()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            ID_usuario_i = 0;
            ID_Producto_i = 0;
            nombre_nv = "";
            Owner_nv = "";
            Cantidad_i= 0;
            direccion_nv = "";
            FechaCreacion_dt = DateTime.MinValue;
            FechaModificacion_dt = DateTime.MinValue;

        }

    }
    public class EntityCarritos
    {
        public IList<EntityCarrito> lista;

        public EntityCarritos()
        {
            if (lista == null)
            {
                lista = new List<EntityCarrito>();
            }
        }
    }

    
}