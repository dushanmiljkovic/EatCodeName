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

        public bool StoreVores()
        {
            try
            {
                var scoreboardDTO = CastScoreToList(scoreboardRedisStack.GetScoreboard());
                var sotored = true; // store data into MongoDB 
                if (sotored)
                {
                    var status2 = scoreboardRedisStack.DeleteScoreboard();
                    return status2;
                }
                return sotored;
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
