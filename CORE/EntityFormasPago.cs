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
    public class EntityFormaPago
    {
        [Display(Name = "Código")]
        public int ididentifier_i
        {
            get
            {
                
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

        [Display(Name = "Nombre")]
        public string name_nv
        {
            get
            {
                
                return _name_nv;
            }
            set
            {
                _name_nv = value;
            }
        }

        [Display(Name = "Tipo o Categoria")]
        public short type_i
        {
            get
            {
                
                return _type_i;
            }
            set
            {
                _type_i = value;
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
        private short _type_i;
        
        private DateTime? _FechaCreacion_dt;
        private DateTime? _FechaModificacion_dt;
        private bool _delete_b;
        public EntityFormaPago()
        {
            
            ididentifier_i = 0;
            name_nv = "";
            type_i = 0;
            

        }

    }
    public class EntityFormasPago
    {
        public IList<EntityFormaPago> lista;

        public EntityFormasPago()
        {
            if (lista == null)
            {
                lista = new List<EntityFormaPago>();
            }
        }
    }


}