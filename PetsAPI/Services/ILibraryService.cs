using PetsAPI.Dto;
using PetsDB.Models;

namespace PetsAPI.Services
{
    public interface ILibraryService
    {
        Task<List<OwnerDTO>> GetOwnersAsync();
        Task<OwnerDTO> GetOwnerAsync(int id);
        Task<OwnerDTO> AddOwnerAsync(OwnerAddDTO owner);
        Task<OwnerDTO> UpdateOwnerAsync(int id, OwnerUpdateDTO owner);
        Task<(bool, string)> DeleteOwnerAsync(OwnerDTO owner);

        Task<List<PetDTO>> GetPetsAsync();
        Task<PetDTO> GetPetAsync(int id);
        Task<PetDTO> AddPetAsync(PetAddDTO pet);
        Task<PetDTO> UpdatePetAsync(int id, PetUpdateDTO pet);
        Task<(bool, string)> DeletePetAsync(PetDTO pet);

    }
}
