﻿namespace UserTodoDotNetWebAPI.Services.Interface
{
    public interface IPasswordService
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);
    }
}
