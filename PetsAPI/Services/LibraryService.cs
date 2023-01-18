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

        public async Task<OwnerDTO> UpdateOwnerAsync(int id, OwnerUpdateDTO owner)
        {
            try
            {
                var age = ((DateTime.Now - owner.Birth).Days / 365) - 1;
                _db.Entry(owner).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                var updatedOwner = await _db.Owners.FindAsync(id)
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

        public async Task<List<PetDTO>> GetPetsAsync()
        {
            try
            {
                var today = DateTime.Now;
                var getPets = new List<PetDTO>();
                var pets = await _db.Pets.ToListAsync();
                foreach (var pet in pets)
                {
                    var age = ((today - pet.PetBirth).Days / 365) - 1;
                    getPets.Add(new PetDTO
                    {
                        PetId = pet.PetId,
                        PetName = pet.PetName,
                        Type = pet.Type,
                        Breed = pet.Breed,
                        Genre = pet.Genre,
                        Age = age,
                        OwnerId = pet.OwnerId,
                        Owner = pet.Owner
                    });
                }
                return getPets;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PetDTO> GetPetAsync(int id)
        {
            try
            {
                var pet = await _db.Pets.FindAsync(id);
                var age = ((DateTime.Now - pet.PetBirth).Days / 365) - 1;
                var getPet = new PetDTO
                {
                    PetId = pet.PetId,
                    PetName = pet.PetName,
                    Type = pet.Type,
                    Breed = pet.Breed,
                    Genre = pet.Genre,
                    Age = age,
                    OwnerId = pet.OwnerId,
                    Owner = pet.Owner

                };
                return getPet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PetDTO> AddPetAsync(PetAddDTO pet)
        {
            try
            {
                var age = ((DateTime.Now - pet.PetBirth).Days / 365) - 1;
                await _db.Pets.AddAsync(new Pet
                {
                    PetName = pet.PetName,
                    Type = pet.Type,
                    Breed = pet.Breed,
                    Genre = pet.Genre,
                    PetBirth = pet.PetBirth,
                    OwnerId = pet.OwnerId
                });
                await _db.SaveChangesAsync();
                var newPet = await _db.Pets.FirstOrDefaultAsync(el => el.OwnerId == pet.OwnerId);
                return new PetDTO
                {
                    PetId = newPet.PetId,
                    PetName = newPet.PetName,
                    Type = newPet.Type,
                    Breed = newPet.Breed,
                    Genre = newPet.Genre,
                    Age = age,
                    OwnerId = newPet.OwnerId,
                    Owner = newPet.Owner
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PetDTO> UpdatePetAsync( PetUpdateDTO pet)
        {
            try
            {
                var age = ((DateTime.Now - pet.PetBirth).Days / 365) - 1;
                _db.Entry(pet).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                var updatedPet = await _db.Pets.FindAsync(pet.PetId);
                return new PetDTO
                {
                    PetId = updatedPet.PetId,
                    PetName = updatedPet.PetName,
                    Type = updatedPet.Type,
                    Breed = updatedPet.Breed,
                    Genre = updatedPet.Genre,
                    Age = age,
                    OwnerId = updatedPet.OwnerId,
                    Owner = updatedPet.Owner
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePetAsync(PetDTO pet)
        {
            try
            {
                var deletePet = await _db.Pets.FindAsync(pet.PetId);
                if (deletePet == null)
                {
                    return (false, "Pet not found");
                }
                _db.Pets.Remove(deletePet);
                await _db.SaveChangesAsync();
                return (true, "Pet got deleted");
            }
            catch (Exception ex)
            {
                return (false, $"Error.{ex.Message}");
            }
        }

        #endregion Pets
    }
}
