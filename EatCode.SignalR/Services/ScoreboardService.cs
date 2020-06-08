using Models.Domein;
using MongoDB.Stack;
using Redis.Stack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatCode.SignalR.Services
{
    public class ScoreboardService
    {
        readonly ScoreboardRepository scoreboardMongoRepository;
        readonly ScoreboardStack scoreboardRedisStack;

        public ScoreboardService()
        {
            scoreboardMongoRepository = new ScoreboardRepository();
            scoreboardRedisStack = new ScoreboardStack();
        }

        public bool AddVote(string recipe, string score)
        {
            try
            {
                return scoreboardRedisStack.AddItemToScoreboard(recipe, CastStringToDouble(score));
            }
            catch (Exception ex)
            {
                var log = ex;
                return false;
            }

        }
        public bool RemoveVote(string recipe)
        {
            try
            {
                return scoreboardRedisStack.DeleteItemFromScoreboard(recipe);
            }
            catch (Exception ex)
            {
                var log = ex;
                return false;
            }
        }

        public IDictionary<string, double> GetScoreboar()
        {
            try
            {
                return scoreboardRedisStack.GetScoreboard();
            }
            catch (Exception ex)
            {
                var log = ex;
                return null;
            }
        }

        public List<RecipeVote> GetScoreboarFlat()
        {
            try
            {
                return CastScoreToList(GetScoreboar());
            }
            catch (Exception ex)
            {
                var log = ex;
                return null;
            }
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

        public bool PermaStoreVotes(string user, List<RecipeVote> recipeVotes)
        {
            try
            {
                var scoreboeadTable = new ScoreboeadTable()
                {
                    Name = "Forced by: " + user,
                    Type = Models.Enums.ScoreboeadType.Forced,
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

        private double CastStringToDouble(string score)
        {
            try
            {
                return Convert.ToDouble(score);
            }
            catch
            {
                return 0;
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
