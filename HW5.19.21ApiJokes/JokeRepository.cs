using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW5._19._21ApiJokes
{
    public class JokeRepository
    {
        private readonly String _connectionString;

        public JokeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddJoke (Joke joke)
        {
            var ctx = new JokeContext(_connectionString);
          
            ctx.Add(joke);
            ctx.SaveChanges();
        }

        public Counts GetCounts(int jokeId) {
            var ctx = new JokeContext(_connectionString);
            return new Counts()
            {
                LikeCounts = ctx.UserLikedJokes.Count(ulj => ulj.JokeId == jokeId && ulj.Liked == true),
                DislikeCounts = ctx.UserLikedJokes.Count(ulj => ulj.JokeId == jokeId && ulj.Liked == false)
            };
        }

        public List<Joke> GetAllJokes()
        {
            var ctx = new JokeContext(_connectionString);
            return ctx.Jokes.Include(j => j.Likes).ToList();
        }

        public bool JokeAlreadyExists (Joke joke)
        {
            var ctx = new JokeContext(_connectionString);
            return ctx.Jokes.Any(j => j.Setup == joke.Setup && j.Punchline == joke.Punchline); 
        }

        public void UpdateLikeStatus(UserLikedJokes ulj)
        {
            var ctx = new JokeContext(_connectionString);
            ulj.Timestamp = DateTime.Now;
            var liked = ulj.Liked == true ? 1 : 0;
            if (ctx.UserLikedJokes.Any(ul => ul.UserId == ulj.UserId && ul.JokeId == ulj.JokeId))
            {
                ctx.Database.ExecuteSqlInterpolated($"UPDATE UserLikedJokes SET Liked = {liked}, Timestamp = {DateTime.Now} where jokeid = {ulj.JokeId} AND userId = {ulj.UserId}");
                return;
            }
            ctx.UserLikedJokes.Add(ulj);
            ctx.SaveChanges();
        }

        public int GetJokeId(Joke joke)
        {
            var ctx = new JokeContext(_connectionString);
            return ctx.Jokes.FirstOrDefault(j => j.Setup == joke.Setup && j.Punchline == joke.Punchline).Id;
        }

        public UserLikedJokes GetUserLikedJokes (int userId, int jokeId)
        {
            var ctx = new JokeContext(_connectionString);
            return ctx.UserLikedJokes.FirstOrDefault(ulj => ulj.UserId == userId && ulj.JokeId == jokeId);
        }
    }
}
