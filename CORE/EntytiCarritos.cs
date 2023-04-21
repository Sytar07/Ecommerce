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
    public class EntityCarrito 
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
        public string nombre_nv { 
            get{
                
                return _nombre_nv;
            }
            set{
                _nombre_nv = value;
            } 
        }

        [Display(Name = "Usuerio")]
        public int ID_usuario_i
        {
            get
            {
                
                return _ID_Usuario_i;
            }
            set
            {
                _ID_Usuario_i = value;
            }
        }
        [Display(Name = "Dirección")]
        public string direccion_nv
        {
            get
            {
                
                return _direccion_nv;
            }
            set
            {
                _direccion_nv = value;
            }
        }

        [Display(Name = "Producto")]
        public int ID_Producto_i
        {
            get
            {
                
                return _ID_Producto_i;
            }
            set
            {
                _ID_Producto_i = value;
            }
        }
        public int Cantidad_i
        {
            get
            {
                
                return _Cantidad_i;
            }
            set
            {
                _Cantidad_i = value;
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
        private int _ID_Usuario_i;
        private int _ID_Producto_i;
        private string _nombre_nv;
        
        private int _Cantidad_i;
        private string _direccion_nv;
        private DateTime? _FechaCreacion_dt;
        private DateTime? _FechaModificacion_dt;
        
        private bool _delete_b;
        
        public EntityCarrito()
        {
            
            ididentifier_i = 0;
            ID_usuario_i = 0;
            ID_Producto_i = 0;
            nombre_nv = "";
            
            Cantidad_i= 0;
            direccion_nv = "";

        }

    }
    public class EntityCarritos
    {
        public IList<EntityCarrito> lista;

        public EntityCarritos()
        {
            if (lista == null)
            {
                lista = new List<EntityCarrito>();
            }
        }
    }

    
}