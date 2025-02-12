using APIstarts_API.Data;
using APIstarts_API.Logging;
using APIstarts_API.Models;
using APIstarts_API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APIstarts_API.Controllers
{
    //[Route("api/[controller]")] [controller] yazdığında otomatik olarak ismi alır fakat manuel yazmak daha sağlıklı olacaktır.
    [Route("api/APIStarts")]
    [ApiController]    //otomatik ayarlamalar yapar .
    public class APIStartsController : ControllerBase
    {
        private readonly ILogging _logger;
        public APIStartsController(ILogging logger)

        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()   //Tüm villaları döndürür.   VillaDTO verilerin taşınması için kullanılan bir Data Transfer Object'tir.    
        {
            _logger.Log("Getting All Villas","");
            return Ok(VillaStore.villaList);

        }
        [HttpGet("{id:int}", Name = "GetVilla")]  //belirli bir villa ID'sine göre GET isteği yapılacağını belirtir.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]


        public ActionResult<VillaDTO> GetVilla(int id)    // belirtilen id'ye sahip villa bilgilerini döndürür.
        {
            if (id == 0)
            {
                _logger.Log("Get villa error with Id" + id, "error");
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound(); 
            }

            return Ok(villa);  //villaList içinde id parametresi ile eşleşen ilk villayı bulur ve döndürür. Eğer bulunmazsa, null döner.

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
        {
        

            if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower())!= null)
            {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }

            if (villaDTO == null)
            {  
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;   //VillaDTO.Id villaList'teki son villanın ID'sini alır ve yeni villaya bir id atanır.
            VillaStore.villaList.Add(villaDTO);

            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);   //CreatedAtRoute, yeni oluşturulan villa için bir "Created" HTTP yanıtı döner ve bu yanıtın içinde yeni villayı ve ona ulaşılabilecek rotayı belirtir.
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        [HttpDelete("{id:int}", Name = "DeleteVilla")]

        public IActionResult DeleteVilla(int id)
        {
            if (id == 0 )
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u =>u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();

            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            villa.Name= villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;


            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null ||id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {

            return BadRequest();
            }
            patchDTO.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent ();
        }

    }
}