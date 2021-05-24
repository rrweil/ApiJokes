using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW5._19._21ApiJokes
{
    public class Joke
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Setup { get; set; }
        public string Punchline{ get; set; }

        public List<UserLikedJokes> Likes { get; set;  }
    }
}
