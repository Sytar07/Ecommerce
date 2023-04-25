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

        public int idconexion { get; set; }

        [Display(Name = "Código Producto")]
        public int idproducto {
            get{
                
                if (_idproducto == -1)
                {
                    return 0;
                }
                else
                {
                    return _idproducto;
                }
            }
            set{
                _idproducto=value;
            } 
        }

        [Display(Name = "Producto")]
        public string nombre_nv {
            get
            {
                
                return _nombre_nv;
            }
            set
            {
                _nombre_nv = value;
            }
        }

        [Display(Name = "Cantidad")]
        public int cantidad_i
        {
            get
            {
                
                return _cantidad_i;
            }
            set
            {
                _cantidad_i = value;
            }
        }

     
        [Display(Name = "Precio")]
        public decimal precio_f
        {
            get
            {
                
                return _precio_f;
            }
            set
            {
                _precio_f = value;
            }
        }


        [Display(Name = "Total")]
        public decimal total_f
        {
            get
            {

                return _total_f;
            }
            set
            {
                _total_f = value;
            }
        }


        private int _idproducto;
        private string _id_producto_nv;
        private string _nombre_nv;
        private int _cantidad_i;
        private string _descripcion_nv;
        private decimal _precio_f;
        private decimal _total_f;

        public EntityCarrito()
        {
            this.idproducto = 0;
            this.nombre_nv = "";
            this.cantidad_i = 0;
            this.precio_f = 0;
            this.total_f = 0;
        }


        public EntityCarrito(int idproducto, string nombre_nv, int cantidad_i, decimal precio_f, decimal total_f)
        {
            this.idproducto = idproducto;
            this.nombre_nv = nombre_nv;
            this.cantidad_i = cantidad_i;
            this.precio_f = precio_f;
            this.total_f = total_f;
        }
    }
    

    public class agregarCarrito
    {
        public int id_conexion { get; set; }
        public int id_producto { get; set; }
        public int cantidad { get; set; }
       
    }
}