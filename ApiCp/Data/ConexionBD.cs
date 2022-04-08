using System.Configuration;
using System.Data;
using System.Web;
using Microsoft.AspNetCore.Builder;
using MySql.Data.MySqlClient;

namespace ApiCp.Data
{
    public class ConexionBD : ParametrosMysql
    {
        private readonly IConfiguration _config;

        private readonly string _ParameterPrefix = "@";
        private MySqlConnection Connection { get; set; }
        private MySqlCommand Command { get; set; }
        private MySqlDataAdapter DataAdapter { get; set; }        

        public ConexionBD()
        {
            _config = WebApplication.CreateBuilder().Configuration;
        }
        public void AbrirConexion()
        {
            Connection = new MySqlConnection
            {
                ConnectionString = _config.GetConnectionString("DefaultConnection")
            };
            Command = Connection.CreateCommand();
            Command.Connection.Open();
        }

        public void SentenciaSQL(string psentenciasql, string pcommandtype = "text")
        {
            Command.CommandText = psentenciasql;
            Command.CommandType = CommandType.Text;
            if (pcommandtype == "store")
            {
                Command.CommandType = CommandType.StoredProcedure;
            }
        }

        public void LimpiarParametros()
        {
            Command.Parameters.Clear();
        }

        public void AgregarParametro(List<ParametrosMysql> parametros)
        {
            foreach (var p in parametros)
            {
                MySqlParameter parameter = Command.CreateParameter();
                //_ = new MySqlParameter()
                //{
                //    ParameterName = p.NombreParametro.Replace(_ParameterPrefix, "?"),
                //    DbType = p.TipoParametro,
                //    Value = p.ValorParametro
                //};
                parameter.ParameterName = p.NombreParametro.Replace(_ParameterPrefix, "?");
                parameter.DbType = p.TipoParametro;
                parameter.Value = p.ValorParametro;
                //MySqlParameter parameter = Command.CreateParameter();
                Command.Parameters.Add(parameter);
            }
        }

        public void EjecutaSQL()
        {
            Command.ExecuteNonQuery();
        }

        public DataTable EjecutaConsulta()
        {
            DataTable ResultadoDataTable = new DataTable();

            DataAdapter = new MySqlDataAdapter();
            DataAdapter.SelectCommand = Command;
            DataAdapter.Fill(ResultadoDataTable);

            return ResultadoDataTable;
        }

        public long EjecutaSQLRegresaId()
        {
            Command.ExecuteNonQuery();

            return Command.LastInsertedId;
        }

        public object EjecutaSQLScalar()
        {
            object resp = Command.ExecuteScalar();

            return resp;
        }

        public void CerrarConexion()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }

            if (Connection != null)
            {
                Connection.Dispose();
            }
        }
    }
}
