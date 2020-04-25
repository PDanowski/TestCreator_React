﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestCreator.Data.Commands.Handlers.Common;
using TestCreator.Data.Converters.DAO.Interfaces;
using TestCreator.Data.Database;
using TestCreator.Data.Models.DAO;

namespace TestCreator.Data.Commands.Handlers
{
    public class AddUserCommandHandler : CommandHandler<AddUserCommand>
    {
        private readonly IApplicationUserConverter _converter;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddUserCommandHandler(EfDbContext context, 
            IApplicationUserConverter converter, 
            UserManager<ApplicationUser> userManager) : base(context)
        {
            _converter = converter;
            _userManager = userManager;
        }

        protected override void DoHandle(AddUserCommand request)
        {
            var user = _converter.Convert(request.User);

            user.SecurityStamp = Guid.NewGuid().ToString();
            user.CreationDate = DateTime.Now;
            user.LastModificationDate = DateTime.Now;

            var createResult = _userManager.CreateAsync(user).Result;
            var addToRoleResult = _userManager.AddToRolesAsync(user, request.Roles).Result;

            user.EmailConfirmed = true;
            user.LockoutEnabled = false;

            DbContext.SaveChanges();
        }

        protected override async Task DoHandleAsync(AddUserCommand request)
        {
            var user = _converter.Convert(request.User);

            user.SecurityStamp = Guid.NewGuid().ToString();
            user.CreationDate = DateTime.Now;
            user.LastModificationDate = DateTime.Now;

            await _userManager.CreateAsync(user);

            await _userManager.AddToRolesAsync(user, request.Roles);

            user.EmailConfirmed = true;
            user.LockoutEnabled = false;

            DbContext.SaveChanges();
        }
    }
}
