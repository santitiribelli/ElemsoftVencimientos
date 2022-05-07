﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GestionProcesosEntities : DbContext
    {
        public GestionProcesosEntities()
            : base("name=GestionProcesosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Clasificadores> Clasificadores { get; set; }
        public DbSet<SeguridadPaginas> SeguridadPaginas { get; set; }
        public DbSet<SeguridadRoles> SeguridadRoles { get; set; }
        public DbSet<SeguridadRolesPaginas> SeguridadRolesPaginas { get; set; }
        public DbSet<SeguridadUsuarios> SeguridadUsuarios { get; set; }
        public DbSet<SeguridadUsuariosPaginas> SeguridadUsuariosPaginas { get; set; }
        public DbSet<VehiculosDocumentos> VehiculosDocumentos { get; set; }
        public DbSet<VehiculosLicencias> VehiculosLicencias { get; set; }
        public DbSet<VencimientosServiciosExternos> VencimientosServiciosExternos { get; set; }
        public DbSet<VencimientosServiciosExternosDetalles> VencimientosServiciosExternosDetalles { get; set; }
        public DbSet<Personas> Personas { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Vehiculos> Vehiculos { get; set; }
    
        public virtual ObjectResult<sp_Alertas_Result> sp_Alertas(Nullable<int> usuario)
        {
            var usuarioParameter = usuario.HasValue ?
                new ObjectParameter("Usuario", usuario) :
                new ObjectParameter("Usuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Alertas_Result>("sp_Alertas", usuarioParameter);
        }
    }
}