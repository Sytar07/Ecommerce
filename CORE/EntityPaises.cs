namespace ECOMMERCE.CORE
{
    public class EntityPaises : ICommonEntity
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

        public string Id_Country_nv { 
            get{
                // Comprobaciones tipo/null/etc..
                return _Id_Country_nv;
            }
            set{
                _Id_Country_nv=value;
            } 
        }

        public string name_nv {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _name_nv;
            }
            set
            {
                _name_nv = value;
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
        private string _Id_Country_nv;
        private string _name_nv;
        private string _email_nv;
        private string _owner_nv
        private string _fecha_creacion_nv
        private string _fecha_modificacion_nv
        private bool _delete_b;
        public void Init()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            Id_Country_nv = "";
            name_nv = "";
            email_nv = "";
            owner_nv = "";
            fecha_creacion_nv = "";
            fecha_modificacion_nv = "";
        }

    }
    public class EntityPaises
    {
        public IList<EntityPais> entityPaises;

        public EntityPaises()
        {
            if (entityPaises == null)
            {
                entityPaises = new List<EntityPais>();
            }
        }
    }

    
}