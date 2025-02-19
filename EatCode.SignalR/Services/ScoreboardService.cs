﻿using Microsoft.Extensions.Options;
using Models.Domein;
using MongoDB.Stack;
using Redis.Stack;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EatCode.SignalR.Services
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

        public bool AddVote(string recipe, double score)
        {
            try
            {
                return scoreboardRedisStack.AddItemToScoreboard(recipe, score);
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

        private List<RecipeVote> CastScoreToList(IDictionary<string, double> score)
            => score.Select(x => new RecipeVote()
            {
                Name = x.Key,
                Score = x.Value
            }).ToList();
    }
}