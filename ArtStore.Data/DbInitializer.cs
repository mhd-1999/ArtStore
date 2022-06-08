using ArtStore.Models;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace ArtStore.Data
{
    internal class DbInitializer : DropCreateDatabaseIfModelChanges<ArtStoreContext>
    {
        protected override void Seed(ArtStoreContext context)
        {
            InitializeIdentity(context);

            var art1 = new Art
            {
                Id = 1,
                Name = "Ape 1",
                Description = "asdasd",
                Image = "a1.png",
                Price = 35000,
                IsDeleted = false
            };

            var art2 = new Art
            {
                Id = 2,
                Name = "Ape 2",
                Description = "asdasdas",
                Image = "a1.png",
                Price = 50000,
                IsDeleted = false
            };

            var art3 = new Art
            {
                Id = 3,
                Name = "Ape 3",
                Description = "asdasdas",
                Image = "a1.png",
                Price = 50000,
                IsDeleted = false
            };

            var art4 = new Art
            {
                Id = 4,
                Name = "Ape 4",
                Description = "asdasdas",
                Image = "a1.png",
                Price = 50000,
                IsDeleted = false
            };

            var art5 = new Art
            {
                Id = 5,
                Name = "Ape 5",
                Description = "asdasdas",
                Image = "a1.png",
                Price = 50000,
                IsDeleted = false
            };

            var art6 = new Art
            {
                Id = 6,
                Name = "Ape 6",
                Description = "asdasdas",
                Image = "a1.png",
                Price = 50000,
                IsDeleted = false
            };

            var art7 = new Art
            {
                Id = 7,
                Name = "Ape 7",
                Description = "asdasdas",
                Image = "a1.png",
                Price = 50000,
                IsDeleted = false
            };

            var categories = new List<Category>()
            {
                new Category()
                {
                    Id=1,
                    Name="Picture",
                    IsDeleted=false,
                    Arts = new List<Art>(){art2, art6}
                },
                new Category()
                {
                    Id=2,
                    Name="Digital Picture",
                    IsDeleted = false,
                    Arts=new List<Art>{art1, art7}
                },
                new Category()
                {
                    Id=3,
                    Name="Statue",
                    IsDeleted=false,
                    Arts=new List<Art>{art3,art4,art5}
                }
            };

            context.Categories.AddRange(categories);

            context.SaveChanges();
        }

        public static void InitializeIdentity(ArtStoreContext db)
        {
            var userManager = new UserManager<Account>(new UserStore<Account>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            const string name = "admin@example.com";
            const string password = "admin123";
            const string roleName = "Administrator";

            #region Role
            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleResult = roleManager.Create(role);
            }

            var customer = roleManager.FindByName("Customer");
            if (customer == null)
            {
                customer = new IdentityRole("Customer");
                var roleResult = roleManager.Create(customer);
            }
            #endregion

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new Account { UserName = name, Email = name, Address = "HaNoi", NumberCard = "222222222222", Cvv = "1234", PhoneNumber = "0888866546",Name="Duong" };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }


            //Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}