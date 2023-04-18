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
    public class EntityProducto
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

        [Display(Name = "Nombre")]
        public string nombre_nv {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _nombre_nv;
            }
            set
            {
                _nombre_nv = value;
            }
        }

        [Display(Name = "Stock")]
        public int stock_f
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _stock_f;
            }
            set
            {
                _stock_f = value;
            }
        }

        [Display(Name = "Descripción")]
        public string descripcion_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _descripcion_nv;
            }
            set
            {
                _descripcion_nv = value;
            }
        }

        [Display(Name = "Precio")]
        public decimal precio_f
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _precio_f;
            }
            set
            {
                _precio_f = value;
            }
        }

       public IEnumerable<EntityImagen> imagenes
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _imagenes;
            }
            set
            {
                _imagenes = value;
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

        private IEnumerable<EntityImagen> _imagenes;
        private int _ididentifier_i;
        private string _id_producto_nv;
        private string _nombre_nv;
        private int _stock_f;
        private string _descripcion_nv;
        private decimal _precio_f;
        
        private bool _delete_b;



        public EntityProducto()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            
            nombre_nv = "";
            stock_f = 0;
            descripcion_nv = "";
            precio_f = 0;
            

        }

    }
    public class EntityProductos
    {
        public IList<EntityProducto> lista;

        public EntityProductos()
        {
            if (lista == null)
            {
                lista = new List<EntityProducto>();
            }
        }
    }

    
}