
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/10/2020 13:35:10
-- Generated from EDMX file: C:\Dev\PruebaVerificacionesExternas\DataAccess\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Test_VencimientosExternos];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Clasificadores_Clasificadores]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Clasificadores] DROP CONSTRAINT [FK_Clasificadores_Clasificadores];
GO
IF OBJECT_ID(N'[dbo].[FK_Clasificadores_SeguridadUsuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Clasificadores] DROP CONSTRAINT [FK_Clasificadores_SeguridadUsuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_Personas_ClasificadoresLocalidades]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas] DROP CONSTRAINT [FK_Personas_ClasificadoresLocalidades];
GO
IF OBJECT_ID(N'[dbo].[FK_Personas_ClasificadoresNacionalidad]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas] DROP CONSTRAINT [FK_Personas_ClasificadoresNacionalidad];
GO
IF OBJECT_ID(N'[dbo].[FK_Personas_SeguridadUsuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Personas] DROP CONSTRAINT [FK_Personas_SeguridadUsuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_Proveedores_Clasificadores_ProEst]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Proveedores] DROP CONSTRAINT [FK_Proveedores_Clasificadores_ProEst];
GO
IF OBJECT_ID(N'[dbo].[FK_Proveedores_Personas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Proveedores] DROP CONSTRAINT [FK_Proveedores_Personas];
GO
IF OBJECT_ID(N'[dbo].[FK_Proveedores_SeguridadUsuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Proveedores] DROP CONSTRAINT [FK_Proveedores_SeguridadUsuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_SeguridadRolesPaginas_SeguridadPaginas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SeguridadRolesPaginas] DROP CONSTRAINT [FK_SeguridadRolesPaginas_SeguridadPaginas];
GO
IF OBJECT_ID(N'[dbo].[FK_SeguridadRolesPaginas_SeguridadRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SeguridadRolesPaginas] DROP CONSTRAINT [FK_SeguridadRolesPaginas_SeguridadRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_SeguridadUsuarios_Personas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SeguridadUsuarios] DROP CONSTRAINT [FK_SeguridadUsuarios_Personas];
GO
IF OBJECT_ID(N'[dbo].[FK_SeguridadUsuarios_SeguridadRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SeguridadUsuarios] DROP CONSTRAINT [FK_SeguridadUsuarios_SeguridadRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_SeguridadUsuariosPaginas_SeguridadPaginas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SeguridadUsuariosPaginas] DROP CONSTRAINT [FK_SeguridadUsuariosPaginas_SeguridadPaginas];
GO
IF OBJECT_ID(N'[dbo].[FK_SeguridadUsuariosPaginas_SeguridadUsuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SeguridadUsuariosPaginas] DROP CONSTRAINT [FK_SeguridadUsuariosPaginas_SeguridadUsuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_Vehiculos_Proveedores]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vehiculos] DROP CONSTRAINT [FK_Vehiculos_Proveedores];
GO
IF OBJECT_ID(N'[dbo].[FK_Vehiculos_SeguridadUsuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vehiculos] DROP CONSTRAINT [FK_Vehiculos_SeguridadUsuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_VehiculosDocumentos_Clasificadores]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VehiculosDocumentos] DROP CONSTRAINT [FK_VehiculosDocumentos_Clasificadores];
GO
IF OBJECT_ID(N'[dbo].[FK_VehiculosDocumentos_SeguridadUsuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VehiculosDocumentos] DROP CONSTRAINT [FK_VehiculosDocumentos_SeguridadUsuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_VehiculosDocumentos_Vehiculos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VehiculosDocumentos] DROP CONSTRAINT [FK_VehiculosDocumentos_Vehiculos];
GO
IF OBJECT_ID(N'[dbo].[FK_VehiculosLicencias_ClasificadoresTipoLicencia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VehiculosLicencias] DROP CONSTRAINT [FK_VehiculosLicencias_ClasificadoresTipoLicencia];
GO
IF OBJECT_ID(N'[dbo].[FK_VehiculosLicencias_SeguridadUsuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VehiculosLicencias] DROP CONSTRAINT [FK_VehiculosLicencias_SeguridadUsuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_VehiculosLicencias_Vehiculos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VehiculosLicencias] DROP CONSTRAINT [FK_VehiculosLicencias_Vehiculos];
GO
IF OBJECT_ID(N'[dbo].[FK_VencimientosServiciosExternos_SeguridadUsuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VencimientosServiciosExternos] DROP CONSTRAINT [FK_VencimientosServiciosExternos_SeguridadUsuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_VencimientosServiciosExternosDetalles_Clasificadores]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VencimientosServiciosExternosDetalles] DROP CONSTRAINT [FK_VencimientosServiciosExternosDetalles_Clasificadores];
GO
IF OBJECT_ID(N'[dbo].[FK_VencimientosServiciosExternosDetalles_SeguridadUsuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VencimientosServiciosExternosDetalles] DROP CONSTRAINT [FK_VencimientosServiciosExternosDetalles_SeguridadUsuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_VencimientosServiciosExternosDetalles_VencimientosServiciosExternos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VencimientosServiciosExternosDetalles] DROP CONSTRAINT [FK_VencimientosServiciosExternosDetalles_VencimientosServiciosExternos];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Clasificadores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clasificadores];
GO
IF OBJECT_ID(N'[dbo].[Personas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Personas];
GO
IF OBJECT_ID(N'[dbo].[Proveedores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Proveedores];
GO
IF OBJECT_ID(N'[dbo].[SeguridadPaginas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SeguridadPaginas];
GO
IF OBJECT_ID(N'[dbo].[SeguridadRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SeguridadRoles];
GO
IF OBJECT_ID(N'[dbo].[SeguridadRolesPaginas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SeguridadRolesPaginas];
GO
IF OBJECT_ID(N'[dbo].[SeguridadUsuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SeguridadUsuarios];
GO
IF OBJECT_ID(N'[dbo].[SeguridadUsuariosPaginas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SeguridadUsuariosPaginas];
GO
IF OBJECT_ID(N'[dbo].[Vehiculos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vehiculos];
GO
IF OBJECT_ID(N'[dbo].[VehiculosDocumentos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VehiculosDocumentos];
GO
IF OBJECT_ID(N'[dbo].[VehiculosLicencias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VehiculosLicencias];
GO
IF OBJECT_ID(N'[dbo].[VencimientosServiciosExternos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VencimientosServiciosExternos];
GO
IF OBJECT_ID(N'[dbo].[VencimientosServiciosExternosDetalles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VencimientosServiciosExternosDetalles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Clasificadores'
CREATE TABLE [dbo].[Clasificadores] (
    [Clas_Id] int IDENTITY(1,1) NOT NULL,
    [Clas_Desc] varchar(250)  NOT NULL,
    [Clas_Abrev] varchar(5)  NULL,
    [Clas_Tabla] varchar(35)  NOT NULL,
    [Clas_Detalle] varchar(max)  NULL,
    [Clas_DatoOblig] bit  NOT NULL,
    [Clas_Padre] int  NULL,
    [Clas_FecAlta] datetime  NOT NULL,
    [Clas_FecBaja] datetime  NULL,
    [Clas_Interno] bit  NOT NULL,
    [AudFecha] datetime  NOT NULL,
    [AudUser] int  NOT NULL
);
GO

-- Creating table 'Personas'
CREATE TABLE [dbo].[Personas] (
    [Per_Id] int IDENTITY(1,1) NOT NULL,
    [Per_SoloApe] varchar(50)  NOT NULL,
    [Per_SoloNom] varchar(50)  NULL,
    [Per_Ape] varchar(100)  NOT NULL,
    [Per_Alias] varchar(50)  NULL,
    [Per_Cuil_Cod] int  NULL,
    [Per_Cuil_Doc] int  NULL,
    [Per_Cuil_Con] int  NULL,
    [Per_Doc_Extranjero] varchar(25)  NULL,
    [Clas_Sexo_Id] int  NULL,
    [Clas_Nacionalidad_Id] int  NULL,
    [Per_FecDes] datetime  NULL,
    [Per_FecHas] datetime  NULL,
    [Per_Direccion] varchar(500)  NULL,
    [Clas_Localidad_Id] int  NULL,
    [Per_TE_Celular] varchar(50)  NULL,
    [Per_MAIL] varchar(50)  NULL,
    [Per_PersonaFisica] bit  NOT NULL,
    [Per_DatosCompletos] bit  NOT NULL,
    [Per_DatosVerificados] bit  NOT NULL,
    [Per_RegistroComodin] bit  NOT NULL,
    [Per_Obs] varchar(max)  NULL,
    [Per_Foto] varchar(50)  NULL,
    [AudFecha] datetime  NOT NULL,
    [AudUser] int  NOT NULL
);
GO

-- Creating table 'Proveedores'
CREATE TABLE [dbo].[Proveedores] (
    [Prov_Id] int IDENTITY(1,1) NOT NULL,
    [Per_Id] int  NOT NULL,
    [Prov_Codigo] varchar(5)  NULL,
    [Clas_ProEst_Id] int  NOT NULL,
    [Prov_CtaCorriente] bit  NOT NULL,
    [Prov_Propio] bit  NOT NULL,
    [Prov_DatosVerificados] bit  NOT NULL,
    [Prov_Obs] varchar(max)  NULL,
    [AudFecha] datetime  NOT NULL,
    [AudUser] int  NOT NULL
);
GO

-- Creating table 'SeguridadPaginas'
CREATE TABLE [dbo].[SeguridadPaginas] (
    [SegPag_Id] int IDENTITY(1,1) NOT NULL,
    [SegPag_Nom] varchar(50)  NOT NULL,
    [SegPag_NomDesc] varchar(100)  NULL,
    [SegPag_Act] bit  NOT NULL,
    [SegPag_Cod] varchar(10)  NULL
);
GO

-- Creating table 'SeguridadRoles'
CREATE TABLE [dbo].[SeguridadRoles] (
    [SegRol_Id] int IDENTITY(1,1) NOT NULL,
    [SegRol_Descr] varchar(25)  NULL
);
GO

-- Creating table 'SeguridadRolesPaginas'
CREATE TABLE [dbo].[SeguridadRolesPaginas] (
    [SegRolPag_Id] int IDENTITY(1,1) NOT NULL,
    [SegRol_Id] int  NOT NULL,
    [SegPag_Id] int  NOT NULL,
    [SegRolPag_Ver] bit  NOT NULL,
    [SegRolPag_Alta] bit  NOT NULL,
    [SegRolPag_Expo] bit  NOT NULL
);
GO

-- Creating table 'SeguridadUsuarios'
CREATE TABLE [dbo].[SeguridadUsuarios] (
    [SegUsu_Id] int IDENTITY(1,1) NOT NULL,
    [Per_Id] int  NULL,
    [SegUsu_Pass] varchar(250)  NOT NULL,
    [SegRol_Id] int  NULL,
    [SegUsu_FecAlta] datetime  NOT NULL,
    [SegUsu_FecBaja] datetime  NULL,
    [Salt] varchar(50)  NULL
);
GO

-- Creating table 'SeguridadUsuariosPaginas'
CREATE TABLE [dbo].[SeguridadUsuariosPaginas] (
    [SegUsuPag_Id] int IDENTITY(1,1) NOT NULL,
    [SegUsu_Id] int  NOT NULL,
    [SegPag_Id] int  NOT NULL,
    [SegPagUsu_Ver] bit  NOT NULL,
    [SegPagUsu_Alta] bit  NOT NULL,
    [SegPagUsu_Expo] bit  NOT NULL
);
GO

-- Creating table 'Vehiculos'
CREATE TABLE [dbo].[Vehiculos] (
    [Veh_Id] int IDENTITY(1,1) NOT NULL,
    [Veh_Patente] varchar(15)  NOT NULL,
    [Prov_Id] int  NULL,
    [Veh_FechaDesde] datetime  NOT NULL,
    [Veh_FechaHasta] datetime  NULL,
    [Veh_PasajerosCantidad] int  NOT NULL,
    [Veh_DatosVerificados] bit  NOT NULL,
    [Veh_Obs] varchar(500)  NULL,
    [AudFecha] datetime  NOT NULL,
    [AudUser] int  NOT NULL
);
GO

-- Creating table 'VehiculosDocumentos'
CREATE TABLE [dbo].[VehiculosDocumentos] (
    [VehDoc_Id] int IDENTITY(1,1) NOT NULL,
    [Veh_Id] int  NOT NULL,
    [Clas_TipoDocumento_Id] int  NOT NULL,
    [VehDoc_FechaAprobado] datetime  NOT NULL,
    [VehDoc_FechaVencimiento] datetime  NULL,
    [VehDoc_FechaAviso] datetime  NULL,
    [GeneraAlerta] bit  NOT NULL,
    [VehDoc_ArchivoAdjunto] varchar(50)  NULL,
    [VehDoc_DatosVerificados] bit  NOT NULL,
    [VehDoc_Obs] varchar(max)  NULL,
    [AudFecha] datetime  NOT NULL,
    [AudUser] int  NOT NULL
);
GO

-- Creating table 'VehiculosLicencias'
CREATE TABLE [dbo].[VehiculosLicencias] (
    [VehLic_Id] int IDENTITY(1,1) NOT NULL,
    [Veh_Id] int  NOT NULL,
    [Clas_TipoLicencia_Id] int  NOT NULL,
    [VehLic_FechaDesde] datetime  NOT NULL,
    [VehLic_FechaHasta] datetime  NULL,
    [GeneraAlerta] bit  NOT NULL,
    [VehLic_DatosVerificados] bit  NOT NULL,
    [VehLic_Obs] varchar(max)  NULL,
    [AudFecha] datetime  NOT NULL,
    [AudUser] int  NOT NULL
);
GO

-- Creating table 'VencimientosServiciosExternosSet'
CREATE TABLE [dbo].[VencimientosServiciosExternosSet] (
    [VenSerExt_Id] int IDENTITY(1,1) NOT NULL,
    [VenSerExt_Prov_Id] varchar(100)  NOT NULL,
    [VenSerExt_Identificacion] varchar(100)  NOT NULL,
    [VenSerExt_Descripcion] varchar(max)  NOT NULL,
    [AudFecha] datetime  NOT NULL,
    [AudUser] int  NOT NULL,
    [VenSerExt_Prov_Nombre] varchar(max)  NOT NULL
);
GO

-- Creating table 'VencimientosServiciosExternosDetalles'
CREATE TABLE [dbo].[VencimientosServiciosExternosDetalles] (
    [VenSerExtDet_Id] int IDENTITY(1,1) NOT NULL,
    [VenSerExt_Id] int  NOT NULL,
    [VenSerExtDet_FechaPagado] datetime  NULL,
    [VenSerExtDet_ValorPagado] decimal(18,2)  NOT NULL,
    [Clas_MonedasTipos_Id] int  NULL,
    [VenSerExtDet_FechaVencimiento] datetime  NOT NULL,
    [VenSerExtDet_Obs] varchar(max)  NULL,
    [AudFecha] datetime  NOT NULL,
    [AudUser] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Clas_Id] in table 'Clasificadores'
ALTER TABLE [dbo].[Clasificadores]
ADD CONSTRAINT [PK_Clasificadores]
    PRIMARY KEY CLUSTERED ([Clas_Id] ASC);
GO

-- Creating primary key on [Per_Id] in table 'Personas'
ALTER TABLE [dbo].[Personas]
ADD CONSTRAINT [PK_Personas]
    PRIMARY KEY CLUSTERED ([Per_Id] ASC);
GO

-- Creating primary key on [Prov_Id] in table 'Proveedores'
ALTER TABLE [dbo].[Proveedores]
ADD CONSTRAINT [PK_Proveedores]
    PRIMARY KEY CLUSTERED ([Prov_Id] ASC);
GO

-- Creating primary key on [SegPag_Id] in table 'SeguridadPaginas'
ALTER TABLE [dbo].[SeguridadPaginas]
ADD CONSTRAINT [PK_SeguridadPaginas]
    PRIMARY KEY CLUSTERED ([SegPag_Id] ASC);
GO

-- Creating primary key on [SegRol_Id] in table 'SeguridadRoles'
ALTER TABLE [dbo].[SeguridadRoles]
ADD CONSTRAINT [PK_SeguridadRoles]
    PRIMARY KEY CLUSTERED ([SegRol_Id] ASC);
GO

-- Creating primary key on [SegRolPag_Id] in table 'SeguridadRolesPaginas'
ALTER TABLE [dbo].[SeguridadRolesPaginas]
ADD CONSTRAINT [PK_SeguridadRolesPaginas]
    PRIMARY KEY CLUSTERED ([SegRolPag_Id] ASC);
GO

-- Creating primary key on [SegUsu_Id] in table 'SeguridadUsuarios'
ALTER TABLE [dbo].[SeguridadUsuarios]
ADD CONSTRAINT [PK_SeguridadUsuarios]
    PRIMARY KEY CLUSTERED ([SegUsu_Id] ASC);
GO

-- Creating primary key on [SegUsuPag_Id] in table 'SeguridadUsuariosPaginas'
ALTER TABLE [dbo].[SeguridadUsuariosPaginas]
ADD CONSTRAINT [PK_SeguridadUsuariosPaginas]
    PRIMARY KEY CLUSTERED ([SegUsuPag_Id] ASC);
GO

-- Creating primary key on [Veh_Id] in table 'Vehiculos'
ALTER TABLE [dbo].[Vehiculos]
ADD CONSTRAINT [PK_Vehiculos]
    PRIMARY KEY CLUSTERED ([Veh_Id] ASC);
GO

-- Creating primary key on [VehDoc_Id] in table 'VehiculosDocumentos'
ALTER TABLE [dbo].[VehiculosDocumentos]
ADD CONSTRAINT [PK_VehiculosDocumentos]
    PRIMARY KEY CLUSTERED ([VehDoc_Id] ASC);
GO

-- Creating primary key on [VehLic_Id] in table 'VehiculosLicencias'
ALTER TABLE [dbo].[VehiculosLicencias]
ADD CONSTRAINT [PK_VehiculosLicencias]
    PRIMARY KEY CLUSTERED ([VehLic_Id] ASC);
GO

-- Creating primary key on [VenSerExt_Id] in table 'VencimientosServiciosExternosSet'
ALTER TABLE [dbo].[VencimientosServiciosExternosSet]
ADD CONSTRAINT [PK_VencimientosServiciosExternosSet]
    PRIMARY KEY CLUSTERED ([VenSerExt_Id] ASC);
GO

-- Creating primary key on [VenSerExtDet_Id] in table 'VencimientosServiciosExternosDetalles'
ALTER TABLE [dbo].[VencimientosServiciosExternosDetalles]
ADD CONSTRAINT [PK_VencimientosServiciosExternosDetalles]
    PRIMARY KEY CLUSTERED ([VenSerExtDet_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Clas_Padre] in table 'Clasificadores'
ALTER TABLE [dbo].[Clasificadores]
ADD CONSTRAINT [FK_Clasificadores_Clasificadores]
    FOREIGN KEY ([Clas_Padre])
    REFERENCES [dbo].[Clasificadores]
        ([Clas_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Clasificadores_Clasificadores'
CREATE INDEX [IX_FK_Clasificadores_Clasificadores]
ON [dbo].[Clasificadores]
    ([Clas_Padre]);
GO

-- Creating foreign key on [AudUser] in table 'Clasificadores'
ALTER TABLE [dbo].[Clasificadores]
ADD CONSTRAINT [FK_Clasificadores_SeguridadUsuarios]
    FOREIGN KEY ([AudUser])
    REFERENCES [dbo].[SeguridadUsuarios]
        ([SegUsu_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Clasificadores_SeguridadUsuarios'
CREATE INDEX [IX_FK_Clasificadores_SeguridadUsuarios]
ON [dbo].[Clasificadores]
    ([AudUser]);
GO

-- Creating foreign key on [Clas_Localidad_Id] in table 'Personas'
ALTER TABLE [dbo].[Personas]
ADD CONSTRAINT [FK_Personas_ClasificadoresLocalidades]
    FOREIGN KEY ([Clas_Localidad_Id])
    REFERENCES [dbo].[Clasificadores]
        ([Clas_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Personas_ClasificadoresLocalidades'
CREATE INDEX [IX_FK_Personas_ClasificadoresLocalidades]
ON [dbo].[Personas]
    ([Clas_Localidad_Id]);
GO

-- Creating foreign key on [Clas_Nacionalidad_Id] in table 'Personas'
ALTER TABLE [dbo].[Personas]
ADD CONSTRAINT [FK_Personas_ClasificadoresNacionalidad]
    FOREIGN KEY ([Clas_Nacionalidad_Id])
    REFERENCES [dbo].[Clasificadores]
        ([Clas_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Personas_ClasificadoresNacionalidad'
CREATE INDEX [IX_FK_Personas_ClasificadoresNacionalidad]
ON [dbo].[Personas]
    ([Clas_Nacionalidad_Id]);
GO

-- Creating foreign key on [Clas_ProEst_Id] in table 'Proveedores'
ALTER TABLE [dbo].[Proveedores]
ADD CONSTRAINT [FK_Proveedores_Clasificadores_ProEst]
    FOREIGN KEY ([Clas_ProEst_Id])
    REFERENCES [dbo].[Clasificadores]
        ([Clas_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Proveedores_Clasificadores_ProEst'
CREATE INDEX [IX_FK_Proveedores_Clasificadores_ProEst]
ON [dbo].[Proveedores]
    ([Clas_ProEst_Id]);
GO

-- Creating foreign key on [Clas_TipoDocumento_Id] in table 'VehiculosDocumentos'
ALTER TABLE [dbo].[VehiculosDocumentos]
ADD CONSTRAINT [FK_VehiculosDocumentos_Clasificadores]
    FOREIGN KEY ([Clas_TipoDocumento_Id])
    REFERENCES [dbo].[Clasificadores]
        ([Clas_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehiculosDocumentos_Clasificadores'
CREATE INDEX [IX_FK_VehiculosDocumentos_Clasificadores]
ON [dbo].[VehiculosDocumentos]
    ([Clas_TipoDocumento_Id]);
GO

-- Creating foreign key on [Clas_TipoLicencia_Id] in table 'VehiculosLicencias'
ALTER TABLE [dbo].[VehiculosLicencias]
ADD CONSTRAINT [FK_VehiculosLicencias_ClasificadoresTipoLicencia]
    FOREIGN KEY ([Clas_TipoLicencia_Id])
    REFERENCES [dbo].[Clasificadores]
        ([Clas_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehiculosLicencias_ClasificadoresTipoLicencia'
CREATE INDEX [IX_FK_VehiculosLicencias_ClasificadoresTipoLicencia]
ON [dbo].[VehiculosLicencias]
    ([Clas_TipoLicencia_Id]);
GO

-- Creating foreign key on [AudUser] in table 'Personas'
ALTER TABLE [dbo].[Personas]
ADD CONSTRAINT [FK_Personas_SeguridadUsuarios]
    FOREIGN KEY ([AudUser])
    REFERENCES [dbo].[SeguridadUsuarios]
        ([SegUsu_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Personas_SeguridadUsuarios'
CREATE INDEX [IX_FK_Personas_SeguridadUsuarios]
ON [dbo].[Personas]
    ([AudUser]);
GO

-- Creating foreign key on [Per_Id] in table 'Proveedores'
ALTER TABLE [dbo].[Proveedores]
ADD CONSTRAINT [FK_Proveedores_Personas]
    FOREIGN KEY ([Per_Id])
    REFERENCES [dbo].[Personas]
        ([Per_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Proveedores_Personas'
CREATE INDEX [IX_FK_Proveedores_Personas]
ON [dbo].[Proveedores]
    ([Per_Id]);
GO

-- Creating foreign key on [Per_Id] in table 'SeguridadUsuarios'
ALTER TABLE [dbo].[SeguridadUsuarios]
ADD CONSTRAINT [FK_SeguridadUsuarios_Personas]
    FOREIGN KEY ([Per_Id])
    REFERENCES [dbo].[Personas]
        ([Per_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SeguridadUsuarios_Personas'
CREATE INDEX [IX_FK_SeguridadUsuarios_Personas]
ON [dbo].[SeguridadUsuarios]
    ([Per_Id]);
GO

-- Creating foreign key on [AudUser] in table 'Proveedores'
ALTER TABLE [dbo].[Proveedores]
ADD CONSTRAINT [FK_Proveedores_SeguridadUsuarios]
    FOREIGN KEY ([AudUser])
    REFERENCES [dbo].[SeguridadUsuarios]
        ([SegUsu_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Proveedores_SeguridadUsuarios'
CREATE INDEX [IX_FK_Proveedores_SeguridadUsuarios]
ON [dbo].[Proveedores]
    ([AudUser]);
GO

-- Creating foreign key on [Prov_Id] in table 'Vehiculos'
ALTER TABLE [dbo].[Vehiculos]
ADD CONSTRAINT [FK_Vehiculos_Proveedores]
    FOREIGN KEY ([Prov_Id])
    REFERENCES [dbo].[Proveedores]
        ([Prov_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Vehiculos_Proveedores'
CREATE INDEX [IX_FK_Vehiculos_Proveedores]
ON [dbo].[Vehiculos]
    ([Prov_Id]);
GO

-- Creating foreign key on [SegPag_Id] in table 'SeguridadRolesPaginas'
ALTER TABLE [dbo].[SeguridadRolesPaginas]
ADD CONSTRAINT [FK_SeguridadRolesPaginas_SeguridadPaginas]
    FOREIGN KEY ([SegPag_Id])
    REFERENCES [dbo].[SeguridadPaginas]
        ([SegPag_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SeguridadRolesPaginas_SeguridadPaginas'
CREATE INDEX [IX_FK_SeguridadRolesPaginas_SeguridadPaginas]
ON [dbo].[SeguridadRolesPaginas]
    ([SegPag_Id]);
GO

-- Creating foreign key on [SegPag_Id] in table 'SeguridadUsuariosPaginas'
ALTER TABLE [dbo].[SeguridadUsuariosPaginas]
ADD CONSTRAINT [FK_SeguridadUsuariosPaginas_SeguridadPaginas]
    FOREIGN KEY ([SegPag_Id])
    REFERENCES [dbo].[SeguridadPaginas]
        ([SegPag_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SeguridadUsuariosPaginas_SeguridadPaginas'
CREATE INDEX [IX_FK_SeguridadUsuariosPaginas_SeguridadPaginas]
ON [dbo].[SeguridadUsuariosPaginas]
    ([SegPag_Id]);
GO

-- Creating foreign key on [SegRol_Id] in table 'SeguridadRolesPaginas'
ALTER TABLE [dbo].[SeguridadRolesPaginas]
ADD CONSTRAINT [FK_SeguridadRolesPaginas_SeguridadRoles]
    FOREIGN KEY ([SegRol_Id])
    REFERENCES [dbo].[SeguridadRoles]
        ([SegRol_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SeguridadRolesPaginas_SeguridadRoles'
CREATE INDEX [IX_FK_SeguridadRolesPaginas_SeguridadRoles]
ON [dbo].[SeguridadRolesPaginas]
    ([SegRol_Id]);
GO

-- Creating foreign key on [SegRol_Id] in table 'SeguridadUsuarios'
ALTER TABLE [dbo].[SeguridadUsuarios]
ADD CONSTRAINT [FK_SeguridadUsuarios_SeguridadRoles]
    FOREIGN KEY ([SegRol_Id])
    REFERENCES [dbo].[SeguridadRoles]
        ([SegRol_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SeguridadUsuarios_SeguridadRoles'
CREATE INDEX [IX_FK_SeguridadUsuarios_SeguridadRoles]
ON [dbo].[SeguridadUsuarios]
    ([SegRol_Id]);
GO

-- Creating foreign key on [SegUsu_Id] in table 'SeguridadUsuariosPaginas'
ALTER TABLE [dbo].[SeguridadUsuariosPaginas]
ADD CONSTRAINT [FK_SeguridadUsuariosPaginas_SeguridadUsuarios]
    FOREIGN KEY ([SegUsu_Id])
    REFERENCES [dbo].[SeguridadUsuarios]
        ([SegUsu_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SeguridadUsuariosPaginas_SeguridadUsuarios'
CREATE INDEX [IX_FK_SeguridadUsuariosPaginas_SeguridadUsuarios]
ON [dbo].[SeguridadUsuariosPaginas]
    ([SegUsu_Id]);
GO

-- Creating foreign key on [AudUser] in table 'Vehiculos'
ALTER TABLE [dbo].[Vehiculos]
ADD CONSTRAINT [FK_Vehiculos_SeguridadUsuarios]
    FOREIGN KEY ([AudUser])
    REFERENCES [dbo].[SeguridadUsuarios]
        ([SegUsu_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Vehiculos_SeguridadUsuarios'
CREATE INDEX [IX_FK_Vehiculos_SeguridadUsuarios]
ON [dbo].[Vehiculos]
    ([AudUser]);
GO

-- Creating foreign key on [AudUser] in table 'VehiculosDocumentos'
ALTER TABLE [dbo].[VehiculosDocumentos]
ADD CONSTRAINT [FK_VehiculosDocumentos_SeguridadUsuarios]
    FOREIGN KEY ([AudUser])
    REFERENCES [dbo].[SeguridadUsuarios]
        ([SegUsu_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehiculosDocumentos_SeguridadUsuarios'
CREATE INDEX [IX_FK_VehiculosDocumentos_SeguridadUsuarios]
ON [dbo].[VehiculosDocumentos]
    ([AudUser]);
GO

-- Creating foreign key on [AudUser] in table 'VehiculosLicencias'
ALTER TABLE [dbo].[VehiculosLicencias]
ADD CONSTRAINT [FK_VehiculosLicencias_SeguridadUsuarios]
    FOREIGN KEY ([AudUser])
    REFERENCES [dbo].[SeguridadUsuarios]
        ([SegUsu_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehiculosLicencias_SeguridadUsuarios'
CREATE INDEX [IX_FK_VehiculosLicencias_SeguridadUsuarios]
ON [dbo].[VehiculosLicencias]
    ([AudUser]);
GO

-- Creating foreign key on [Veh_Id] in table 'VehiculosDocumentos'
ALTER TABLE [dbo].[VehiculosDocumentos]
ADD CONSTRAINT [FK_VehiculosDocumentos_Vehiculos]
    FOREIGN KEY ([Veh_Id])
    REFERENCES [dbo].[Vehiculos]
        ([Veh_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehiculosDocumentos_Vehiculos'
CREATE INDEX [IX_FK_VehiculosDocumentos_Vehiculos]
ON [dbo].[VehiculosDocumentos]
    ([Veh_Id]);
GO

-- Creating foreign key on [Veh_Id] in table 'VehiculosLicencias'
ALTER TABLE [dbo].[VehiculosLicencias]
ADD CONSTRAINT [FK_VehiculosLicencias_Vehiculos]
    FOREIGN KEY ([Veh_Id])
    REFERENCES [dbo].[Vehiculos]
        ([Veh_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehiculosLicencias_Vehiculos'
CREATE INDEX [IX_FK_VehiculosLicencias_Vehiculos]
ON [dbo].[VehiculosLicencias]
    ([Veh_Id]);
GO

-- Creating foreign key on [Clas_MonedasTipos_Id] in table 'VencimientosServiciosExternosDetalles'
ALTER TABLE [dbo].[VencimientosServiciosExternosDetalles]
ADD CONSTRAINT [FK_VencimientosServiciosExternosDetalles_Clasificadores]
    FOREIGN KEY ([Clas_MonedasTipos_Id])
    REFERENCES [dbo].[Clasificadores]
        ([Clas_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VencimientosServiciosExternosDetalles_Clasificadores'
CREATE INDEX [IX_FK_VencimientosServiciosExternosDetalles_Clasificadores]
ON [dbo].[VencimientosServiciosExternosDetalles]
    ([Clas_MonedasTipos_Id]);
GO

-- Creating foreign key on [AudUser] in table 'VencimientosServiciosExternosSet'
ALTER TABLE [dbo].[VencimientosServiciosExternosSet]
ADD CONSTRAINT [FK_VencimientosServiciosExternos_SeguridadUsuarios]
    FOREIGN KEY ([AudUser])
    REFERENCES [dbo].[SeguridadUsuarios]
        ([SegUsu_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VencimientosServiciosExternos_SeguridadUsuarios'
CREATE INDEX [IX_FK_VencimientosServiciosExternos_SeguridadUsuarios]
ON [dbo].[VencimientosServiciosExternosSet]
    ([AudUser]);
GO

-- Creating foreign key on [AudUser] in table 'VencimientosServiciosExternosDetalles'
ALTER TABLE [dbo].[VencimientosServiciosExternosDetalles]
ADD CONSTRAINT [FK_VencimientosServiciosExternosDetalles_SeguridadUsuarios]
    FOREIGN KEY ([AudUser])
    REFERENCES [dbo].[SeguridadUsuarios]
        ([SegUsu_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VencimientosServiciosExternosDetalles_SeguridadUsuarios'
CREATE INDEX [IX_FK_VencimientosServiciosExternosDetalles_SeguridadUsuarios]
ON [dbo].[VencimientosServiciosExternosDetalles]
    ([AudUser]);
GO

-- Creating foreign key on [VenSerExt_Id] in table 'VencimientosServiciosExternosDetalles'
ALTER TABLE [dbo].[VencimientosServiciosExternosDetalles]
ADD CONSTRAINT [FK_VencimientosServiciosExternosDetalles_VencimientosServiciosExternos]
    FOREIGN KEY ([VenSerExt_Id])
    REFERENCES [dbo].[VencimientosServiciosExternosSet]
        ([VenSerExt_Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VencimientosServiciosExternosDetalles_VencimientosServiciosExternos'
CREATE INDEX [IX_FK_VencimientosServiciosExternosDetalles_VencimientosServiciosExternos]
ON [dbo].[VencimientosServiciosExternosDetalles]
    ([VenSerExt_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------