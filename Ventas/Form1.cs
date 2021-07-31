using LogicaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Ventas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            cmbBaseDeDatos.DataSource = Enum.GetValues(typeof(SeleccionBaseDeDatos.TipoBaseDeDatos));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBaseDeDatos.SelectedValue.ToString() == "SQL")
                {
                    Global.TipoBaseDeDatos = SeleccionBaseDeDatos.TipoBaseDeDatos.SQL;
                    Global.FuenteDeDatos = "Server=localhost;Database=VENTAS_DB;Trusted_Connection=True;";
                }
                else 
                {
                    Global.TipoBaseDeDatos = SeleccionBaseDeDatos.TipoBaseDeDatos.Excel;
                    Global.FuenteDeDatos = Path.Combine(Environment.CurrentDirectory, "Database.xlsx");
                }


                Empleado empleado = new Empleado(Global.TipoBaseDeDatos, Global.FuenteDeDatos);
                empleado.Login("Antonio", "123");

                Global.UsuarioId = empleado.Id;
                Global.NombreUsuario = empleado.Nombre;

                Clientes frmCliente = new Clientes();
                frmCliente.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
