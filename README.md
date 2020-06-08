NO SQL 

Redis
Neo4J
MongoDB

For local debuging please make sure data bases are running.
Hint, monogDb requirs MongoDB Service started.
Make sure configurations are filled in properly.
Mark EatCodeName.Api, EatCodeName.SinalR and EatCodeName.HF as starting projects. 


EatCodeName.Api
Working with recipes... Basic cruds plus linking...
- FileController: uplaoding imgs (mongoDB)
- RecipeController: crud with recipes (mongoDB)
- RelatedController: linking dishes (neo4j)

EatCodeName.SinalR
Chat app with bot. Bot support vote/deleteVote/store comands. Idea is allow user to chat about favorite recipes and vote for them. 
Along side chat we have daily scoreboard. User can see live update of recipes votes. (redis)

EatCodeName.HF
Storing recipes on daily base.


ToDo's:
1. Simple validation / Simple FE to consume Recipe API / 
2. Change Comands on redis / signalR
3. Transaction style 
4. Weekly scoreboard. Every monday calc prevouse week votes and create PDF.
5. Authorization 
6. Move hardcoded settings to config
7. Style to the chat please