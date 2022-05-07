using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public partial class Personas
    {
        public string NombreCompleto
        {
            get { return Per_Ape; }
        }

        public string DocumentoNombre
        {
            get { return Documento + " - " + Per_Ape; }
        }

        public string Cuit
        {
            get { return Per_Cuil_Cod + "-" + (Per_Cuil_Doc.HasValue ? Per_Cuil_Doc.Value.ToString("00000000") : "") + "-" + Per_Cuil_Con; }
        }

        public string Documento
        {
            get { return Per_Cuil_Doc != null ? (Per_PersonaFisica ? Per_Cuil_Doc.ToString() : Cuit) : Per_Doc_Extranjero != null ? Per_Doc_Extranjero : "SIN IDENTIFICAR"; }
        }
    }
}
