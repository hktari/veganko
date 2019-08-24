﻿
namespace VegankoService.Helpers
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }

            public static class Roles
            {
                public const string Admin = "Admin";
                public const string Manager = "Manager";
                public const string Member = "Member";
            }
        }
    }
}
