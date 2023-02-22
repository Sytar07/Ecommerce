using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ECOMMERCE.CORE
{
    public class EntityProducto : ICommonEntity
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

        public string id_producto_nv { 
            get{
                // Comprobaciones tipo/null/etc..
                return _id_producto_nv;
            }
            set{
                _id_producto_nv = value;
            } 
        }

        public string nombre_nv {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _nombre_nv;
            }
            set
            {
                _nombre_nv = value;
            }
        }

        public double stock_f
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _stock_f;
            }
            set
            {
                _stock_f = value;
            }
        }

        public string descripcion_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _descripcion_nv;
            }
            set
            {
                _descripcion_nv = value;
            }
        }

        public double precio_f
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _precio_f;
            }
            set
            {
                _precio_f = value;
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

        public DateTime? FechaCreacion_dt
        {
            get
            {
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
        private string _id_producto_nv;
        private string _nombre_nv;
        private double _stock_f;
        private string _descripcion_nv;
        private double _precio_f;
        private string _Owner_nv;
        private DateTime? _FechaCreacion_dt;
        private DateTime? _FechaModificacion_dt;
        private bool _delete_b;
        public EntityProducto()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            id_producto_nv = "";
            nombre_nv = "";
            stock_f = 0;
            descripcion_nv = "";
            precio_f = 0;
            Owner_nv = "";
            FechaCreacion_dt = DateTime.MinValue;
            FechaModificacion_dt = DateTime.MinValue;

        }

    }
    public class EntityProductos
    {
        public IList<EntityProducto> lista;

        public EntityProductos()
        {
            if (lista == null)
            {
                lista = new List<EntityProducto>();
            }
        }
    }

    
}