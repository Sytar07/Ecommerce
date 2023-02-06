namespace ECOMMERCE.CORE
{
    public class EntityDirecciones : ICommonEntity
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

        public string id_direccion_nv { 
            get{
                // Comprobaciones tipo/null/etc..
                return _id_direccion_nv;
            }
            set{
                _id_direccion_nv = value;
            } 
        }

        public string calle_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _calle_nv;
            }
            set
            {
                _calle_nv = value;
            }
        }

        public string numero_nv {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _numero_nv;
            }
            set
            {
                _numero_nv = value;
            }
        }

        public string puerta_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _puerta_nv;
            }
            set
            {
                _puerta_nv = value;
            }
        }

        public string ciudad_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _ciudad_nv;
            }
            set
            {
                _ciudad_nv = value;
            }
        }

        public string pais_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _pais_nv;
            }
            set
            {
                _pais_nv = value;
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

        public string FechaModificacion_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _FechaModificacion_nv;
            }
            set
            {
                _FechaModificacion_nv = value;
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
        private string _id_direccion_nv;
        private string _calle_nv;
        private string _numero_nv;
        private string _puerta_nv;
        private string _ciudad_nv;
        private string _pais_nv;
        private string _Owner_nv;
        private string _FechaCreacion_nv;
        private string _FechaModificacion_nv;
        private bool _delete_b;
        public void Init()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            id_direccion_nv = "";
            calle_nv = "";
            numero_nv = "";
            puerta_nv = "";
            ciudad_nv = "";
            pais_nv = "";
            Owner_nv = "";
            FechaCreacion_nv = "";
            FechaModificacion_nv = "";

        }

    }
    public class EntityDirecciones
    {
        public IList<EntityDireccion> entityDirecciones;

        public EntityDirecciones()
        {
            if (entityDirecciones == null)
            {
                entityDirecciones = new List<EntityDireccion>();
            }
        }
    }

    
}