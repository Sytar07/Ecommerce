using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
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
    public class EntityImagen
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

        [Display(Name = "Código Producto")]
        public int id_producto
        {
            get
            {
                
                if (_id_producto == -1)
                {
                    return 0;
                }
                else
                {
                    return _id_producto;
                }
            }
            set
            {
                _id_producto = value;
            }
        }

        [Display(Name = "Ruta URL")]
        public string? path_nv {
            get
            {
                
                return _path_nv;
            }
            set
            {
                _path_nv = value;
            }
        }

        [Display(Name = "Tipo o Categoria")]
        public string tipo_nv
        {
            get
            {
                
                return _tipo_nv;
            }
            set
            {
                _tipo_nv = value;
            }
        }

        public IFormFile? imagen { get; set; }
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
        private string? _path_nv;
        private int _id_producto;
        private string _tipo_nv;
        
        private bool _delete_b;

        public  EntityImagen()
        {
            
            ididentifier_i = 0;
            path_nv = "";
            tipo_nv = "";
            id_producto = 0;

        }

    }
    public class EntityImagenes
    {
        public IList<EntityImagen> lista;

        public EntityImagenes()
        {
            if (lista == null)
            {
                lista = new List<EntityImagen>();
            }
        }
    }

    
}