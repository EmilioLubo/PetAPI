using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetsAPI.Dto;
using PetsAPI.Services;

namespace PetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public PetController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPets()
        {
            var pets = await _libraryService.GetPetsAsync();
            if (pets == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No pets in database");
            }

            return StatusCode(StatusCodes.Status200OK, pets);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetPet(int id)
        {
            var pet = await _libraryService.GetPetAsync(id);

            if (pet == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No pet found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, pet);
        }

        [HttpPost]
        public async Task<ActionResult<PetDTO>> AddPet(PetAddDTO addPet)
        {
            var pet = await _libraryService.AddPetAsync(addPet);

            if (pet == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{addPet.PetName} could not be added.");
            }

            return CreatedAtAction("GetPet", new { id = pet.PetId }, pet);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdatePet(int id, PetUpdateDTO pet)
        {
            var newPet = await _libraryService.UpdatePetAsync(pet);

            if (newPet == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{newPet.PetName} could not be updated");
            }

            return StatusCode(StatusCodes.Status201Created, $"{newPet.PetName} has been updated");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await _libraryService.GetPetAsync(id);
            (bool status, string message) = await _libraryService.DeletePetAsync(pet); ;

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, pet);
        }
    }
}
