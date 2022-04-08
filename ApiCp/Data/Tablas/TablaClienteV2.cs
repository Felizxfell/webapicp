using ApiCp.Entidades;
using System.Data;

namespace ApiCp.Data.Tablas
{
    public class TablaClienteV2
    {
        private ConexionBD _con;

        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCustomers()
        {
            _con = new ConexionBD();

            _con.AbrirConexion();
            string sql = @"Select id, nombre, telefono, ultimo_pago 
                        FROM clientes ";
            _con.SentenciaSQL(sql);
            DataTable dt = _con.EjecutaConsulta();

            return dt;
        }
    }
}
