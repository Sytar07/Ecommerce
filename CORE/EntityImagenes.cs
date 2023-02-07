namespace ECOMMERCE.CORE
{
    public class EntityImagenes : ICommonEntity
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

        public string id_imagen_nv { 
            get{
                // Comprobaciones tipo/null/etc..
                return _id_imagen;
            }
            set{
                _id_imagen_nv = value;
            } 
        }

        public string path_nv {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _path_nv;
            }
            set
            {
                _path_nv = value;
            }
        }

        public string tipo_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _tipo_nv;
            }
            set
            {
                _tipo_nv = value;
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
        private string _id_imagen_nv;
        private string _path_nv;
        private string _tipo_nv;
        private string _owner_nv;
        private string _fecha_creacion_nv;
        private string _fecha_modificacion_nv;
        private bool _delete_b;
        public void Init()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            name_nv = "";

        }

    }
    public class EntityImagenes
    {
        public IList<EntityImagen> entityImagenes;

        public EntityImagenes()
        {
            if (entityImagenes == null)
            {
                entityImagenes = new List<EntityImagen>();
            }
        }
    }

    
}