using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;




namespace DataAccess
{
    public static class VencimientosServiciosExternosRepository
    {
        public static List<VencimientosServiciosExternos> GetList()
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.VencimientosServiciosExternos
                            .Include("VencimientosServiciosExternosDetalles")
                            //.Include( p => p.Proveedores)
                            
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
        public static VencimientosServiciosExternos Get(int id)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.VencimientosServiciosExternos
                            
                            
                            //.Include(x => x.VencimientosServiciosExternosDetalles.Select (i => i.VenSerExtDet_FechaVencimiento))                 
                            where p.VenSerExt_Id == id
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
        public static void Update(VencimientosServiciosExternos varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var viejo =
                        context.VencimientosServiciosExternos.Attach(context.VencimientosServiciosExternos.Single(i => i.VenSerExt_Id == varia.VenSerExt_Id));

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

        public static void Insert(VencimientosServiciosExternos varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    context.VencimientosServiciosExternos.Add(varia);

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
                    var valor = (from p in context.VencimientosServiciosExternos where p.VenSerExt_Id == id select p).FirstOrDefault();

                    context.VencimientosServiciosExternos.Remove(valor);

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
