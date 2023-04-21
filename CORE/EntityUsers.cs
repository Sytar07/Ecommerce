using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ECOMMERCE.CORE
{
    public class EntityUser 
    {
        [Display(Name = "Código")]
        public int ididentifier_i {
            get{
                
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

        [Display(Name = "Nombre")]
        public string name_nv { 
            get{
                
                return _name_nv;
            }
            set{
                _name_nv=value;
            } 
        }
        [Display(Name = "Rol")]
        public int rol_i
        {
            get
            {
                
                return _rol_i;
            }
            set
            {
                _rol_i = value;
            }
        }
        [Display(Name = "Email")]
        public string email_nv
        {
            get
            {
                
                return _email_nv;
            }
            set
            {
                _email_nv = value;
            }
        }

        [Display(Name = "Clave")]
        public string Clave_nv
        {
            get
            {
                
                return _clave_nv;
            }
            set
            {
                _clave_nv = value;
            }
        }

        [Display(Name = "Confirme Clave")]
        public string Clave_nvConfirm
        {
            get
            {
                
                return _clave2_nv;
            }
            set
            {
                _clave2_nv = value;
            }
        }
        public bool delete
        {
            get
            {
                
                return _delete_b;
            }
            set
            {
                _delete_b = value;
            }
        }


        private int _ididentifier_i;
        private string _name_nv;
        private string _clave_nv;
        private string _clave2_nv;
        private string _email_nv;
        
        private int _rol_i;
        private DateTime? _FechaCreacion_dt;
        private DateTime? _FechaModificacion_dt;
        private bool _delete_b;
        

        public EntityUser()
        {
            
            ididentifier_i = 0;
            name_nv = "";
            email_nv = "";           
            _rol_i = 1;
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