using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;

namespace LogicaNegocios
{
    public class SeleccionBaseDeDatos
    {
        public static IBaseDeDatos Seleccionar(TipoBaseDeDatos tipoBaseDeDatos, string fuente) 
        {
            if (tipoBaseDeDatos == TipoBaseDeDatos.Excel) 
            {
                return new Excel(fuente);
            }
            else 
            {
                return new SQL(fuente);
            }
        }
        public enum TipoBaseDeDatos
        {
            Excel,
            SQL
        }
    }
}
