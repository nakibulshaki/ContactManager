﻿using ContactManager.DAL;
using ContactMangaer.Data.Enums;
using Microsoft.EntityFrameworkCore;
namespace ContactMangaer.Data.Extentions;
public static class ModelBuilderExtensions
{
    public static void ConfigureEntity(this ModelBuilder modelBuilder)
    {


        // Configure the relationship between Contact and EmailAddress
        modelBuilder.Entity<Contact>()
            .HasMany(c => c.EmailAddresses)
            .WithOne(e => e.Contact)
            .HasForeignKey(e => e.ContactId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure the relationship for the PrimaryEmailAddress using Fluent API
        modelBuilder.Entity<Contact>()
            .HasOne(c => c.PrimaryEmailAddress)
            .WithOne()  // Remove the navigation property on the other side
            .HasForeignKey<Contact>(c => c.PrimaryEmailAddressId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<EmailAddress>()
            .HasIndex(e => new { e.ContactId, e.IsPrimary })
            .HasFilter("IsPrimary = 1")
            .HasName("UQ_EmailAddress_ContactId_IsPrimary");
    }
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        // Seed data for the specified entity using modelBuilder.Entity<TEntity>().HasData()
        var bill =
                new Contact
                {
                    Id = new Guid("930d4f10-9daf-4582-b4bb-cb9abfd382b3"),
                    Title = "Mr.",
                    FirstName = "Bill",
                    LastName = "Gates",
                    DOB = new DateTime(1960, 05, 01),
                };

        var steve =
            new Contact
            {
                Id = new Guid("b728f6ef-65d8-4da2-8e5f-0f67e3c3401c"),
                Title = "Mr.",
                FirstName = "Steve",
                LastName = "Jobs",
                DOB = new DateTime(1950, 09, 21),
            };

        var sundar =
            new Contact
            {
                Id = new Guid("99580d68-9d2f-4552-862e-06b3204193f1"),
                Title = "Mr.",
                FirstName = "Sundar",
                LastName = "Pichai",
                DOB = new DateTime(1980, 01, 11),
            };


        modelBuilder.Entity<Contact>().HasData(bill, steve, sundar);

        modelBuilder.Entity<EmailAddress>().HasData(
            new
            {
                Id = new Guid("5111f412-a7f4-4169-bb27-632687569ccd"),
                Email = "Bill@gates.com",
                Type = EmailType.Personal,
                ContactId = bill.Id,
                IsPrimary = true
            },

            new
            {
                Id = new Guid("3ddeb084-5e5d-4eca-b275-e4f6886e04dc"),
                Email = "Steve@Jobs.com",
                Type = EmailType.Personal,
                ContactId = steve.Id,
                IsPrimary = true

            },
            new
            {
                Id = new Guid("d1a50413-20c0-4972-a351-8be24e1fc939"),
                Email = "SundarPichai@gmail.com",
                Type = EmailType.Business,
                ContactId = sundar.Id,
                IsPrimary = false

            });

        modelBuilder.Entity<Address>().HasData(
            new
            {
                Id = new Guid("b39467d9-a1f4-4843-aee9-7e9060448ded"),
                Street1 = "10 Main St",
                Street2 = string.Empty,
                City = "Melvile",
                State = "NY",
                Zip = 11757,
                Type = AddressType.Primary,
                ContactId = bill.Id
            },

            new
            {
                Id = new Guid("1632295e-fc3c-49c5-b30f-d673fc816b73"),
                Street1 = "245 Coral Place",
                Street2 = "Appt #3",
                City = "Westbury",
                State = "NY",
                Zip = 11590,
                Type = AddressType.Primary,
                ContactId = steve.Id
            },

            new
            {
                Id = new Guid("a0762d8b-9dc6-4fa0-99dc-6f7438732760"),
                Street1 = "1 Apple Way",
                Street2 = string.Empty,
                City = "Los Angles",
                State = "CA",
                Zip = 11757,
                Type = AddressType.Business,
                ContactId = steve.Id
            },

            new
            {
                Id = new Guid("7931505c-fca8-4a35-8d24-b32df341643d"),
                Street1 = "10 Main St",
                Street2 = string.Empty,
                City = "Melvile",
                State = "NY",
                Zip = 11757,
                Type = AddressType.Primary,
                ContactId = sundar.Id
            });

    }
}
