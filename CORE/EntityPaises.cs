namespace ECOMMERCE.CORE
{
    public class EntityPais : ICommonEntity
    {
        // ID_COUNTRY
        public int ididentifier_i
        {
            get
            {
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
            set
            {
                _ididentifier_i = value;
            }
        }

        // NAME
        public string name_nv
        {
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

        private int _ididentifier_i;
        private string _name_nv;

        public EntityPais(int p_ididentifier_i, string p_name_nv)   
        {
            // Aqui hago cosas.
            this.ididentifier_i = p_ididentifier_i;
            this.name_nv = p_name_nv;
        }
        public void Init()
        {
        }

    }
    public class EntityPaises
    {
        public IList<EntityPais> list;

        public EntityPaises()
        {
            if (list == null)
            {
                list = new List<EntityPais>();
            }
        }
    }


}