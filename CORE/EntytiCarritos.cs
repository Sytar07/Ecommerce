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

        public string ID_usuario_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _ID_usuario_nv;
            }
            set
            {
                _ID_usuario_nv = value;
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
        public string FechaCreacion_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _FechaCreacion_nv;
            }
            set
            {
                _FechaCreacion_nv = value;
            }
        }
        public string Fechamodificacion_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _Fechamodificacion_nv;
            }
            set
            {
                _Fechamodificacion_nv = value;
            }
        }

        public string owner_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _owner_nv;
            }
            set
            {
                _owner_nv = value;
            }
        }
        public string ID_Producto_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _ID_Producto_nv;
            }
            set
            {
                _ID_Producto_nv = value;
            }
        }
        public string cantidad_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _cantidad_nv;
            }
            set
            {
                _cantidad_nv = value;
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

        public int ID_Usuario_i { get; set; }
        public int ID_Producto_i { get; set; }
        public DateTime FechaCreacion_dt { get; set; }
        public DateTime FechaModificacion_dt { get; set; }

        private int _ididentifier_i;
        private int _ID_Usuario_i;
        private string _direccion_nv;
        private DateTime? _FechaCreacion_dt;
        private DateTime? _FechaModificacion_dt;
        private int _owner_i;
        private string _nombre_nv;
        private int _ID_Producto_i;
        private string _Cantidad_nv;
        private bool _delete_b;
        
        public EntityCarrito()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            ID_Usuario_i = 0;
            ID_Producto_i = 0;
            nombre_nv = "";
            owner_nv = "";
            cantidad_nv = "";
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