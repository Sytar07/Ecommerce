namespace ECOMMERCE.CORE
{
    // Declaramos Interfaz para que las clases LO VEAN!!!
    public interface ICommonEntity
    {
        #region PROPERTIES
        public int ididentifier_i { get; set; }
        public string name_nv { get; set; }
        #endregion


        #region METHODS
        public void Init();

        #endregion
    }



    // CLASE REAL. Esta hace cosas (EXAMPLE)
    public class CommonEntity :  ICommonEntity
    {
        public int ididentifier_i { get; set; }
        public string name_nv { get; set; }

        public void Init()
        {
            // Aqui hago cosas.
            ididentifier_i = 0;
            name_nv = "";

        }

    }
}