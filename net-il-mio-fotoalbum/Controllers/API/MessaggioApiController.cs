using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessaggioApiController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreaMessaggio([FromBody] Messaggio messaggio)
        {
            try
            {
                using (FotoContext db = new FotoContext())
                {
                    db.Messaggi.Add(messaggio);
                    db.SaveChanges();

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message});
            }
            
        }
    }
}
