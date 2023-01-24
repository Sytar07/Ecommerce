using DAL;

namespace ECOMMERCE.DAL
{
    public class Init
    {

        public void TESTPAISES() {
            PaisesBD paisesBD = new PaisesBD();

            foreach(CORE.EntityPais e in paisesBD.GETALLPAISES().list)
            {
                Console.WriteLine("PAIS:" + e.ididentifier_i + " NOMBRE:" + e.name_nv);
            }

        }
    }
}