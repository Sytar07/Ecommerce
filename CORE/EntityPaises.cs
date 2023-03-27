using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ECOMMERCE.CORE
{
    public class EntityPais : ICommonEntity
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

        public string Id_Country_nv { 
            get{
                // Comprobaciones tipo/null/etc..
                return _Id_Country_nv;
            }
            set{
                _Id_Country_nv=value;
            } 
        }

        public string name_nv {
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
        private string _Id_Country_nv;
        private string _name_nv;
        
        private DateTime? _FechaCreacion_dt;
        private DateTime? _FechaModificacion_dt;
        private bool _delete_b;

        public EntityPais()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            Id_Country_nv = "";
            name_nv = "";
            
            FechaCreacion_dt = DateTime.MinValue;
            FechaModificacion_dt = DateTime.MinValue;
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