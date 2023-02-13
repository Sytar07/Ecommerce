namespace ECOMMERCE.CORE
{
    // Declaramos Interfaz para que las clases LO VEAN!!!
    public interface ICommonEntity
    {
        #region PROPERTIES
        public int ididentifier_i { get; set; }
        public DateTime? FechaCreacion_dt { get; set; }
        public DateTime? FechaModificacion_dt { get; set; }
        public string Owner_nv { get; set; }

        #endregion


        #region METHODS
        

        #endregion
    }

}