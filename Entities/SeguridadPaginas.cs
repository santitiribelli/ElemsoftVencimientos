//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class SeguridadPaginas
    {
        public SeguridadPaginas()
        {
            this.SeguridadRolesPaginas = new HashSet<SeguridadRolesPaginas>();
            this.SeguridadUsuariosPaginas = new HashSet<SeguridadUsuariosPaginas>();
        }
    
        public int SegPag_Id { get; set; }
        public string SegPag_Nom { get; set; }
        public string SegPag_NomDesc { get; set; }
        public bool SegPag_Act { get; set; }
        public string SegPag_Cod { get; set; }
    
        public virtual ICollection<SeguridadRolesPaginas> SeguridadRolesPaginas { get; set; }
        public virtual ICollection<SeguridadUsuariosPaginas> SeguridadUsuariosPaginas { get; set; }
    }
}