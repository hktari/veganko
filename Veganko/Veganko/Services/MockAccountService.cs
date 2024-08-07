﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Veganko.Models;

//[assembly: Xamarin.Forms.Dependency(typeof(Veganko.Services.MockAccountService))]
namespace Veganko.Services
{
    class MockAccountService : IAccountService
    {
        private static int IdCounter;
        
        private User user;
        public User User => throw new NotImplementedException();

        private List<User> userDatabase = new List<User>();

        public bool CreateAccount(string username, string password, string profileImage)
        {
            // check if username exists
            if (userDatabase.Exists(u => u.Username == username))
                return false;
            var hashedPassword = Helper.CalculateBase64Sha256Hash(password);
            var curId = IdCounter.ToString();
            IdCounter++;
            var user = new User { Id = curId, Username = username, Password = hashedPassword, ProfileImage = profileImage };
            userDatabase.Add(user);
            return true;
        }

        public bool Login(string username, string password)
        {
            var user = userDatabase.Find(u => u.Username == username);
            if (user == null)
                return false;
            return Helper.CalculateBase64Sha256Hash(password) == user.Password;
        }

        public bool Logout()
        {
            user = null;
            return true;
        }
    }
}
