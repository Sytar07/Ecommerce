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
    public class EntityConexion 
    {
        [Display(Name = "Código")]
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
        
        [Display(Name = "IP")]
        public string? ip
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                if (_ididentifier_i == -1)
                {
                    return "";
                }
                else
                {
                    return  _ip;
                }
            }
            set
            {
                _ip = value;
            }
        }

        public int ID_Carrito_i { 
            get{
                // Comprobaciones tipo/null/etc..
                return _ID_Carrito_i;
            }
            set{
                _ID_Carrito_i = value;
            } 
        }

        [Display(Name = "Fecha Inicio")]
        public DateTime? FechaInicio_dt
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _FechaInicio_dt;
            }
            set
            {
                _FechaInicio_dt = value;
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
        public int _ID_Carrito_i;
        public bool _delete_b;
        public string? _ip;
        public DateTime? _FechaInicio_dt;

        public EntityConexion()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            ID_Carrito_i = 0;
            FechaInicio_dt = DateTime.MinValue;
            ip = "";
        }

    }
    public class EntityConexiones
    {
        public IList<EntityConexion> lista;
       
        public EntityConexiones()
        {
            if (lista == null)
            {
                lista = new List<EntityConexion>();
            }
        }
    }

    
}