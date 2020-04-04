﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veganko.Common.Models.Users;
using Veganko.Models;
using Veganko.Models.User;
using Veganko.Other;

namespace Veganko.Services
{
    class MockAccountService : IAccountService
    {
        private static int IdCounter;
        public UserPublicInfo User { get; set; }

        private List<UserPublicInfo> userDatabase = new List<UserPublicInfo>();
        private Dictionary<string, string> userPasswords = new Dictionary<string, string>();
        
        private Exception nextRequestException;

        public void SetError(Exception ex)
        {
            nextRequestException = ex;
        }

        public Task CreateAccount(UserPublicInfo user, string password)
        {
            if(nextRequestException != null)
            {
                throw nextRequestException;
            }

            // check if username exists
            if (userDatabase.Exists(u => u.Username == user.Username))
                throw new Exception("Exists !");

            var curId = IdCounter.ToString();
            IdCounter++;
            
            userDatabase.Add(user);
            userPasswords.Add(user.Username, password);

            return Task.CompletedTask;
        }

        public Task Login(string username, string password)
        {
            var user = userDatabase.Find(u => u.Username == username);
            if (user == null || password != userPasswords[username])
            {
                throw new Exception("Invalid credentials.");
            }

            User = user;

            return Task.CompletedTask;
        }

        public void Logout()
        {
            User = null;
        }

        public Task<bool> LoginWithFacebook()
        {
            throw new NotImplementedException();
        }

        public Task ForgotPassword(string email)
        {
            return Task.CompletedTask;
        }

        public Task ResetPassword(string email, string token, string newPassword)
        {
            return Task.CompletedTask;
        }

        public Task<string> ValidateOTP(string email, int otp)
        {
            return Task.FromResult("pwd-reset-token");
        }

        public Task ResendConfirmationEmail(string email)
        {
            return Task.CompletedTask;
        }
    }
}
