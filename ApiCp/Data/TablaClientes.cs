using ApiCp.Data;
using ApiCp.Entidades;
using Dapper;
using MySql.Data.MySqlClient;

namespace ApiCp.Data
{
    public class TablaClientes
    {
        private readonly string con;

        public TablaClientes(MysqlConfiguration connection)
        {
            con = connection.ConnectionString;
        }

        /// <summary>
        /// Metodo que nos da acceso a la conexion de la base de datos
        /// </summary>
        /// <returns></returns>
        protected MySqlConnection DbConnection()
        {
            return new MySqlConnection(con);
        }

        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Cliente>> GetAllCustomers()
        {
            var db = DbConnection();

            var sql = @"Select id, nombre, telefono, ultimo_pago 
                        FROM clientes ";

            return await db.QueryAsync<Cliente>(sql, new { });
        }

        /// <summary>
        /// Obtiene un cliente por el id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Cliente> GetCustomer(int id)
        {
            var db = DbConnection();

            var sql = @"Select id, nombre, telefono, ultimo_pago, fecha_creacion 
                        FROM clientes 
                        WHERE id = @Id ";

            return await db.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = id });
        }

        /// <summary>
        /// Crea un cliente en la base de datos
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public async Task<bool> InsertCustomer(Cliente cliente)
        {
            var db = DbConnection();

            var sql = @"INSERT INTO clientes (nombre, telefono, ultimo_pago, fecha_creacion)
                        VALUES (@Nombre, @Telefono, null, NOW())";

            var result = await db.ExecuteAsync(sql, new { cliente.Nombre, cliente.Telefono });

            return result > 0;
        }

        /// <summary>
        /// Actualiza un cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCustomer(Cliente cliente)
        {
            var db = DbConnection();

            var sql = @"UPDATE clientes SET
                        nombre=@Nombre, telefono=@Telefono, ultimo_pago=@UltimoPago
                        WHERE id = @Id";

            var result = await db.ExecuteAsync(sql, new { cliente.Nombre, cliente.Telefono, cliente.UltimoPago, cliente.Id });

            return result > 0;
        }

        /// <summary>
        /// Obtiene una coleccion de clientes con sus pagos relacionados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ClientePagosResponse> GetCustomerWithPayments(int id)
        {
            var db = DbConnection();

            var sql = @"SELECT c.id AS idcliente, c.nombre, c.telefono, c.ultimo_pago AS UltimoPago, p.id AS idpago, p.monto, p.fecha_creacion AS FechaPago 
                        FROM pagos AS p 
                        RIGHT JOIN clientes AS c ON c.id = p.cliente_id
                        WHERE c.id = @Id";

            var result = await db.QueryAsync<ClientePagos>(sql, new { Id = id });
            
            var clientpagoresp = new ClientePagosResponse();

            foreach (var p in result)
            {
                clientpagoresp.Id = p.IdCliente;
                clientpagoresp.Nombre = p.Nombre;
                clientpagoresp.Telefono = p.Telefono;
                clientpagoresp.UltimoPago = p.UltimoPago;

                var pagoresp = new PagosResponse()
                {
                    Id = p.IdPago,
                    Monto = p.Monto,
                    FechaPago = p.FechaPago,
                };
                clientpagoresp.Pagos.Add(pagoresp);
            }

            return clientpagoresp;
        }

        /// <summary>
        /// Elimina un cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCustomer(int id)
        {
            var db = DbConnection();

            var sql = @"DELETE FROM clientes 
                        WHERE id = @Id ";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }
    }
}
