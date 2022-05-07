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
    public static class VencimientosServiciosExternosDetallesAdmin
    {

        public static List<VencimientosServiciosExternosDetalles> GetList()
        {
            try
            {
                return VencimientosServiciosExternosDetallesRepository.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static object GetListByDetalleJson(int id)
        {
            try
            {
                var lista = VencimientosServiciosExternosDetallesRepository.GetListByDetalles(id);
                var ultimoId = 0;
                if (lista.Count > 0)
                {
                    ultimoId = lista.FirstOrDefault().VenSerExtDet_Id;
                }
                return lista.Select(per =>
                     new
                     {
                         id = per.VenSerExtDet_Id,
                         vencimientoId = per.VenSerExt_Id,
                         fechaVencimiento = per.VenSerExtDet_FechaVencimiento.ToString(),
                         fechaPagado = per.VenSerExtDet_FechaPagado.HasValue ? per.VenSerExtDet_FechaPagado.Value.ToString("dd/MM/yyyy") : "",
                         valorPagado = per.VenSerExtDet_ValorPagado,
                         tipo = per.Clasificadores != null ? per.Clasificadores.Clas_Desc : "",
                         tipoId = per.Clas_MonedasTipos_Id.ToString(),
                         observacion = per.VenSerExtDet_Obs,
                         verif = per.VenSerExtDet_ValorPagado > 0 ? "True" : "False",



                         //FechaVencimiento = per.VencimientosServiciosExternosDetalles.Count != 0 ? per.VencimientosServiciosExternosDetalles.FirstOrDefault().VenSerExtDet_FechaVencimiento : (DateTime?)null,
                         //FechaVencimiento = per.VencimientosServiciosExternosDetalles.OrderBy(VencimientosServiciosExternosDetalles)
                         //FechaVencimiento = per.VencimientosServiciosExternosDetalles.Single(i => i.VenSerExt_Id == per.VenSerExt_Id).VenSerExtDet_FechaVencimiento


                     }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public static VencimientosServiciosExternosDetalles Get(int id)
        {
            try
            {
                return VencimientosServiciosExternosDetallesRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void InsertOrUpdate(VencimientosServiciosExternosDetalles varia)
        {
            try
            {
                varia.AudFecha = DateTime.Now;
                varia.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SegUsu_Id;
                var usuario = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]);
                if (varia.VenSerExtDet_Id > 0)
                {
                    var original = VencimientosServiciosExternosDetallesRepository.Get(varia.VenSerExt_Id);

                    VencimientosServiciosExternosDetallesRepository.Update(varia);
                }
                else
                {
                    VencimientosServiciosExternosDetallesRepository.Insert(varia);
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
                VencimientosServiciosExternosDetallesRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }
    }
}
