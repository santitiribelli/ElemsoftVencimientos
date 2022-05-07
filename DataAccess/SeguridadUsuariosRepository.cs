using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess
{
    public static class SeguridadUsuariosRepository
    {
        public static SeguridadUsuarios Login(int pUsername)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    return
                        (from p in context.SeguridadUsuarios.Include("Personas1").Include("SeguridadRoles")
                         where (p.Personas1.Per_Cuil_Doc == pUsername || p.Personas1.Per_Doc_Extranjero == pUsername.ToString())
                         select p).FirstOrDefault();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ErrorHelper.dbError(ex));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SeguridadUsuarios> GetList()
        {
            using (var context = Utiles.ContextoLocal())
            {

                return (from p in context.SeguridadUsuarios.Include("SeguridadRoles").Include("Personas1")
                        select p).ToList();
            }
        }

        public static List<SeguridadUsuarios> GetListAdministradores()
        {
            using (var context = Utiles.ContextoLocal())
            {

                return (from p in context.SeguridadUsuarios.Include("SeguridadRoles").Include("Personas1")
                        where p.SeguridadRoles.SegRol_Descr.ToLower() == "administrador"
                    select p).ToList();
            }
        }

        public static SeguridadUsuarios Get(int id)
        {
            using (var context = Utiles.ContextoLocal())
            {
                return (from p in context.SeguridadUsuarios.Include("Personas1").Include("SeguridadRoles")
                        where p.SegUsu_Id == id
                        select p).FirstOrDefault();
            }
        }

        public static void Insert(SeguridadUsuarios varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    context.SeguridadUsuarios.Add(varia);

                    var paginas = SeguridadPaginasRepository.GetList();
                    foreach (var item in paginas)
                    {
                        var algo = new SeguridadUsuariosPaginas();
                        algo.SegPagUsu_Alta = false;
                        algo.SegPagUsu_Expo = false;
                        algo.SegPagUsu_Ver = false;
                        algo.SegPag_Id = item.SegPag_Id;
                        algo.SeguridadUsuarios = varia;
                        context.SeguridadUsuariosPaginas.Add(algo);
                    }

                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ErrorHelper.dbError(ex));
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("uniqueUsuario"))
                    throw new Exception("Ya existe un usuario con ese nombre.");
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static void Update(SeguridadUsuarios varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var viejo =
                        context.SeguridadUsuarios.Attach(
                            context.SeguridadUsuarios.Single(i => i.SegUsu_Id == varia.SegUsu_Id));

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
                if (ex.InnerException.InnerException.Message.Contains("uniqueUsuario"))
                    throw new Exception("Ya existe un usuario con ese nombre.");
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        #region Sitio Clientes

        public static SeguridadUsuarios LoginClientes(string pUsername)
        {
            try
            {
                int temp;
                Int32.TryParse(pUsername, out temp);

                using (var context = Utiles.ContextoLocal())
                {
                    return
                        (from p in context.SeguridadUsuarios.Include("Personas1.Empleados")
                            where (p.Personas1.Per_Cuil_Doc == temp || p.Personas1.Per_Doc_Extranjero == pUsername) 
                            select p).FirstOrDefault();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ErrorHelper.dbError(ex));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
