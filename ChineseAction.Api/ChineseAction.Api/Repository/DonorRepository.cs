using ChineseAction.Api.Data;
using ChineseAction.Api.DTOs;
using ChineseAction.Api.NewFolder;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.InteropServices;

namespace WebApi.Repositories;

public class DonorRepository
{
    private readonly ApplicationDbContext _context;

    public DonorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    //get all donors as DonorDto
    public async Task<IEnumerable<DonorDto>> GetAllDonorsAsDtoAsync()
    {
        return await _context.Donors
            .Select(d => new DonorDto
            {
                Id = d.Id,
                Name = d.Name,
                Phone = d.Phone
            })
            .ToListAsync();
    }
    //add new donor
    public async Task<CreateDonorDtoWithGift> AddDonorAsync(CreateDonorDtoWithGift createDonorDtoWithGift)
    {
        if (createDonorDtoWithGift == null)
        {
            throw new ArgumentNullException(nameof(createDonorDtoWithGift), "Donor cannot be null.");
        }

        _context.Donors.Add((Donor)createDonorDtoWithGift);
        await _context.SaveChangesAsync();
        return createDonorDtoWithGift;
    }

    // Delete a donor by ID
    public async Task<bool> DeleteDonorAsync(int id)
    {
        var donor = await _context.Donors.FindAsync(id);
        if (donor == null)
        {
            return false; // Donor not found
        }

        _context.Donors.Remove(donor);
        await _context.SaveChangesAsync();
        return true; // Donor successfully deleted
    }

    // Update an existing donor
    public async Task<Donor?> UpdateDonorAsync(int id, Donor updatedDonor)
    {
        var donor = await _context.Donors.FindAsync(id);
        if (donor == null)
        {
            return null; // Donor not found
        }

        // Update donor properties
        donor.Name = updatedDonor.Name;
        donor.Email = updatedDonor.Email;
        donor.Phone = updatedDonor.Phone;

        _context.Donors.Update(donor);
        await _context.SaveChangesAsync();
        return donor; // Return the updated donor
    }
}