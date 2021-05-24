using HW5._19._21ApiJokes.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HW5._19._21ApiJokes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokeController : ControllerBase
    {
        private string _connectionString;

        public JokeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("GetJoke")]
        public Joke GetJoke()
        {
            var client = new HttpClient();
            var json = client.GetStringAsync($"https://official-joke-api.appspot.com/jokes/programming/random").Result;
            Joke joke = JsonConvert.DeserializeObject<List<Joke>>(json).First();
            var repo = new JokeRepository(_connectionString);
            if (!repo.JokeAlreadyExists(joke))
            {
                repo.AddJoke(joke);
            }
            else
            {
                joke.Id = repo.GetJokeId(joke);
            }
            return joke;
        }

        [HttpGet]
        [Route("GetCounts")]
        public Counts GetCounts(int jokeId)
        {
            var repo = new JokeRepository(_connectionString);
            return repo.GetCounts(jokeId);
        }

        [HttpGet]
        [Route("GetAllJokes")]
        public List<Joke> GetAllJokes()
        {
            var repo = new JokeRepository(_connectionString);
            return repo.GetAllJokes();
        }

        [Authorize]
        [HttpPost]
        [Route("LikeJoke")]
        public void LikeJoke(UserLikedJokes ulj)
        {
            
            var repo = new JokeRepository(_connectionString);
            var userRepo = new UserRepository(_connectionString);
            ulj.UserId = userRepo.GetByEmail(User.Identity.Name).Id;
            repo.UpdateLikeStatus(ulj);
        }

        [Authorize]
        [HttpGet]
        [Route("GetLikeButtonStatus")]
        public string GetLikeButtonStatus(int jokeId)
        {
            var jokeRepo = new JokeRepository(_connectionString);

            var userRepo = new UserRepository(_connectionString);
            var userId = userRepo.GetByEmail(User.Identity.Name).Id;

            var userLikedJokesForUser = jokeRepo.GetUserLikedJokes(userId, jokeId);
            var elapsedTime = (int)DateTime.Now.Subtract(userLikedJokesForUser.Timestamp).TotalMinutes;


            if (userLikedJokesForUser == null)
            {
                return "Didn't Interact";
            } else if (elapsedTime < 5)
            {
                return userLikedJokesForUser.Liked == true ? "Liked" : "Disliked";
            } else
            {
                return "TimeElapsedButtonsLocked";
            }

        }
    }
}

