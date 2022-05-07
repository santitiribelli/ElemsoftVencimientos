using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BusinessLogic.Utiles;
using DataAccess;
using Entities;

namespace BusinessLogic
{
    public static class VehiculosAdmin
    {
        public static List<Vehiculos> GetList()
        {
            try
            {
                return VehiculosRepository.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Vehiculos> GetListVerificados()
        {
            try
            {
                return VehiculosRepository.GetListVerificados();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object GetListJson()
        {
            try
            {
                return VehiculosRepository.GetList().Select(per =>
                     new
                     {
                         id = per.Veh_Id,
                         patente = per.Veh_Patente,
                         fechaDesde = per.Veh_FechaDesde.ToString("dd/MM/yyyy"),
                         fechaHasta = per.Veh_FechaHasta.HasValue ? per.Veh_FechaHasta.Value.ToString("dd/MM/yyyy") : "",
                         observacion = per.Veh_Obs ?? "",
                         cantidad = per.Veh_PasajerosCantidad,
                         proveedor = per.Prov_Id != null ? per.Proveedores.Personas.NombreCompleto : "",
                         proveedorId = per.Prov_Id.ToString() ?? "",
                         datosVerificados = per.Veh_DatosVerificados ? "SI" : "NO",
                         verificados = per.Veh_DatosVerificados,
                     }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Vehiculos Get(int id)
        {
            try
            {
                return VehiculosRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertOrUpdate(Vehiculos model)
        {
            try
            {
                model.AudFecha = DateTime.Now;
                model.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SegUsu_Id;
                var usuario = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]);
                if (model.Veh_Id > 0)
                {
                    var original = VehiculosRepository.Get(model.Veh_Id);
                    model.Veh_DatosVerificados = true;
                    VehiculosRepository.Update(model);
                }
                else
                {
                    model.Veh_DatosVerificados = true;
                    VehiculosRepository.Insert(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Verificar(int id)
        {
            try
            {
                var v = VehiculosRepository.Get(id);
                v.Veh_DatosVerificados = true;
                v.AudFecha = DateTime.Now;
                v.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SegUsu_Id;
                VehiculosRepository.Update(v);
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un problema, no se pudo realizar la modificación.");
            }
        }

        public static void Delete(int id)
        {
            try
            {
                VehiculosRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }
    }
}
