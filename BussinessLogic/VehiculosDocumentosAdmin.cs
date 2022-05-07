using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataAccess;
using Entities;

namespace BusinessLogic
{
    public static class VehiculosDocumentosAdmin
    {
        public static List<VehiculosDocumentos> GetList()
        {
            return VehiculosDocumentosRepository.GetList();
        }

        //public static List<VehiculosDocumentos> GetListByVehiculoJson(int id)
        //{
        //    return VehiculosDocumentosRepository.GetList(id);
        //}

        public static object GetListByVehiculoJson(int id)
        {
            try
            {
                return VehiculosDocumentosRepository.GetList(id).Select(per =>
                    new
                    {
                        id = per.VehDoc_Id,
                        clas = per.Clasificadores != null ? per.Clasificadores.Clas_Desc : "",
                        clasId = per.Clasificadores != null ? per.Clasificadores.Clas_Id.ToString() : "",
                        fechaAprobado = per.VehDoc_FechaAprobado.ToString("dd/MM/yyyy"),
                        fechaVigencia = per.VehDoc_FechaVencimiento.HasValue ? per.VehDoc_FechaVencimiento.Value.ToString("dd/MM/yyyy") : "",
                        fechaAviso = per.VehDoc_FechaAviso.HasValue ? per.VehDoc_FechaAviso.Value.ToString("dd/MM/yyyy") : "",
                        generarAlerta = per.GeneraAlerta ? "SI" : "NO",
                        datosVerificados = per.VehDoc_DatosVerificados ? "SI" : "NO",
                        verificados = per.VehDoc_DatosVerificados,
                        documento = per.VehDoc_ArchivoAdjunto ?? "",
                        observacion = per.VehDoc_Obs ?? "",
                    }).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static VehiculosDocumentos Get(int id)
        {
            return VehiculosDocumentosRepository.Get(id);
        }

        public static void InsertOrUpdate(VehiculosDocumentos varia)
        {
            try
            {
                varia.AudFecha = DateTime.Now;
                varia.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SegUsu_Id;
                var usuario = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]);
                if (varia.VehDoc_Id > 0)
                {
                    var original = VehiculosDocumentosRepository.Get(varia.VehDoc_Id);
                    varia.VehDoc_DatosVerificados = true;
                    if (!String.IsNullOrEmpty(original.VehDoc_ArchivoAdjunto))
                        varia.VehDoc_ArchivoAdjunto = original.VehDoc_ArchivoAdjunto;

                    VehiculosDocumentosRepository.Update(varia);
                }
                else
                {
                    varia.VehDoc_DatosVerificados = true;
                    VehiculosDocumentosRepository.Insert(varia);
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
                VehiculosDocumentosRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }


        public static void UpdateVehiculosDocumentosArchivos(VehiculosDocumentos varia)
        {
            try
            {
                VehiculosDocumentosRepository.Update(varia);
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static void Verificar(int id)
        {
            try
            {
                var v = VehiculosDocumentosRepository.Get(id);
                v.VehDoc_DatosVerificados = true;
                v.AudFecha = DateTime.Now;
                v.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SegUsu_Id;
                VehiculosDocumentosRepository.Update(v);
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un problema, no se pudo realizar la modificación.");
            }
        }

    }
}
