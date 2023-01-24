namespace ECOMMERCE.CORE
{
    public class EntityUser : ICommonEntity
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

        public string name_nv { 
            get{
                // Comprobaciones tipo/null/etc..
                return _name_nv;
            }
            set{
                _name_nv=value;
            } 
        }

        public string fullname_nv {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _fullname_nv;
            }
            set
            {
                _fullname_nv = value;
            }
        }

        public string email_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _email_nv;
            }
            set
            {
                _email_nv = value;
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
        private string _name_nv;
        private string _fullname_nv;
        private string _email_nv;
        private bool _delete_b;
        public void Init()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            name_nv = "";

        }

    }
    public class EntityUsers
    {
        public IList<EntityUser> entityUsers;

        public EntityUsers()
        {
            if (entityUsers == null)
            {
                entityUsers = new List<EntityUser>();
            }
        }
    }

    
}