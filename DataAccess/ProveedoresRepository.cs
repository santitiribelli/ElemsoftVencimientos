using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.Entity;
using System.Web;

namespace DataAccess
{
    public static class ProveedoresRepository
    {
        public static List<Proveedores> GetList()
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                        return (from p in context.Proveedores
                                        .Include("Personas.Proveedores")
                                        .Include("Clasificadores")
                                 select p).ToList();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ErrorHelper.dbError(ex));
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static List<Proveedores> GetListVerificados()
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Proveedores
                            .Include("Personas")
                            .Include("Personas.Empleados")
                            .Include("Personas.Tripulantes")
                            .Include("Personas.Clientes")
                            .Include("ProductosProveedores")
                            .Include("Clasificadores.ClasificadoresIdiomas")
                                //.Include("Clasificadores1")
                            where p.Prov_DatosVerificados
                            select p).ToList();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ErrorHelper.dbError(ex));
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static Proveedores Get(int id)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Proveedores
                            .Include("Personas")
                            //.Include("Personas.Empleados") //jfr - falla en la version
                            //.Include("Personas.Tripulantes") //jfr - falla en la version
                            //.Include("Personas.Clientes") //jfr - falla en la version
                            //.Include("ProductosProveedores") //jfr - falla en la version
                            .Include("Clasificadores")
                                //.Include("Compras")
                            where p.Prov_Id == id
                            select p).FirstOrDefault();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ErrorHelper.dbError(ex));
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static Proveedores GetByPerId(int id)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Proveedores
                            .Include("Personas")
                            .Include("Personas.Empleados")
                            .Include("Personas.Tripulantes")
                            .Include("Personas.Clientes")
                            .Include("ProductosProveedores")
                            .Include("Clasificadores")
                            where p.Per_Id == id
                            select p).FirstOrDefault();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ErrorHelper.dbError(ex));
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static void Update(Proveedores varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var viejo =
                        context.Proveedores.Attach(context.Proveedores.Single(i => i.Prov_Id == varia.Prov_Id));

                    context.Entry(viejo).CurrentValues.SetValues(varia);

                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ErrorHelper.dbError(ex));
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("uniqueProductosNomGen"))
                    throw new Exception("Ya existe ese nombre genérico asignado a esa marca/modelo.");
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static void Insert(Proveedores varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    context.Proveedores.Add(varia);

                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ErrorHelper.dbError(ex));
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("uniqueProductosNomGen"))
                    throw new Exception("Ya existe ese nombre genérico asignado a esa marca/modelo.");
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static void Delete(int id)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var valor = (from p in context.Proveedores where p.Prov_Id == id select p).FirstOrDefault();

                    context.Proveedores.Remove(valor);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        if (ex.InnerException.InnerException.Message.Contains("REFERENCE") &&
                            ex.InnerException.InnerException.Message.Contains("DELETE"))
                            throw new Exception(
                                "No puede eliminar el lugar ya que tiene relación con otros registros.");
                    }
                    throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
                }
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }
    }
}
