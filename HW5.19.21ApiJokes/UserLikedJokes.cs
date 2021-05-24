using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HW5._19._21ApiJokes
{
    public class UserLikedJokes
    {
        public int UserId { get; set; }
        public int JokeId { get; set; }
        [JsonIgnore]
        public User User { get; set;  }
        [JsonIgnore]
        public Joke Joke { get; set;  }
        public DateTime Timestamp { get; set; }
        public Boolean Liked { get; set; }
    }
}
