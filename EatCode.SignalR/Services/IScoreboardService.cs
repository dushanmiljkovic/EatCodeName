using Models.Domein;
using System.Collections.Generic;

namespace EatCode.SignalR.Services
{
    public interface IScoreboardService
    {
        bool AddVote(string recipe, double score);
        bool RemoveVote(string recipe);
        IDictionary<string, double> GetScoreboar();
        List<RecipeVote> GetScoreboarFlat();
        bool StoreVotes(string user);
        bool PermaStoreVotes(string user, List<RecipeVote> recipeVotes);
    }
}