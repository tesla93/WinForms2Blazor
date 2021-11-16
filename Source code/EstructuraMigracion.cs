using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBSMigracionBlazor
{
    public class EstructuraMigracion
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public string NombreControl { get; set; } = string.Empty;
        public string Tipo { get; set; }
        public string Texto { get; set; }
        public string Propiedad { get; set; }
        public string ValorPropiedad { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Componente { get; set; }

    }
}
