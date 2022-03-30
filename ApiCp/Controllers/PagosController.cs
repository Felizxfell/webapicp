using ApiCp.Data;
using ApiCp.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace ApiCp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : Controller
    {
        private readonly TablaPagos tabla;

        /// <summary>
        /// Cargamos el objeto que nos da acceso a los metodos de nuestra tabla
        /// </summary>
        /// <param name="table"></param>
        public PagosController(TablaPagos table)
        {
            tabla = table;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPagos()
        {
            return Ok(await tabla.GetAllPayments());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPago(int id)
        {
            return Ok(await tabla.GetPayment(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePago([FromBody] Pago pago)
        {
            if (pago is null)
                return BadRequest("El pago esta vacio");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await tabla.InsertPayment(pago);

            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePago([FromBody] Pago pago)
        {
            if (pago is null)
                return BadRequest("El cliente esta vacio");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await tabla.UpdatePayment(pago);

            //return NoContent();
            return Ok();
        }

        [HttpPost("pagosclientesinsert")]
        public async Task<IActionResult> InsertPagoUpdateCliente([FromBody] PagosClientesSP sp)
        {
            if (sp is null)
                return BadRequest("El request esta vacio");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await tabla.InsertPaymentUpdateCostumer(sp);

            return Created("insert sp", created);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            await tabla.DeletePayment(id);

            return Ok();
        }
    }
}
