﻿using System;
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

        public int iduser
        {
            get
            {
                return _id_user;
            }
            set
            {
                _id_user = value;
            }
        }


        [Display(Name = "Fecha Inicio")]
        public DateTime? FechaInicio_dt
        {
            get
            {
                
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
                
                return _delete_b;
            }
            set
            {
                _delete_b = value;
            }
        }


        private int _ididentifier_i;
        private int _id_user;
        public bool _delete_b;
        public string? _ip;
        public DateTime? _FechaInicio_dt;

        public EntityConexion()
        {
            
            ididentifier_i = 0;
            iduser = 0;
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