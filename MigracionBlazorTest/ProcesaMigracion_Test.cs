using FBSMigracionBlazor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MigracionBlazorTest
{
    [TestClass]
    public class ProcesaMigracion_Test
    {
        private ProcesaMigracion _procesa;

        [TestInitialize]
        public void Inicializar()
        {
            _procesa = new ProcesaMigracion();
        }
        [TestMethod]
        public void ChequeaTextoColumna()
        {            
            var textoProcesado = _procesa.TextoColumna("Secuencial");
            var textoReferencia = "   <Column @bind-Field=\"context.Secuencial\"></Column>";
            StringAssert.ReferenceEquals(textoProcesado, textoReferencia); 
        }
    }
}
