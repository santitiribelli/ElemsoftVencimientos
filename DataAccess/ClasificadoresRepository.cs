using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Entities;
using System.Data.Entity;
namespace DataAccess
{
    public static class ClasificadoresRepository
    {
        public static List<Clasificadores> GetList(string tabla)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Clasificadores
                            .Include("Clasificadores1")
                            .Include("Clasificadores2")
                        where p.Clas_Tabla == tabla select p).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static List<string> GetTablas()
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Clasificadores select p).Select(i => i.Clas_Tabla).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static Clasificadores Get(string abrev, string tabla)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Clasificadores
                            where p.Clas_Abrev == abrev &&
                                    p.Clas_Tabla == tabla
                            select p).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("REFERENCE") &&
                    ex.InnerException.InnerException.Message.Contains("DELETE"))
                    throw new Exception(
                        "No puede eliminar el clasificador ya que tiene relación con otras tablas o tiene clasificadores que dependen de él.");
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static Clasificadores GetByNombre(string nombre, string tabla)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Clasificadores
                            where p.Clas_Desc == nombre &&
                                    p.Clas_Tabla == tabla
                            select p).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("REFERENCE") &&
                    ex.InnerException.InnerException.Message.Contains("DELETE"))
                    throw new Exception(
                        "No puede eliminar el clasificador ya que tiene relación con otras tablas o tiene clasificadores que dependen de él.");
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static Clasificadores Get(int id)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    //TODO: Esta fallando por que no trae ClasificadoresIdiomas
                    //return (from p in context.Clasificadores.Include("ClasificadoresIdiomas") where p.Clas_Id == id select p).FirstOrDefault();
                    return (from p in context.Clasificadores where p.Clas_Id == id select p).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static List<Clasificadores> GetListByTabla(string tabla, string palabra, int? idPropio)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Clasificadores
                        where p.Clas_Tabla == tabla &&
                              p.Clas_Desc.Contains(palabra) &&
                              p.Clas_Padre == null &&
                              (idPropio == null || p.Clas_Id != idPropio)
                        select p).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un problema, no se pudo realizar la modificación.");
            }
        }

        public static List<Clasificadores> GetListByTablaAndPreviousId(string tabla, int id, string palabra, int? idPropio)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Clasificadores
                        where p.Clas_Tabla == tabla &&
                              p.Clas_Padre == id &&
                              p.Clas_Desc.Contains(palabra) &&
                              (idPropio == null || p.Clas_Id != idPropio)
                        select p).ToList();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("REFERENCE") &&
                    ex.InnerException.InnerException.Message.Contains("DELETE"))
                    throw new Exception(
                        "No puede eliminar el clasificador ya que tiene relación con otras tablas o tiene clasificadores que dependen de él.");
                throw new Exception("Hubo un problema, no se pudo realizar la modificación.");
            }
        }

        public static void Update(Clasificadores varia, Clasificadores padreNuevo, Clasificadores padreOriginal)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var viejo = new Clasificadores();
                    if (padreOriginal.Clas_Id > 0)
                    {
                        viejo = context.Clasificadores.Attach(context.Clasificadores.Single(i => i.Clas_Id == padreOriginal.Clas_Id));
                        context.Entry(viejo).CurrentValues.SetValues(padreOriginal);
                    }

                    if (padreNuevo.Clas_Id > 0)
                    {
                        viejo = context.Clasificadores.Attach(context.Clasificadores.Single(i => i.Clas_Id == padreNuevo.Clas_Id));
                        context.Entry(viejo).CurrentValues.SetValues(padreNuevo);
                    }

                    viejo = context.Clasificadores.Attach(context.Clasificadores.Single(i => i.Clas_Id == varia.Clas_Id));
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
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static void Insert(Clasificadores varia, Clasificadores padre)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    if (padre.Clas_Id > 0)
                    {
                        var viejo =
                        context.Clasificadores.Attach(context.Clasificadores.Single(i => i.Clas_Id == padre.Clas_Id));

                        context.Entry(viejo).CurrentValues.SetValues(padre);
                    }
                    
                    context.Clasificadores.Add(varia);

                    context.SaveChanges();
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

        public static List<Clasificadores> GetListHijos(int id)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Clasificadores
                            .Include("Clasificadores1")
                            .Include("Clasificadores2")
                        where p.Clas_Padre == id
                        select p).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }
    }
}
