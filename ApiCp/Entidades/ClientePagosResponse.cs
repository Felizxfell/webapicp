namespace ApiCp.Entidades
{
    public class ClientePagosResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Telefono { get; set; }

        public DateTime UltimoPago { get; set; }

        public List<PagosResponse> Pagos { get; set; } = new List<PagosResponse>();
    }

    public class PagosResponse
    {
        public int Id { get; set; }

        public double Monto { get; set; }

        public DateTime FechaPago { get; set; }
    }
}
