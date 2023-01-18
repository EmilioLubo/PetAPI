using Microsoft.EntityFrameworkCore;
using PetsAPI.Dto;
using PetsDB;
using PetsDB.Models;

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
                var getOwners = new List<OwnerDTO>();
                var owners = await _db.Owners.ToListAsync();
                foreach (var owner in owners) 
                {
                    var age = ((today - owner.Birth).Days / 365) - 1;
                    getOwners.Add(new OwnerDTO
                    {
                        OwnerId = owner.OwnerId,
                        Name = owner.Name,
                        LastName = owner.LastName,
                        Age = age
                    });
                }
                return getOwners;

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
                var owner = await _db.Owners.FindAsync(id);
                var age = ((DateTime.Now - owner.Birth).Days / 365) - 1;
                var getOwner = new OwnerDTO
                {
                    OwnerId = owner.OwnerId,
                    Name = owner.Name,
                    LastName = owner.LastName,
                    Age = age

                };
                return getOwner;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<OwnerDTO> AddOwnerAsync(OwnerAddDTO owner)
        {
            try
            {
                var age = ((DateTime.Now - owner.Birth).Days / 365) - 1;
                await _db.Owners.AddAsync(new Owner
                {
                    Name = owner.Name,
                    LastName = owner.LastName,
                    Email = owner.Email,
                    Birth = owner.Birth
                });
                await _db.SaveChangesAsync();
                var newOwner = await _db.Owners.FirstOrDefaultAsync(el => el.Email == owner.Email);
                return new OwnerDTO
                {
                    OwnerId = newOwner.OwnerId,
                    Name = newOwner.Name,
                    LastName = newOwner.LastName,
                    Age = age
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<OwnerDTO> UpdateOwnerAsync(OwnerUpdateDTO owner)
        {
            try
            {
                var isOwner = await _db.Owners.FindAsync(owner.OwnerId);
                var age = ((DateTime.Now - owner.Birth).Days / 365) - 1;
                _db.Entry(isOwner).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                var updatedOwner = await _db.Owners.FindAsync(isOwner.OwnerId);
                return new OwnerDTO
                {
                    OwnerId = updatedOwner.OwnerId,
                    Name = updatedOwner.Name,
                    LastName = updatedOwner.LastName,
                    Age = age
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<(bool, string)> DeleteOwnerAsync(OwnerDTO owner)
        {
            try
            {
                var deleteOwner = await _db.Owners.FindAsync(owner.OwnerId);
                if (deleteOwner == null)
                {
                    return (false, "Owner not found");
                }
                _db.Owners.Remove(deleteOwner);
                await _db.SaveChangesAsync();
                return (true, "Owner got deleted");
            }
            catch (Exception ex)
            {
                return (false, $"Error.{ex.Message}");
            }
        }

        #endregion Owners

        #region Pets

        Task<List<PetDTO>> ILibraryService.GetPetsAsync()
        {
            throw new NotImplementedException();
        }

        Task<PetDTO> ILibraryService.GetPetAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<PetDTO> ILibraryService.AddPetAsync(PetAddDTO pet)
        {
            throw new NotImplementedException();
        }

        Task<PetDTO> ILibraryService.UpdatePetAsync(PetUpdateDTO pet)
        {
            throw new NotImplementedException();
        }

        Task<(bool, string)> ILibraryService.DeletePetAsync(PetDTO pet)
        {
            throw new NotImplementedException();
        }

        #endregion Pets
    }
}
