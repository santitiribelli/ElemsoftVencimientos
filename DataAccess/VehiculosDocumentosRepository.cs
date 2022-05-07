using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Entities;
using System.Data.Entity;
using System.Web;

namespace DataAccess
{
    public static class VehiculosDocumentosRepository
    {
        public static List<VehiculosDocumentos> GetList()
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.VehiculosDocumentos
                            .Include("Clasificadores")
                            .Include("Vehiculos")
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
        public static List<VehiculosDocumentos> GetList(int id)
        {
            using (var context = Utiles.ContextoLocal())
            {
                return (from p in context.VehiculosDocumentos
                        .Include("Clasificadores.ClasificadoresIdiomas")
                        .Include("Vehiculos")
                        where p.Veh_Id == id
                        select p).ToList();
            }
        }
        public static VehiculosDocumentos Get(int id)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.VehiculosDocumentos
                            .Include("Clasificadores")
                            .Include("Vehiculos")
                            where p.VehDoc_Id == id
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

        public static void Update(VehiculosDocumentos varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var viejo =
                        context.VehiculosDocumentos.Attach(context.VehiculosDocumentos.Single(i => i.VehDoc_Id == varia.VehDoc_Id));

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

        public static void Insert(VehiculosDocumentos varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    context.VehiculosDocumentos.Add(varia);

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
                    var valor = (from p in context.VehiculosDocumentos where p.VehDoc_Id == id select p).FirstOrDefault();

                    context.VehiculosDocumentos.Remove(valor);

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
