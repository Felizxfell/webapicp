using ApiCp.Entidades;
using Dapper;
using MySql.Data.MySqlClient;

namespace ApiCp.Data
{
    public class TablaPagos
    {
        private readonly string con;

        public TablaPagos(MysqlConfiguration connection)
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
        /// Obtiene todos los pagos
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PagoResponse>> GetAllPayments()
        {
            var db = DbConnection();

            var sql = @"SELECT p.id, p.cliente_id AS ClienteId, c.nombre AS NombreCliente, p.monto
                        FROM pagos AS p 
                        INNER JOIN clientes AS c ON c.id = p.cliente_id ";

            return await db.QueryAsync<PagoResponse>(sql, new { });
        }

        /// <summary>
        /// Obtiene un solo pago por el id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Pago> GetPayment(int id)
        {
            var db = DbConnection();

            var sql = @"Select id, cliente_id, monto, fecha_creacion 
                        FROM pagos 
                        WHERE id = @Id ";

            return await db.QueryFirstOrDefaultAsync<Pago>(sql, new { Id = id });
        }
        
        /// <summary>
        /// Crea un pago en la base de datos
        /// </summary>
        /// <param name="pago"></param>
        /// <returns></returns>
        public async Task<bool> InsertPayment(Pago pago)
        {
            var db = DbConnection();

            var sql = @"INSERT INTO pagos (cliente_id, monto, fecha_creacion)
                        VALUES (@ClienteId, @Monto, NOW())";

            var result = await db.ExecuteAsync(sql, new { pago.ClienteId, pago.Monto });

            return result > 0;
        }
        
        /// <summary>
        /// Actualiza un pago en la base de datos
        /// </summary>
        /// <param name="pago"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePayment(Pago pago)
        {
            var db = DbConnection();

            var sql = @"UPDATE pagos SET
                        cliente_id=@ClienteId, monto=@Monto
                        WHERE id = @Id";

            var result = await db.ExecuteAsync(sql, new { pago.ClienteId, pago.Monto, pago.Id });

            return result > 0;
        }
        
        /// <summary>
        /// Inserta un pago y actualiza la fecha del ultimo para para el cliente por medio de un storage procedure
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public async Task<bool> InsertPaymentUpdateCostumer(PagosClientesSP sp)
        {
            var db = DbConnection();

            var sql = @"CALL sp_pagoscliente(@IdCliente, @Monto, NOW());";

            var result = await db.ExecuteAsync(sql, new { sp.IdCliente, sp.Monto });

            return result > 0;
        }

        /// <summary>
        /// Eliminar un pago de la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeletePayment(int id)
        {
            var db = DbConnection();

            var sql = @"DELETE FROM pagos 
                        WHERE id = @Id ";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }
    }
}
