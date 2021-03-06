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
    
    public partial class Personas
    {
        public Personas()
        {
            this.Proveedores = new HashSet<Proveedores>();
            this.SeguridadUsuarios1 = new HashSet<SeguridadUsuarios>();
           

        }
    
        public int Per_Id { get; set; }
        public string Per_SoloApe { get; set; }
        public string Per_SoloNom { get; set; }
        public string Per_Ape { get; set; }
        public string Per_Alias { get; set; }
        public Nullable<int> Per_Cuil_Cod { get; set; }
        public Nullable<int> Per_Cuil_Doc { get; set; }
        public Nullable<int> Per_Cuil_Con { get; set; }
        public string Per_Doc_Extranjero { get; set; }
        public Nullable<int> Clas_Sexo_Id { get; set; }
        public Nullable<int> Clas_Nacionalidad_Id { get; set; }
        public Nullable<System.DateTime> Per_FecDes { get; set; }
        public Nullable<System.DateTime> Per_FecHas { get; set; }
        public string Per_Direccion { get; set; }
        public Nullable<int> Clas_Localidad_Id { get; set; }
        public string Per_TE_Celular { get; set; }
        public string Per_MAIL { get; set; }
        public bool Per_PersonaFisica { get; set; }
        public bool Per_DatosCompletos { get; set; }
        public bool Per_DatosVerificados { get; set; }
        public bool Per_RegistroComodin { get; set; }
        public string Per_Obs { get; set; }
        public string Per_Foto { get; set; }
        public System.DateTime AudFecha { get; set; }
        public int AudUser { get; set; }
    
        public virtual Clasificadores Clasificadores { get; set; }
        public virtual Clasificadores Clasificadores1 { get; set; }
        public virtual SeguridadUsuarios SeguridadUsuarios { get; set; }
        public virtual ICollection<Proveedores> Proveedores { get; set; }
        public virtual Proveedores Proveedores1 { get; set; }
        public virtual ICollection<SeguridadUsuarios> SeguridadUsuarios1 { get; set; }

        
    }
}
