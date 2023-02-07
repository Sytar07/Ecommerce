namespace ECOMMERCE.CORE
{
    public class EntityFormasPago : ICommonEntity
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

        public string id_FPA_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _id_FPA_nv;
            }
            set
            {
                _id_FPA_nv = value;
            }
        }

        public string name_nv { 
            get{
                // Comprobaciones tipo/null/etc..
                return _name_nv;
            }
            set{
                _name_nv=value;
            } 
        }
        
        public string type_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _type_nv;
            }
            set
            {
                _type_nv = value;
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

        public string Created_Date_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _Created_Date_nv;
            }
            set
            {
                _Created_Date_nv = value;
            }
        }

        public string Mod_Date_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _Mod_Dater_nv;
            }
            set
            {
                _Mod_Date_nv = value;
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
        private string _id_FPA_nv;
        private string _name_nv;
        private string _type_nv;
        private string _Owner_nv;
        private string _Created_Date_nv;
        private string _Mod_Date_nv;
        private bool _delete_b;
        public void Init()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            name_nv = "";
            id_FPA_nv = "";
            name_nv = "";
            type_nv = "";
            Owner_nv = "";
            Created_Date_nv = "";
            Mod_Date_nv = "";

    }

    }
    public class EntityFormasPagos
    {
        public IList<EntityFormasPago> entityFormasPagos;

        public EntityFormasPagos()
        {
            if (entityFormasPagos == null)
            {
                entityFormasPagos = new List<EntityFormasPago();
            }
        }
    }

    
}