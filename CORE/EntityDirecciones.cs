using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using System.Xml.Linq;

namespace ECOMMERCE.CORE
{
    // JAT: Entidad principal: en singular. Hereda de Interfaz!
    public class EntityDireccion
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

        public int user_i
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _user_i;
            }
            set
            {
                _user_i = value;
            }
        }

        [Display(Name = "Dirección")]
        public string direccion_nv
        { 
            get{
                // Comprobaciones tipo/null/etc..
                return _direccion_nv;
            }
            set{
                _direccion_nv = value;
            } 
        }

        [Display(Name = "Numero o Portal")]
        public int? numero_i {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _numero_i;
            }
            set
            {
                _numero_i = value;
            }
        }

        [Display(Name = "Puerta")]
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

        [Display(Name = "Ciudad")]
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
        
        [Display(Name = "País")]
        public int pais_i
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _pais_i;
            }
            set
            {
                _pais_i = value;
            }
        }

        public IEnumerable<EntityPais>? paises
        {
            get
            {
                return _paises_list;
            }
            set
            {
                _paises_list = value;
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

        private IEnumerable<EntityPais> _paises_list;
        private int _ididentifier_i;
        private string _direccion_nv;
        private string _calle_nv;
        private int? _numero_i;
        private string _puerta_nv;
        private string _ciudad_nv;
        private int _pais_i;
        private bool _delete_b;
        private int _user_i;

        // JAT: contructor
        public EntityDireccion()
        {
            // JAT: iniciador/ constructor. 
            ididentifier_i = 0;
            direccion_nv = "";
            numero_i = null;
            puerta_nv = "";
            ciudad_nv = "";
            pais_i = 0;
            user_i = 0;

        }

    }

    public class EntityDirecciones
    {
        public IList<EntityDireccion> lista;

        public EntityDirecciones()
        {
            if (lista == null)
            {
                lista = new List<EntityDireccion>();
            }
        }
    }

    
}