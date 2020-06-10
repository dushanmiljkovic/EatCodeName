using Microsoft.Extensions.Options;
using Models.Domein;
using MongoDB.Stack;
using Redis.Stack;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HF.Run.Services
{
    public class ScoreboardService : IScoreboardService
    {
        private readonly ScoreboardRepository scoreboardMongoRepository;
        private readonly ScoreboardStack scoreboardRedisStack;

        public ScoreboardService(IOptions<RedisSettings> redisSettings, IOptions<ScoreboardMongoDbSettings> mongoDbSettings)
        {
            scoreboardMongoRepository = new ScoreboardRepository(mongoDbSettings);
            scoreboardRedisStack = new ScoreboardStack(redisSettings);
        }

        public bool StoreVotes(string user)
        {
            try
            {
                var scoreboardDTO = CastScoreToList(scoreboardRedisStack.GetScoreboard());

                if (!PermaStoreVotes(user, scoreboardDTO)) { return false; }

                return scoreboardRedisStack.DeleteScoreboard();
            }
            catch (Exception ex)
            {
                var log = ex;
                return false;
            }
        }

        private bool PermaStoreVotes(string user, List<RecipeVote> recipeVotes)
        {
            try
            {
                var scoreboeadTable = new ScoreboeadTable()
                {
                    Name = "Daily Store",
                    Type = Models.Enums.ScoreboeadType.Planed,
                    StoredBy = user,
                    StoredDate = DateTime.UtcNow,
                    Votes = recipeVotes,
                    VotesCount = recipeVotes.Count
                };
                scoreboardMongoRepository.InsertRecord<ScoreboeadTable>(scoreboeadTable);

                return true;
            }
            catch (Exception ex)
            {
                var log = ex;
                return false;
            }
        }

        private List<RecipeVote> CastScoreToList(IDictionary<string, double> score)
           => score.Select(x => new RecipeVote()
           {
               Name = x.Key,
               Score = x.Value
           }).ToList();
    }
}