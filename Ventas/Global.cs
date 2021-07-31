using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocios;

namespace Ventas
{
    public static class Global
    {
        public static int UsuarioId { get; set; }
        public static string NombreUsuario { get; set; }

        public static SeleccionBaseDeDatos.TipoBaseDeDatos TipoBaseDeDatos { get; set; }
        public static string FuenteDeDatos { get; set; }
    }
}
