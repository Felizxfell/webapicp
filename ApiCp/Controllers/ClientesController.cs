using ApiCp.Data;
using ApiCp.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace ApiCp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        private readonly TablaClientes tabla;

        /// <summary>
        /// Cargamos el objeto que nos da acceso a los metodos de nuestra tabla
        /// </summary>
        /// <param name="table"></param>
        public ClientesController(TablaClientes table)
        {
            tabla = table;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientes()
        {
            return Ok(await tabla.GetAllCustomers());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            return Ok(await tabla.GetCustomer(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] Cliente cliente)
        {
            if (cliente is null)
                return BadRequest("El cliente esta vacio");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await tabla.InsertCustomer(cliente);

            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCliente([FromBody] Cliente cliente)
        {
            if (cliente is null)
                return BadRequest("El cliente esta vacio");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await tabla.UpdateCustomer(cliente);

            return Ok();
        }
        
        [HttpGet("clientepagos/{id:int}", Name = "clientepagos")]
        public async Task<IActionResult> GetClienteConpagos(int id)
        {
            return Ok(await tabla.GetCustomerWithPayments(id));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            await tabla.DeleteCustomer(id);

            return Ok();
        }
    }
}
