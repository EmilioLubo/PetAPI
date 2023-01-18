using Microsoft.EntityFrameworkCore;
using PetsAPI.Dto;
using PetsDB;

namespace PetsAPI.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly PetsContext _db;

        public LibraryService(PetsContext db) 
        {
            _db = db;
        }

        #region Owners

        public async Task<List<OwnerDTO>> GetOwnersAsync()
        {
            try
            {
                var today = DateTime.Now;
                var newOwners = new List<OwnerDTO>();
                var owners = await _db.Owners.ToListAsync();
                foreach (var owner in owners) 
                {
                    var edad = ((today - owner.Birth).Days / 365) - 1;
                    newOwners.Add(new OwnerDTO
                    {
                        OwnerId = owner.OwnerId,
                        Name = owner.Name,
                        LastName = owner.LastName,
                        Age = edad
                    });
                }
                return newOwners;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<OwnerDTO> GetOwnerAsync(int id)
        {
            try
            {
                return null
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        Task<OwnerDTO> ILibraryService.AddOwnerAsync(OwnerAddDTO owner)
        {
            throw new NotImplementedException();
        }

        Task<PetDTO> ILibraryService.AddPetAsync(PetAddDTO pet)
        {
            throw new NotImplementedException();
        }

        Task<(bool, string)> ILibraryService.DeleteOwnerAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<(bool, string)> ILibraryService.DeletePetAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<PetDTO> ILibraryService.GetPetAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<PetDTO>> ILibraryService.GetPetsAsync()
        {
            throw new NotImplementedException();
        }

        Task<OwnerDTO> ILibraryService.UpdateOwnerAsync(OwnerUpdateDTO owner)
        {
            throw new NotImplementedException();
        }

        Task<PetDTO> ILibraryService.UpdatePetAsync(PetUpdateDTO pet)
        {
            throw new NotImplementedException();
        }
    }
}
