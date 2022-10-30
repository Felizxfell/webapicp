using ApiCp.Data.Tablas;
using ApiCp.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ApiCp.Controllers.v2
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesV2Controller : Controller
    {
        private readonly TablaClientesV2 tabla;

        public ClientesV2Controller(TablaClientesV2 table)
        {
            tabla = table;
        }

        [HttpGet]
        public IEnumerable<DataTable> GetAllClientes()
        {
            return tabla.GetAllCustomers();
        }
    }
}
