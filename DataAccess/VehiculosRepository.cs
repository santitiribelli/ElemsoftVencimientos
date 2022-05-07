using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Entities;
using  System.Data.Entity;
namespace DataAccess
{
    public static class VehiculosRepository
    {
        public static List<Vehiculos> GetList()
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Vehiculos
                            .Include("Proveedores.Personas")
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

        public static List<Vehiculos> GetListVerificados()
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Vehiculos
                            .Include(x=>x.VehiculosLicencias.Select(i=>i.Clasificadores))
                            where p.Veh_DatosVerificados && !p.Veh_FechaHasta.HasValue
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

        public static Vehiculos Get(int id)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Vehiculos
                            .Include("Proveedores.Personas")
                            .Include("VehiculosDocumentos")
                            .Include(x => x.VehiculosLicencias.Select(i => i.Clasificadores))
                            where p.Veh_Id == id
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

        public static void Update(Vehiculos varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var viejo =
                        context.Vehiculos.Attach(context.Vehiculos.Single(i => i.Veh_Id == varia.Veh_Id));

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
                if (ex.InnerException != null && ex.InnerException.InnerException != null && ex.InnerException.InnerException.Message.Contains("uniquePacientesDoc"))
                    throw new Exception("Ya existe ese número de documento asignado a una persona.");
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static void Insert(Vehiculos varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    context.Vehiculos.Add(varia);

                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ErrorHelper.dbError(ex));
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException != null && ex.InnerException.InnerException.Message.Contains("uniquePacientesDoc"))
                    throw new Exception("Ya existe ese número de documento asignado a una persona.");
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static void Delete(int id)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var valor = (from p in context.Vehiculos where p.Veh_Id == id select p).FirstOrDefault();

                    context.Vehiculos.Remove(valor);

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
                    throw new Exception("Hubo un problema, no se pudo realizar la modificación.");
                }
                throw new Exception("Hubo un problema, no se pudo realizar la modificación.");
            }
        }
    }
}
