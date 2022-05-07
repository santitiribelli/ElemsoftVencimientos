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
    public static class ProveedoresAdmin
    {
        public static List<Proveedores> GetList()
        {
            try
            {
                return ProveedoresRepository.GetList();
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
                return ProveedoresRepository.GetList().Select(per =>
                    new
                    {
                        id = per.Prov_Id,
                        persona = per.Personas.NombreCompleto,
                        personaId = per.Per_Id,
                        codigo = per.Prov_Codigo ??"",
                        estado = per.Clasificadores.Clas_Desc,
                        estadoId = per.Clas_ProEst_Id,
                        cuentaCorriente = per.Prov_CtaCorriente ? "SI" : "NO",
                        observacion = per.Prov_Obs ?? "",
                        verificados = per.Prov_DatosVerificados,
                    }).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Proveedores GetByPerId(int id)
        {
            try
            {
                return ProveedoresRepository.GetByPerId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void InsertOrUpdate(Proveedores model)
        {
            try
            {
                model.AudFecha = DateTime.Now;
                model.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SegUsu_Id;
                if (model.Prov_Id > 0)
                {
                    var original = ProveedoresRepository.Get(model.Prov_Id);
                    model.Prov_DatosVerificados = true;
                    ProveedoresRepository.Update(model);
                }
                else
                {
                    model.Prov_DatosVerificados = true;
                    ProveedoresRepository.Insert(model);
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
                ProveedoresRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }
    }
}
