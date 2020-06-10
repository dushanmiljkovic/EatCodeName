NO SQL:
Redis
Neo4J
MongoDB

For local debuging please make sure data-bases are running.
Hint, monogDb requirs MongoDB Service started.
Make sure configurations are filled in properly(config files).
Mark EatCodeName.Api, EatCodeName.SinalR and EatCodeName.HF as starting projects.

EatCodeName.Api
Working with recipes... Basic cruds plus linking...
- FileController: uplaoding imgs (mongoDB)
- RecipeController: crud with recipes (mongoDB)
- RelatedController: linking dishes (neo4j)

EatCodeName.SinalR
Chat app with bot. Bot support vote/deleteVote/store commands. Idea is allow user to chat about favorite recipes and vote for them.
Along side chat we have daily scoreboard. User can see live update of recipes votes. (redis)

EatCodeName.HF
Storing recipes on daily base.


ToDo's:
1. Simple Consume API FE
2. Change commands on redis / signalR
3. Transaction style
4. Weekly scoreboard. Every Monday calc last  week votes and create PDF.
5. Authorization

Matrix Service Functionality:
1. string CreateDishe(DisheDTO model)
2. Dishe GetSpecificDish(string id)
3. string CreateDrink(DrinkDTO model)
4. bool UpdateDrink(DrinkDTO model)
5. bool DeleteDrink(string id) //Delete all inbound rel's
6. Drink GetSpecificDrink(string id)
7. bool RelateDisheDrink(string disheId, string drinkId, DisheDrink relation)
8. (Dishe, long) GetSpecificDishWithGoesWithCount(string id)
9. (Dishe, List<Drink>) GetSpecificDishWithGoesWithDrinks(string id)

