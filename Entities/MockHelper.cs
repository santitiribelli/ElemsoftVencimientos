using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public sealed class MockHelper
    {
        private static MockHelper instance = null;
        private static readonly object Instancelock = new object();
        private MockHelper()
        {

            try
            {
                IsEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["Mock"]);
            }
            catch
            {
                IsEnabled = false;
            }
            if (IsEnabled)
            {
                Idiomas = new List<IdiomasPalabras>();

                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Seguridad - Acceso Denegado", IdiPal_Texto = "Roles" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Menu - Auxiliares - Roles", IdiPal_Texto = "Roles" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Empleados - Empleados - App Acceso", IdiPal_Texto = "App Acceso" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Usuarios - Rol", IdiPal_Texto = "Usuarios - Rol" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Usuarios - Asignar Permisos", IdiPal_Texto = "Usuarios - Rol" });

                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Grilla - Paginado", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Grilla - Buscar sin resultados", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Grilla - No hay datos", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Grilla - Info", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Grilla - Info sin datos", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Grilla - Info Filtro", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Grilla - Buscador", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Grilla - Cargando", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Grilla - Primero", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Grilla - Ultimo", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Grilla - Siguiente", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Grilla - Anterior", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Permisos - Permisos de", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Permisos - No tiene Permiso", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Menu - Auxiliares - Usuarios", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Usuarios - Usuario", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Usuarios - Fecha Baja", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Usuarios - Fecha Alta", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Usuarios - Resetear Contraseña", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Usuarios - Asignar Permisos", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Usuarios - Texto Cambio Contraseña", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Menu - Auxiliares - Clasificadores", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Clasificadores - Clasificador", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Clasificadores - Descripcion", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Clasificadores - Abreviatura", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Clasificadores - Depende de", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Clasificadores - Fecha Baja", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Menu - Auxiliares - Idiomas", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Clasificadores - Fecha Alta", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Clasificadores - Ultimo Nivel", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Idiomas - Idioma", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Idiomas - Texto", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Validacion - Solo Numeros", IdiPal_Texto = "" });
                Idiomas.Add(new IdiomasPalabras() { IdiPal_Clave = "Idiomas - Textos Incompletos", IdiPal_Texto = "" });

            }
        }

        public static MockHelper GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (Instancelock)
                    {
                        if (instance == null)
                        {
                            instance = new MockHelper();
                        }
                    }
                }
                return instance;
            }
        }

        public bool IsEnabled { private set; get; }
        public List<Entities.IdiomasPalabras> Idiomas { private set; get; }

    }
}
