using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<MenuItem> MenuItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<MenuItem>().HasData(
            new MenuItem
            {
                Id = 1,
                Name = "Samosa",
                Description = "Crispy and delicious samosa filled with spiced potatoes and peas.",
                Image = "",
                Price = 2.99,
                Category = "Appetizer",
                SpecialTag = "",
            },
            new MenuItem
            {
                Id = 2,
                Name = "Paneer Tikka",
                Description = "Grilled paneer cubes marinated in a blend of spices and yogurt.",
                Image = "",
                Price = 9.99,
                Category = "Main Course",
                SpecialTag = "",
            },
            new MenuItem
            {
                Id = 3,
                Name = "Gulab Jamun",
                Description = "Soft and sweet gulab jamun soaked in rose-flavored syrup.",
                Image = "",
                Price = 4.99,
                Category = "Dessert",
                SpecialTag = "",
            },
            new MenuItem
            {
                Id = 4,
                Name = "Gulab Jamun",
                Description = "Soft and sweet gulab jamun soaked in rose-flavored syrup.",
                Image = "",
                Price = 4.99,
                Category = "Dessert",
                SpecialTag = "",
            },
            new MenuItem
            {
                Id = 5,
                Name = "Gulab Jamun",
                Description = "Soft and sweet gulab jamun syrup.",
                Image = "",
                Price = 4.99,
                Category = "Dessert",
                SpecialTag = "",
            },
            new MenuItem
            {
                Id = 6,
                Name = "Gulab Jamun",
                Description = "Soft and sweet gulab jamun syrup.",
                Image = "",
                Price = 4.99,
                Category = "Dessert",
                SpecialTag = "",
            },
            new MenuItem
            {
                Id = 7,
                Name = "Gulab Jamun",
                Description = "Soft and sweet gulab jamun syrup.",
                Image = "",
                Price = 4.99,
                Category = "Dessert",
                SpecialTag = "",
            },
            new MenuItem
            {
                Id = 8,
                Name = "Gulab Jamun",
                Description = "Soft and sweet gulab jamun syrup.",
                Image = "",
                Price = 4.99,
                Category = "Dessert",
                SpecialTag = "",
            },
            new MenuItem
            {
                Id = 9,
                Name = "Gulab Jamun",
                Description = "Soft and sweet gulab jamun syrup.",
                Image = "",
                Price = 4.99,
                Category = "Dessert",
                SpecialTag = "",
            }
        );
    }
}

