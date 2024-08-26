namespace SmartTaskApp.CommonLib.Shared
{
    public static class AppConstants
    {
        public const string JwtSecret_key = "JwtConfigSecret";
        public const string AdminRole = "Admin";
        public const string UserRole = "User";
        public const string SmartTaskDbConnection_Key = "SmartTaskDbConnection";
        public static class Messages
        {
            public const string UserCreationFailed = "Error creating user";
            public const string InvalidCredentials = "Invalid email or password";
            public const string UserNotFound = "User not found";
            public const string TokenGenerationFailed = "Token generation failed";
        }

        public static class Roles
        {
            public const string Admin = "Admin";
            public const string User = "User";
        }

        public static class Validation
        {
            public const string EmailRequired = "Email is required";
            public const string PasswordRequired = "Password is required";
            public const string InvalidEmailFormat = "Invalid email format";
        }
    }

}
