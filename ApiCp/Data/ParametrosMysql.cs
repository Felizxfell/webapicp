using System.Data;

namespace ApiCp.Data
{
    public class ParametrosMysql
    {
        public string NombreParametro { get; set; }

        public DbType TipoParametro { get; set; }

        public Object ValorParametro { get; set; }
    }
}
