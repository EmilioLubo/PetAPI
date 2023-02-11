using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetsAPI.Dto;
using PetsAPI.Services;

namespace PetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public OwnerController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOwners()
        {
            var owners = await _libraryService.GetOwnersAsync();
            if (owners == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No owners in database");
            }

            return StatusCode(StatusCodes.Status200OK, owners);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetOwner(int id)
        {
            var owner = await _libraryService.GetOwnerAsync(id);

            if (owner == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No owner found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, owner);
        }

        [HttpPost]
        public async Task<ActionResult<OwnerDTO>> AddOwner(OwnerAddDTO addOwner)
        {
            var owner = await _libraryService.AddOwnerAsync(addOwner);

            if (owner == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{addOwner.Name} could not be added.");
            }

            return CreatedAtAction("GetOwner", new { id = owner.OwnerId }, owner);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateOwner(int id, OwnerUpdateDTO owner)
        {

            var newOwner = await _libraryService.UpdateOwnerAsync(id, owner);

            if (newOwner == null)
            {
                var oldOwner = await _libraryService.GetOwnerAsync(id);
                return StatusCode(StatusCodes.Status500InternalServerError, $"{oldOwner.Name} could not be updated");
            }

            return StatusCode(StatusCodes.Status201Created, $"{owner.Name} has been updated");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            var owner = await _libraryService.GetOwnerAsync(id);
            (bool status, string message) = await _libraryService.DeleteOwnerAsync(owner); ;

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, owner);
        }
    }
}
