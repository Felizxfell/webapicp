namespace ApiCp.Entidades
{
    public class ClientePagos
    {
        public int IdCliente { get; set; }

        public string Nombre { get; set; }

        public string Telefono { get; set; }

        public DateTime UltimoPago { get; set; }

        public int IdPago { get; set; }

        public double Monto { get; set; }

        public DateTime FechaPago { get; set; }
    }
}
