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

        private readonly IBaseDeDatos baseDeDatos;
        
        public Empleado(SeleccionBaseDeDatos.TipoBaseDeDatos tipoBaseDeDatos, string fuente)
        {
            baseDeDatos = SeleccionBaseDeDatos.Seleccionar(tipoBaseDeDatos, fuente);
        }

        public void Login(string usuario, string contrasena)
        {
            try
            {
                string query = "";
                if(baseDeDatos is SQL) 
                {
                    query = $"SELECT Id, Nombre FROM EMPLEADOS WHERE Nombre='{usuario}' AND Contrasena = '{contrasena}'";
                }
                else 
                {
                    query = $"SELECT Id, Nombre FROM [EMPLEADOS$] WHERE Nombre='{usuario}' AND Contrasena = '{contrasena}'";
                }
                

                Dictionary<string, object> dicEmpleado = baseDeDatos.Reader(query);

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
