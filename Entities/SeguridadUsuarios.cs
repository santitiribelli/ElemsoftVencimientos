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
    
    public partial class SeguridadUsuarios
    {
        public SeguridadUsuarios()
        {
            this.Clasificadores = new HashSet<Clasificadores>();
            this.SeguridadUsuariosPaginas = new HashSet<SeguridadUsuariosPaginas>();
            this.VehiculosDocumentos = new HashSet<VehiculosDocumentos>();
            this.VehiculosLicencias = new HashSet<VehiculosLicencias>();
            this.VencimientosServiciosExternos = new HashSet<VencimientosServiciosExternos>();
            this.VencimientosServiciosExternosDetalles = new HashSet<VencimientosServiciosExternosDetalles>();
            this.Personas = new HashSet<Personas>();
            this.Proveedores = new HashSet<Proveedores>();
            this.Vehiculos = new HashSet<Vehiculos>();
        }
    
        public int SegUsu_Id { get; set; }
        public Nullable<int> Per_Id { get; set; }
        public string SegUsu_Pass { get; set; }
        public Nullable<int> SegRol_Id { get; set; }
        public System.DateTime SegUsu_FecAlta { get; set; }
        public Nullable<System.DateTime> SegUsu_FecBaja { get; set; }
        public string Salt { get; set; }
    
        public virtual ICollection<Clasificadores> Clasificadores { get; set; }
        public virtual SeguridadRoles SeguridadRoles { get; set; }
        public virtual ICollection<SeguridadUsuariosPaginas> SeguridadUsuariosPaginas { get; set; }
        public virtual ICollection<VehiculosDocumentos> VehiculosDocumentos { get; set; }
        public virtual ICollection<VehiculosLicencias> VehiculosLicencias { get; set; }
        public virtual ICollection<VencimientosServiciosExternos> VencimientosServiciosExternos { get; set; }
        public virtual ICollection<VencimientosServiciosExternosDetalles> VencimientosServiciosExternosDetalles { get; set; }
        public virtual ICollection<Personas> Personas { get; set; }
        public virtual Personas Personas1 { get; set; }
        public virtual ICollection<Proveedores> Proveedores { get; set; }
        public virtual ICollection<Vehiculos> Vehiculos { get; set; }
    }
}
