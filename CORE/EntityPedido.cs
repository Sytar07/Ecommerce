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
    public class EntityPedido
    {
        public int conexion { get; set; }
        public int ididentifier_i { get; set; }
        public int id_direccion { get; set; }
        public int id_user { get; set; }

        public DateTime Fecha_Pedido { get; set; }
        public DateTime? Fecha_Envio { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Total { get; set; }
        public decimal? Iva { get; set; }
        public string? Estado { get; set; }



        // Auxiliares.
        public EntityDireccion? Direccion= new EntityDireccion();        
        public EntityFormaPago? FormaPago= new EntityFormaPago();
        public EntityUser? Usuario = new EntityUser();
        public EntityTarjeta? Tarjeta= new EntityTarjeta();

        public List<EntityFormaPago>? FormaPagos= new List<EntityFormaPago>();
        public List<EntityDireccion>? Direcciones = new List<EntityDireccion>();
        
    }
    
    public class EntityTarjeta
    {
        public int iduser { get; set; }
        public int iddireccion { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(10)]
        [Display(Name = "Nombre del titular")]
        public string NombreTitular
        {
            get{
                    return _NombreTitular;
            }
            set{
                _NombreTitular = value;
            } 
        }

        [Required(ErrorMessage = "Campo obligatorio")]
        [CreditCard(ErrorMessage = "El número de tarjeta no es válido.")]
        [Display(Name = "Numero de Tarjeta")]
        public string NumeroTarjeta
        {
            get
            {
                
                return _NumeroTarjeta;
            }
            set
            {
                _NumeroTarjeta = value;
            }
        }
        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "Fecha de Vencimiento")]
        public DateTime FechaVencimiento
        {
            get
            {
                
                return _FechaVencimiento;
            }
            set
            {
                _FechaVencimiento = value;
            }
        }

        [Required(ErrorMessage = "Campo obligatorio")]
        [Display(Name = "CVV")]
        public int CVV
        {
            get
            {
                
                return _CVV;
            }
            set
            {
                _CVV = value;
            }
        }

        public static bool ValidateCreditCardNumber(string creditCardNumber)
        {
            int sum = 0;
            bool alternate = false;
            for (int i = creditCardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(creditCardNumber.Substring(i, 1));
                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n = (n % 10) + 1;
                    }
                }
                sum += n;
                alternate = !alternate;
            }
            return (sum % 10 == 0);
        }

        private string _NombreTitular;
        private string _NumeroTarjeta;
        private DateTime _FechaVencimiento;
        private int _CVV;


    }
    

}