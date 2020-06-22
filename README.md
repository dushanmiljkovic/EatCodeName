# Eat Code Name
#### NO SQL DB's used: Redis, Neo4J, MongoDB

#### Testing locally
<p> For local debuging please make sure data-bases are running.
Hint, **monogDb requirs MongoDB Service** started. <br>
Make sure configurations are filled in properly(config files).<br>
Mark EatCodeName.Api, EatCodeName.SinalR and EatCodeName.HF as starting projects.

### EatCodeName.Api
Working with recipes... Basic cruds plus linking...
- FileController: uplaoding imgs (mongoDB)
- RecipeController: crud with recipes (mongoDB)
- RelatedController: linking dishes (neo4j)

### EatCodeName.SinalR
Chat app with bot and history. Bot support vote/deleteVote/store commands. Idea is allow user to chat about favorite recipes and vote for them.
Along side chat we have daily scoreboard. User can see live update of votes. (redis)

### EatCodeName.HF
Storing recipes on daily base.

## ToDo's:
4. Weekly scoreboard. Every Monday calc last  week votes and create PDF.
7. Authorization