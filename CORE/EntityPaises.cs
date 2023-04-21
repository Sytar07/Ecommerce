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
    public class EntityPais
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
            get
            {
                
                return _name_nv;
            }
            set
            {
                _name_nv = value;
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
        private string _Id_Country_nv;
        private string _name_nv;
        
        private DateTime? _FechaCreacion_dt;
        private DateTime? _FechaModificacion_dt;
        private bool _delete_b;

        public EntityPais()
        {
            
            ididentifier_i = 0;
            name_nv = "";
            
        }

    }
    public class EntityPaises
    {
        public IList<EntityPais> lista;
       
        public EntityPaises()
        {
            if (lista == null)
            {
                lista = new List<EntityPais>();
            }
        }
    }

    
}