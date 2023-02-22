using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ECOMMERCE.CORE
{
    // JAT: Entidad principal: en singular. Hereda de Interfaz!
    public class EntityDireccion : ICommonEntity
    {
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

        public string calle_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _calle_nv;
            }
            set
            {
                _calle_nv = value;
            }
        }

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

        public string pais_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _pais_nv;
            }
            set
            {
                _pais_nv = value;
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
        private string _direccion_nv;
        private string _calle_nv;
        private int? _numero_i;
        private string _puerta_nv;
        private string _ciudad_nv;
        private string _pais_nv;
        private string _Owner_nv;
        private DateTime? _FechaCreacion_dt;
        private DateTime? _FechaModificacion_dt;
        private bool _delete_b;

        // JAT: contructor
        public EntityDireccion()
        {
            // JAT: iniciador/ constructor. 
            ididentifier_i = 0;
            direccion_nv = "";
            calle_nv = "";
            numero_i = null;
            puerta_nv = "";
            ciudad_nv = "";
            pais_nv = "";
            Owner_nv = "";
            FechaCreacion_dt = DateTime.MinValue;
            FechaModificacion_dt = DateTime.MinValue;

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