using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocios
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }


        string conectionString;
        public Empleado()
        {
            conectionString = "Server=localhost;Database=VENTAS_DB;Trusted_Connection=True;";
        }

        public void Login(string usuario, string contrasena)
        {
            try
            {
                SQL sql = new SQL(conectionString);

                string query = $"SELECT Id, Nombre FROM EMPLEADOS WHERE Nombre='{usuario}' AND Contrasena = '{contrasena}'";

                Dictionary<string, object> dicEmpleado = sql.Reader(query);

                if (dicEmpleado.Count < 1)
                {
                    throw new Exception("Usuario y/o contraseña incorrectos");
                }

                int.TryParse(dicEmpleado["Id"].ToString(), out int usuarioId);

                Id = usuarioId;
                Nombre = dicEmpleado["Nombre"].ToString();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
