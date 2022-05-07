using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Entities;
using System.Data.Entity;
using System.Web;

namespace DataAccess
{
    public static class PersonasRepository
    {
        #region Web Central
        public static List<Personas> GetList()
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Personas
                            .Include("Clasificadores")
                            .Include("Clasificadores1")
                            .Include("PersonasDomicilios")
                                .Include("PersonasContactos")
                                .Include("PersonasDocumentos")
                                .Include("PersonasCaracteristicas")
                                .Include("PersonasIdiomas")
                                .Include("Clientes")
                                .Include("Empleados")
                                .Include("Proveedores")
                                .Include("Tripulantes")
                            select p).OrderBy(x => x.Per_SoloApe).ThenBy(x => x.Per_SoloNom).ToList();
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

        public static List<Personas> GetList(bool perFisica)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Personas
                                .Include("Clasificadores")
                            where p.Per_PersonaFisica == perFisica
                            select p).OrderBy(x=> x.Per_SoloApe).ThenBy(x=> x.Per_SoloNom).ToList();
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

        public static List<Personas> GetList(string palabra)
        {
            using (var context = Utiles.ContextoLocal())
            {
                int doc;
                int.TryParse(palabra, out doc);

                return (from p in context.Personas
                        where p.Per_Ape.Contains(palabra) ||
                        p.Per_Cuil_Doc == doc
                        select p).ToList();
            }
        }

        public static List<Personas> GetList(string palabra, bool perFisica)
        {
            using (var context = Utiles.ContextoLocal())
            {
                int doc;
                int.TryParse(palabra, out doc);

                return (from p in context.Personas
                        where (p.Per_Ape.Contains(palabra) ||
                                    p.Per_Cuil_Doc == doc) && p.Per_PersonaFisica == perFisica
                        select p).ToList();
            }
        }

        public static List<Personas> GetList(int id)
        {
            using (var context = Utiles.ContextoLocal())
            {
                return (from p in context.Personas
                        where p.Per_Id == id
                        select p).ToList();
            }
        }

        public static Personas Get(int codigo, int numero, int control)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Personas
                            where p.Per_Cuil_Cod == codigo && p.Per_Cuil_Doc == numero && p.Per_Cuil_Con == control
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

        public static Personas Get(int id)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Personas
                            .Include("Clasificadores")
                            .Include("Clasificadores1")
                                .Include("PersonasDomicilios")
                                .Include("PersonasContactos")
                                .Include("PersonasDocumentos")
                                .Include("PersonasCaracteristicas")
                                .Include("PersonasIdiomas")
                                .Include("Clientes")
                                .Include("Empleados")
                                .Include("Proveedores")
                                .Include("Tripulantes")
                            where p.Per_Id == id
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

        public static Personas GetLimpio(int id)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Personas
                        where p.Per_Id == id
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

        public static void Update(Personas varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var viejo =
                        context.Personas.Attach(context.Personas.Single(i => i.Per_Id == varia.Per_Id));

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

        public static void Insert(Personas varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    context.Personas.Add(varia);

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

        public static void Insert(Personas varia,
            SeguridadUsuarios Usuario,
            Proveedores Proveedores)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    context.Personas.Add(varia);

                    if (Proveedores != null)
                        context.Proveedores.Add(Proveedores);
                    
                    if (Usuario != null)
                        context.SeguridadUsuarios.Add(Usuario);
                    

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
                    var valor = (from p in context.Personas where p.Per_Id == id select p).FirstOrDefault();

                    context.Personas.Remove(valor);

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
                    throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
                }
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        #endregion

        #region Web Clientes
        public static List<Personas> GetListByCli(int? user)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return (from p in context.Personas
                            .Include("Clasificadores")
                            .Include("Clasificadores1")
                            .Include("PersonasDomicilios")
                                .Include("PersonasContactos")
                                .Include("PersonasDocumentos")
                                .Include("PersonasCaracteristicas")
                                .Include("PersonasIdiomas")
                                .Include("Empleados")
                                .Include("Tripulantes")
                            select p).OrderBy(x => x.Per_SoloApe).ThenBy(x => x.Per_SoloNom).ToList();
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

        #endregion
    }
}
