using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class VehiculosLicenciasRepository
    {
        public static List<VehiculosLicencias> GetList()
        {
            using (var context = Utiles.ContextoLocal())
            {

                return (from p in context.VehiculosLicencias
                            .Include("Clasificadores")
                            .Include("Vehiculos")
                        select p).ToList();
            }
        }

        public static List<VehiculosLicencias> GetListByVehiculo(int id)
        {
            using (var context = Utiles.ContextoLocal())
            {

                return (from p in context.VehiculosLicencias
                        .Include("Clasificadores")
                        .Include("Vehiculos")
                        where p.Veh_Id == id
                        select p).OrderByDescending(x => x.VehLic_FechaDesde).ToList();
            }
        }

        public static VehiculosLicencias Get(int id)
        {
            using (var context = Utiles.ContextoLocal())
            {
                return (from p in context.VehiculosLicencias
                            .Include("Clasificadores")
                        .Include("Vehiculos")
                        where p.VehLic_Id == id
                        select p).FirstOrDefault();
            }
        }

        public static void Insert(VehiculosLicencias varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    context.VehiculosLicencias.Add(varia);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("uniqueVehiculosDomicilio"))
                    throw new Exception("No pueden haber dos domicilios del mismo tipo en vigencia.");
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static void Update(VehiculosLicencias varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var viejo =
                        context.VehiculosLicencias.Attach(
                            context.VehiculosLicencias.Single(i => i.VehLic_Id == varia.VehLic_Id));

                    context.Entry(viejo).CurrentValues.SetValues(varia);
                    
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("uniqueVehiculosDomicilio"))
                    throw new Exception("No pueden haber dos domicilios del mismo tipo en vigencia.");
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static void Delete(int id)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var valor = (from p in context.VehiculosLicencias where p.VehLic_Id == id select p).FirstOrDefault();

                    context.VehiculosLicencias.Remove(valor);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }
    }
}
