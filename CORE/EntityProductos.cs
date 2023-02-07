namespace ECOMMERCE.CORE
{
    public class EntityProductos : ICommonEntity
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

        public string stock_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _stock_nv;
            }
            set
            {
                _stock_nv = value;
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

        public string precio_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _precio_nv;
            }
            set
            {
                _precio_nv = value;
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

        public string fecha_creacion_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _fecha_creacion_nv;
            }
            set
            {
                _fecha_creacion_nv = value;
            }
        }

        public string fecha_modificacion_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _fecha_modificacion_nv;
            }
            set
            {
                _fecha_modificacion_nv = value;
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
        private string _stock_nv;
        private string _descripcion_nv;
        private string _precio_nv;
        private string _owner_nv;
        private string _fecha_creacion_nv;
        private string _fecha_modificacion_nv;
        private bool _delete_b;
        public void Init()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            id_producto_nv = "";
            nombre_nv = "";
            stock_nv = "";
            descripcion_nv = "";
            precio_nv = "";
            owner_nv = "";
            fecha_creacion_nv = "";
            fecha_modificacion_nv = "";

    }

    }
    public class EntityProductos
    {
        public IList<EntityProducto> entityProductos;

        public EntityProductos()
        {
            if (entityProductos == null)
            {
                entityProductos = new List<EntityProducto>();
            }
        }
    }

    
}