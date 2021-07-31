using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public interface IBaseDeDatos
    {
        bool ProbarConexion();
        object Scalar(string query);
        int NonQuery(string query);
        Dictionary<string, object> Reader(string query);
        DataTable ObtenerDataTable(string query);
    }
}
