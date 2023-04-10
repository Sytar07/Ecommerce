using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ECOMMERCE.CORE
{
    public class EntityImagen : ICommonEntity
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

        public int id_producto
        {
            get
            {
                // Comprobaciones tipo/null/etc..
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
        public string path_nv {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _path_nv;
            }
            set
            {
                _path_nv = value;
            }
        }

        public string tipo_nv
        {
            get
            {
                // Comprobaciones tipo/null/etc..
                return _tipo_nv;
            }
            set
            {
                _tipo_nv = value;
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
        private string _path_nv;
        private int _id_producto;
        private string _tipo_nv;
        
        private DateTime? _FechaCreacion_dt;
        private DateTime? _FechaModificacion_dt;
        private bool _delete_b;



        public  EntityImagen()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            path_nv = "";
            tipo_nv = "";
            id_producto = 0;
            FechaCreacion_dt = DateTime.MinValue;
            FechaModificacion_dt = DateTime.MinValue;

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