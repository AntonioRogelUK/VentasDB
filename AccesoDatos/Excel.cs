using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class Excel : IBaseDeDatos
    {
        private readonly string connectionString;
        private readonly string archivo;
        public Excel(string archivo)
        {
            if (string.IsNullOrWhiteSpace(archivo))
            {
                throw new Exception("Debe especificar el nombre del archivo");
            }

            this.archivo = archivo;
            connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={archivo};" +
                    $"Extended Properties='Excel 12.0 Xml; HDR = YES; IMEX = 1'";
        }

        public bool ProbarConexion()
        {
            if (!File.Exists(archivo))
            {
                throw new Exception($"No se encontró el archivo {archivo}");
            }

            using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
            {
                try
                {
                    oleDbConnection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    string strError = ex.Message;
                    if (strError.IndexOf("Microsoft.ACE.OLEDB.12.0") > -1)
                    {
                        throw new Exception("El proveedor de acceso a datos no está instalado Microsoft.ACE.OLEDB.12.0");
                    }

                    throw new Exception(strError);
                }
            }
        }

        public object Scalar(string query)
        {
            using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand(query, oleDbConnection))
                {
                    oleDbCommand.CommandType = CommandType.Text;
                    oleDbConnection.Open();
                    return oleDbCommand.ExecuteScalar();
                }
            }
        }

        public int NonQuery(string query)
        {
            using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand(query, oleDbConnection))
                {
                    oleDbCommand.CommandType = CommandType.Text;
                    oleDbConnection.Open();
                    return oleDbCommand.ExecuteNonQuery();
                }
            }
        }

        public Dictionary<string, object> Reader(string query)
        {
            using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand(query, oleDbConnection))
                {
                    oleDbCommand.CommandType = CommandType.Text;
                    oleDbConnection.Open();
                    OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
                    int cuentaReader = oleDbDataReader.FieldCount - 1;

                    Dictionary<string, object> dicRespuesta = new Dictionary<string, object>();

                    while (oleDbDataReader.Read())
                    {
                        if (!oleDbDataReader.IsDBNull(0))
                        {
                            for (int i = 0; i <= cuentaReader; i++)
                            {
                                dicRespuesta.Add(
                                    oleDbDataReader.GetName(i).ToString(),
                                    oleDbDataReader.GetValue(i));
                            }
                        }
                    }

                    return dicRespuesta;
                }
            }
        }

        public DataTable ObtenerDataTable(string query)
        {
            using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand oleDbCommand = new OleDbCommand(query, oleDbConnection))
                {
                    oleDbCommand.CommandType = CommandType.Text;
                    oleDbConnection.Open();

                    DataTable dtRespuesta = new DataTable();

                    using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
                    {
                        oleDbDataAdapter.Fill(dtRespuesta);
                    }

                    return dtRespuesta;
                }
            }
        }
    }
}
