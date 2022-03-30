namespace ApiCp.Entidades
{
    public class Pago
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public Double Monto { get; set; }

        public DateTime FechaCreacion { get; set; }
    }

    public class PagoResponse
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public string NombreCliente { get; set; }

        public Double Monto { get; set; }
    }
}
