using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using DataAccess;
using Entities;

namespace BusinessLogic
{
    public static class ClasificadoresAdmin
    {
        public static List<Clasificadores> listaClas { set; get; }

        public static List<Clasificadores> GetList(string tabla)
        {
            try
            {
                return ClasificadoresRepository.GetList(tabla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<string> GetTablas()
        {
            try
            {
                return ClasificadoresRepository.GetTablas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Clasificadores Get(string abrev, string tabla)
        {
            try
            {
                return ClasificadoresRepository.Get(abrev,tabla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Clasificadores Get(int id)
        {
            try
            {
                return ClasificadoresRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object GetClasificador(string tabla, int? id, string palabra, int? idPropio)
        {
            var list = id != null ? ClasificadoresRepository.GetListByTablaAndPreviousId(tabla, (int)id, palabra, idPropio) : ClasificadoresRepository.GetListByTabla(tabla, palabra, idPropio);
            return
                list
                    .Select(x => new
                    {
                        id = x.Clas_Id,
                        text = x.Clas_Desc,
                        ultimoNivel = x.Clas_DatoOblig
                    })
                    .ToList();
        }

        public static object GetClasificadorByCBU(string abrev, string tabla)
        {
            var item = ClasificadoresRepository.Get(abrev, tabla);
            if (item != null)
            {
                var list = GetListCascadeList(item.Clas_Id);
                var auxList =
                    list.Select(clasificadorese => new { id = clasificadorese.Clas_Id, text = clasificadorese.Clas_Desc })
                        .OrderBy(m => m.id)
                        .ToList();
                return auxList;
            }
            
            return "";
        }

        public static void InsertOrUpdate(Clasificadores model)
        {
            try
            {
                var padre = new Clasificadores();
                model.AudFecha = DateTime.Now;
                model.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SegUsu_Id;
                model.Clas_Desc = model.Clas_Desc.ToUpper();

                if (model.Clas_Id > 0)
                {
                    var padreOriginal = new Clasificadores();
                    var original = ClasificadoresRepository.Get(model.Clas_Id);
                    model.Clas_FecAlta = original.Clas_FecAlta;
                    model.Clas_Interno = original.Clas_Interno;
                    model.Clas_Detalle = original.Clas_Detalle;
                    model.Clas_DatoOblig = original.Clas_DatoOblig;

                    //EL PADRE ES DISTINTO AL ORIGINAL?
                    if (model.Clas_Padre != original.Clas_Padre)
                    {
                        //SI
                        //PADRE ORIGINAL ES DISTINTO A NULL?
                        if (original.Clas_Padre != null)
                        {
                            //SI
                            //TIENE MAS HIJOS SIN CONTAR EL QUE ESTOY MODIFICANDO?
                            padreOriginal = ClasificadoresRepository.Get((int)original.Clas_Padre);
                            if (padreOriginal.Clasificadores1.Count - 1 <= 0)
                            {
                                //SI
                                //ULTIMO NIVEL = NO
                                padre.Clas_DatoOblig = false;
                            }
                            else
                            {
                                //NO
                                //ULTIMO NIVEL = SI
                                padre.Clas_DatoOblig = true;
                            }
                        }
                        //PADRE NUEVO ES DISTINTO A NULL?
                        if (model.Clas_Padre != null)
                        {
                            //SI
                            //EL PADRE ES ULTIMO NIVEL??
                            padre = ClasificadoresRepository.Get((int)model.Clas_Padre);
                            if (padre.Clas_DatoOblig)
                            {
                                //SI
                                //SETEAR AL PADRE COMO ULTIMO NIVEL = NO
                                padre.Clas_DatoOblig = false;
                            }
                        }
                    }
                    ClasificadoresRepository.Update(model, padre, padreOriginal);
                }
                else
                {
                    model.Clas_FecAlta = DateTime.Now;
                    //ULT. NIVEL = SI
                    model.Clas_DatoOblig = true;

                    
                    //TIENE PADRE?
                    if (model.Clas_Padre != null)
                    {
                        //SI
                        //EL PADRE ES ULTIMO NIVEL??
                        padre = ClasificadoresRepository.Get((int)model.Clas_Padre);
                        if (padre.Clas_DatoOblig)
                        {
                            //SI
                            //SETEAR AL PADRE COMO ULTIMO NIVEL = NO
                            padre.Clas_DatoOblig = false;
                        }
                    }
                    ClasificadoresRepository.Insert(model, padre);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetNombreCompleto(int id)
        {
            try
            {
                var nomCompleto = "";
                var tope = false;

                while (tope == false)
                {
                    var clasi = ClasificadoresRepository.Get(id);

                    if (nomCompleto == "")
                        nomCompleto = clasi.Clas_Desc;
                    else
                        nomCompleto = clasi.Clas_Desc + " - " + nomCompleto;

                    if (clasi.Clas_Padre == null)
                        tope = true;
                    else
                        id = (int)clasi.Clas_Padre;
                }

                return nomCompleto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Clasificadores> GetListCascadeList(int id)
        {
            var tope = false;
            var list = new List<Clasificadores>();

            while (tope == false)
            {
                var clasi = ClasificadoresRepository.Get(id);

                list.Add(clasi);

                if (clasi.Clas_Padre == null)
                    tope = true;
                else
                    id = (int)clasi.Clas_Padre;
            }
            list.Reverse();
            return list;
        }

        public static object GetListCascade(int id)
        {
            var tope = false;
            var list = new List<Clasificadores>();

            while (tope == false)
            {
                var clasi = ClasificadoresRepository.Get(id);

                list.Add(clasi);

                if (clasi.Clas_Padre == null)
                    tope = true;
                else
                    id = (int)clasi.Clas_Padre;
            }
            return list.Select(clasificadorese => new { id = clasificadorese.Clas_Id, text = clasificadorese.Clas_Desc, ultimoNivel = clasificadorese.Clas_DatoOblig })
                            .Reverse()
                            .ToList();
        }

        public static object GenerarMenuPadre(string tabla)
        {
            listaClas = new List<Clasificadores>();
            var lista = ClasificadoresRepository.GetList(tabla);

            foreach (var dr in lista)
            {
                if (dr.Clas_Padre == null)
                {
                    listaClas.Add(dr);

                    GenerarMenu(lista, dr);
                }
            }

            return listaClas.Select(per =>
                    new
                    {
                        id = per.Clas_Id,
                        dependeId = per.Clasificadores2 != null ? per.Clas_Padre.ToString() : "",
                        depende = per.Clasificadores2 != null ? per.Clasificadores2.Clas_Desc : "",
                        desc = per.Clas_Desc,
                        abrev = per.Clas_Abrev,
                        ultimoNivel = per.Clas_DatoOblig ? "SI" : "NO",
                        fechaAlta = per.Clas_FecAlta.ToString("dd/MM/yyyy"),
                        fechaBaja = per.Clas_FecBaja.HasValue ? per.Clas_FecBaja.Value.ToShortDateString() : "",
                        interno = per.Clas_Interno,
                    }).ToList();
        }

        public static void GenerarMenu(List<Clasificadores> list, Clasificadores clas)
        {
            foreach (var dr in list)
            {
                if (dr.Clas_Padre == clas.Clas_Id)
                {
                    listaClas.Add(dr);

                    GenerarMenu(list, dr);
                }
            }
        }

        public static List<Clasificadores> GetListHijos(int id)
        {
            try
            {
                return ClasificadoresRepository.GetListHijos(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IList<string> GetListFormulariosTipos()
        {
            try
            {
                var lista = ClasificadoresRepository.GetList("Formularios - Tipos");
                foreach (var item in lista)
                {
                    item.Clas_Desc = item.Clas_Desc.Contains(" A") || item.Clas_Desc.Contains(" B") || item.Clas_Desc.Contains(" C") ? item.Clas_Desc.Substring(0, item.Clas_Desc.Length - 2) : item.Clas_Desc;
                }
                var algo = lista.GroupBy(x => x.Clas_Desc).Select(i => i.Key).ToList();
                return algo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region WebApi

        public static object GetListApp(string tabla)
        {
            try
            {
                return ClasificadoresRepository.GetList(tabla).Select(per =>
                    new
                    {
                        id = per.Clas_Id,
                        desc = per.Clas_Desc,
                    }).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion
    }
}
