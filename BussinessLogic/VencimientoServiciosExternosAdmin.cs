using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogic
{
    public static class VencimientosServiciosExternosAdmin
    {
  
            public static List<VencimientosServiciosExternos> GetList()
            {
                try
                {
                    return VencimientosServiciosExternosRepository.GetList();
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
                return VencimientosServiciosExternosRepository.GetList().Select(per =>
                     new
                     {
                         id = per.VenSerExt_Id,
                         descripcion = per.VenSerExt_Descripcion ?? "",
                         identificacion = per.VenSerExt_Identificacion ?? "",
                         proveedor = per.VenSerExt_Prov_Id,
                      
                         FechaVencimiento = per.VencimientosServiciosExternosDetalles != null && per.VencimientosServiciosExternosDetalles.Count > 0 ? per.VencimientosServiciosExternosDetalles.OrderByDescending(x => x.VenSerExtDet_FechaVencimiento).FirstOrDefault().VenSerExtDet_FechaVencimiento.ToString() : ""

                     }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        

    }
        public static VencimientosServiciosExternos Get(int id)
        {
            try
            {
                return VencimientosServiciosExternosRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void InsertOrUpdate(VencimientosServiciosExternos model)
        {
            try
            {
                model.AudFecha = DateTime.Now;
                model.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SegUsu_Id;
                var usuario = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]);
                if (model.VenSerExt_Id > 0)
                {
                    var original = VencimientosServiciosExternosRepository.Get(model.VenSerExt_Id);
                    
                    VencimientosServiciosExternosRepository.Update(model);
                }
                else
                {
                    VencimientosServiciosExternosRepository.Insert(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Delete(int id)
        {
            try
            {
                VencimientosServiciosExternosRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }
    }
}
