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
    public class VehiculosLicenciasAdmin
    {
        public static List<VehiculosLicencias> GetList()
        {
            return VehiculosLicenciasRepository.GetList();
        }

        //public static List<VehiculosLicencias> GetListByPersona(int id)
        //{
        //    return VehiculosLicenciasRepository.GetListByEmpleado(id);
        //}

        public static object GetListByVehiculoJson(int id)
        {
            try
            {
                var lista = VehiculosLicenciasRepository.GetListByVehiculo(id);
                var ultimoId = 0;
                if (lista.Count > 0)
                {
                    ultimoId = lista.FirstOrDefault().VehLic_Id;
                }
                    
                return lista.Select(per =>
                    new
                    {
                        id = per.VehLic_Id, //Id Registro                    
                        fechaDesde = per.VehLic_FechaDesde.ToString("dd/MM/yyyy"),
                        fechaHasta = per.VehLic_FechaHasta.HasValue ? per.VehLic_FechaHasta.Value.ToString("dd/MM/yyyy") : "", // este es el que iria realmente
                        observacion = per.VehLic_Obs ?? "",
                        vehiculoId = per.Veh_Id, //Empleado
                        tipo = per.Clasificadores != null ? per.Clasificadores.Clas_Desc : "",
                        tipoId = per.Clas_TipoLicencia_Id.ToString(),
                        generarAlerta = per.GeneraAlerta == true ? "True" : "False",
                        verificados = per.VehLic_DatosVerificados,
                        ultimo = per.VehLic_Id == ultimoId ? "True" : "False",
                    }).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static VehiculosLicencias Get(int id)
        {
            return VehiculosLicenciasRepository.Get(id);
        }

        public static void InsertOrUpdate(VehiculosLicencias varia)
        {
            try
            {
                varia.AudFecha = DateTime.Now;
                varia.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SegUsu_Id;
                var usuario = (SeguridadUsuarios)HttpContext.Current.Session["UserLogged"];
                SuperposicionFechas(varia.VehLic_FechaDesde, varia.VehLic_FechaHasta,
                    varia.VehLic_Id > 0 ? varia.VehLic_Id : (int?)null, varia.Veh_Id);

                var ultimoEmpleadoLicencia = getUltimoRegEmpLic(varia);

                if (varia.VehLic_Id > 0)
                {
                    if (ultimoEmpleadoLicencia != null && ultimoEmpleadoLicencia.VehLic_Id != varia.VehLic_Id)
                        throw new Exception("No es posible editar este registro.");
                    var original = VehiculosLicenciasRepository.Get(varia.VehLic_Id);
                    varia.VehLic_DatosVerificados = true;
                    VehiculosLicenciasRepository.Update(varia);
                }
                else
                {
                    //if (ultimoEmpleadoLicencia != null && !ultimoEmpleadoLicencia.VehLic_FechaHasta.HasValue)
                    //    throw new Exception("No es posible insertar este registro. Ultimo registro sin 'Fecha Hasta'");
                    if (ultimoEmpleadoLicencia != null && ultimoEmpleadoLicencia.VehLic_FechaDesde > varia.VehLic_FechaDesde)
                        throw new Exception("No es posible insertar este registro. La Fecha es mayor que la ultimo registrada.");
                    varia.VehLic_DatosVerificados = true;
                    VehiculosLicenciasRepository.Insert(varia);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("posible"))
                    throw new Exception(ex.Message);
                else
                {
                    throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
                }
            }
        }

        public static void Delete(int id)
        {
            try
            {
                var registroVehiculoEstados = VehiculosLicenciasRepository.Get(id);

                var ultimoEmpleadoLicencia = getUltimoRegEmpLic(registroVehiculoEstados);

                if (ultimoEmpleadoLicencia != null && ultimoEmpleadoLicencia.VehLic_Id != id)
                    throw new Exception("No es posible eliminar este registro.");

                VehiculosLicenciasRepository.Delete(id);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("posible"))
                    throw new Exception(ex.Message);
                else
                    throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }
        
        
        public static void Verificar(int id)
        {
            try
            {
                var v = VehiculosLicenciasRepository.Get(id);
                v.VehLic_DatosVerificados = true;
                v.AudFecha = DateTime.Now;
                v.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SegUsu_Id;
                VehiculosLicenciasRepository.Update(v);
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un problema, no se pudo realizar la modificación.");
            }
        }

        private static VehiculosLicencias getUltimoRegEmpLic(VehiculosLicencias model)
        {
            return VehiculosLicenciasRepository.GetListByVehiculo(model.Veh_Id).FirstOrDefault();
        }

        public static void SuperposicionFechas(DateTime desde, DateTime? hasta, int? idPropio, int vehiculoId)
        {
            try
            {
                var lista = VehiculosLicenciasRepository.GetListByVehiculo(vehiculoId);

                foreach (var item in lista)
                {
                    if (item.VehLic_Id != idPropio)
                    {
                        if (desde >= item.VehLic_FechaDesde && desde <= item.VehLic_FechaHasta)
                            throw new Exception("Validacion - Superposicion - Fecha Desde Superpone");

                        if (hasta >= item.VehLic_FechaDesde && hasta <= item.VehLic_FechaHasta)
                            throw new Exception("Validacion - Superposicion - Fecha Hasta Superpone");

                        if (desde <= item.VehLic_FechaDesde && hasta >= item.VehLic_FechaHasta)
                            throw new Exception("Validacion - Superposicion - Entre Fecha Desde y Hasta");

                        //if (desde >= item.PerEstCer_FecDes && item.PerEstCer_FecHas == null)
                        //    throw new Exception("La fecha hasta se superpone con la fecha de otro periodo.");
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
