﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VegankoService.Models;
using VegankoService.Models.User;

namespace VegankoService.Data.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly VegankoContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersRepository(VegankoContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public CustomerProfile GetProfile(string identityId)
        {
             IQueryable<CustomerProfile> query = 
                from customer in context.Customer
                where customer.IdentityId == identityId
                join appUser in context.Users on customer.IdentityId equals appUser.Id
                join userRole in context.UserRoles on customer.IdentityId equals userRole.UserId
                join role in context.Roles on userRole.RoleId equals role.Id
                select new CustomerProfile
                {
                    Id = customer.Id,
                    Username = appUser.UserName,
                    Email = appUser.Email,
                    AvatarId = customer.AvatarId,
                    Description = customer.Description,
                    Label = customer.Label,
                    ProfileBackgroundId = customer.ProfileBackgroundId,
                    Role = role.Name
                };

            return query.FirstOrDefault();
        }

        public PagedList<CustomerProfile> GetAll(int page, int pageSize = 10)
        { 
            // Paged customers
            IQueryable<Customer> customers = context.Customer
                .Skip(page * pageSize)
                .Take(pageSize);

            IQueryable<CustomerProfile> customerProfiles =
                from customer in customers
                join appUser in context.Users on customer.IdentityId equals appUser.Id
                join userRole in context.UserRoles on customer.IdentityId equals userRole.UserId
                join role in context.Roles on userRole.RoleId equals role.Id
                select new CustomerProfile
                {
                    Id = customer.Id,
                    Username = appUser.UserName,
                    Email = appUser.Email,
                    AvatarId = customer.AvatarId,
                    Description = customer.Description,
                    Label = customer.Label,
                    ProfileBackgroundId = customer.ProfileBackgroundId,
                    Role = role.Name
                };

            return new PagedList<CustomerProfile>
            {
                Items = customerProfiles.ToList(),
                Page = page,
                PageSize = pageSize,
                TotalCount = context.Customer.Count()
            };
        }

        public void Update(CustomerProfile customerProfile)
        {
            Customer customer = context.Customer.FirstOrDefault(c => c.Id == customerProfile.Id);
            customerProfile.MapToCustomer(customer);
            context.Customer.Update(customer);
            context.SaveChanges();
        }

        public Customer Get(string id)
        {
            return context.Customer.FirstOrDefault(c => c.Id == id);
        }

        public async Task Delete(string identityId)
        {
            var customer = context.Customer.FirstOrDefault(c => c.IdentityId == identityId);
            if (customer != null)
            {
                context.Customer.Remove(customer);
            }

            var user = await userManager.FindByIdAsync(identityId);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to delete user."
                        + result.Errors?.Aggregate("", (str, err) => str += $"{err.Code}: {err.Description}, "));
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
