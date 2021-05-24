using System;
using System.Collections.Generic;

namespace HW5._19._21ApiJokes
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public List<UserLikedJokes> LikedJokes { get; set; }
    }
}
