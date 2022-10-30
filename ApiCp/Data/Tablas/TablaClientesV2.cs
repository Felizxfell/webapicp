using ApiCp.Entidades;
using System.Data;

namespace ApiCp.Data.Tablas
{
    public class TablaClientesV2
    {
        private ConexionBD _con;

        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataTable> GetAllCustomers()
        {
            _con = new ConexionBD();

            _con.AbrirConexion();
            string sql = @"Select id, nombre, telefono, ultimo_pago 
                        FROM clientes ";
            _con.SentenciaSQL(sql);
            DataTable dt = _con.EjecutaConsulta();
            yield return dt;
        }
    }
}
