using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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

        public int rol_i
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _rol_i;
            }
            set
            {
                _rol_i = value;
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
        private string _name_nv;
        private string _fullname_nv;
        private string _email_nv;
        private string _Owner_nv;
        private int _rol_i;
        private DateTime? _FechaCreacion_dt;
        private DateTime? _FechaModificacion_dt;
        private bool _delete_b;
        

        public EntityUser()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            name_nv = "";
            email_nv = "";
            Owner_nv = "";
            _rol_i = 1;
            FechaCreacion_dt = DateTime.MinValue;
            FechaModificacion_dt = DateTime.MinValue;

        }

    }
    public class EntityUsers
    {
        public IList<EntityUser> lista;

        public EntityUsers()
        {
            if (lista == null)
            {
                lista = new List<EntityUser>();
            }
        }
    }

    
}