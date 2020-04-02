﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veganko.Common.Models.Users;
using VegankoService.Data;
using VegankoService.Data.Users;
using VegankoService.Helpers;
using VegankoService.Models;
using VegankoService.Models.User;

namespace VegankoService.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<AccountController> logger;
        private readonly VegankoContext context;
        private readonly IConfiguration configuration;
        private readonly IUsersRepository usersRepository;

        public UsersController(
            IConfiguration configuration,
            IUsersRepository usersRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor,
            ILogger<AccountController> logger,
            VegankoContext context)
        {
            this.configuration = configuration;
            this.usersRepository = usersRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
            this.context = context;
        }

        [HttpPut("{id}")]
        public ActionResult<CustomerProfile> Edit(string id, [FromBody]CustomerProfile input)
        {
            Customer customer = usersRepository.Get(id);

            // Only logged in user can edit the profile.
            string userIdentityId = Identity.GetUserIdentityId(httpContextAccessor.HttpContext.User);
            if (customer.IdentityId != userIdentityId)
            {
                return Forbid();
            }

            usersRepository.Update(input);

            return input;
        }

        [HttpGet("{id}")]
        public ActionResult<PagedList<CustomerProfile>> Get(string id)
        {
            CustomerProfile customer = usersRepository.GetProfile(id);
            if (customer == null)
            {
                logger.LogWarning($"Customer by id: {id} not found.");
                return NotFound();
            }

            return Ok(customer);
        }

        [Authorize(Roles = Constants.Strings.Roles.Admin + ", " + Constants.Strings.Roles.Manager)]
        [HttpGet]
        public ActionResult<PagedList<CustomerProfile>> GetAll([FromQuery]UserQuery query)
        {
            return usersRepository.GetAll(query);
        }
    }
}
