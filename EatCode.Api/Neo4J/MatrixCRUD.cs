using AutoMapper;
using EatCode.Api.Services;
using Microsoft.Extensions.Options;
using Models.Domein;
using Models.DTO;
using Neo4jClient;
using Neo4jClient.Cypher;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatCode.Api.Neo4J
{
    public class MatrixCRUD : IMatrixService
    {
        private MatrixSettings matrixSettings;
        private readonly IMapper mapper;
        private string GenerateStringId() => Guid.NewGuid().ToString();
        private GraphClient GetGraphClient() => new GraphClient(new Uri(matrixSettings.ConnectionString), matrixSettings.Username, matrixSettings.Password);

        public MatrixCRUD(IOptions<MatrixSettings> settings, IMapper mapper)
        {
            // Dont forget to start a services :) 
            this.matrixSettings = settings.Value;
            this.mapper = mapper;
        }

        public string CreateDishe(DisheDTO model)
        {
            try
            {
                var client = GetGraphClient();

                var disheDB = mapper.Map<Dishe>(model);
                disheDB.Id = GenerateStringId();

                client.Connect();
                client.Cypher
                    .Create("(d:Dishe {newDishe})")
                    .WithParam("newDishe", disheDB)
                    .ExecuteWithoutResults();

                return disheDB.Id;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                return null;
            }
        }
        public Dishe GetSpecificDish(string id)
        {
            try
            {
                var client = GetGraphClient();
                client.Connect();
                return client.Cypher
                      .Match("(dishe:Dishe)")
                      .Where((Dishe dishe) => dishe.Id == id)
                      .Return(dishe => dishe.As<Dishe>())
                      .Results.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string CreateDrink(DrinkDTO model)
        {
            try
            {
                var client = GetGraphClient();
                client.Connect();

                var drinkDB = mapper.Map<Drink>(model);
                drinkDB.Id = GenerateStringId();

                client.Connect();
                client.Cypher
                    .Create("(d:Drink {newDrink})")
                    .WithParam("newDrink", drinkDB)
                    .ExecuteWithoutResults();

                return drinkDB.Id;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                return null;
            }
        }
       
        public bool UpdateDrink(DrinkDTO model)
        {
            try
            {
                var client = GetGraphClient();
                client.Connect();

                client.Cypher
                         .Match("(drink:Drink)")
                            .Where((Drink drink) => drink.Id == model.Id)
                               .Set("drink = {updated}")
                                 .WithParam("updated", new Drink { Name = model.Name, AlcoholLevel = model.AlcoholLevel, FileId = model.FileId, Type = model.Type })
                                    .ExecuteWithoutResults();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        //Delete all inbound rel's
        public bool DeleteDrink(string id)
        {
            try
            {
                var client = GetGraphClient();
                client.Connect();

                client.Cypher
                      .OptionalMatch("(drink:Drink)<-[r]-()")
                      .Where((Drink drink) => drink.Id == id)
                      .Delete("r, drink")
                      .ExecuteWithoutResults();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public Drink GetSpecificDrink(string id)
        {
            try
            {
                var client = GetGraphClient();
                client.Connect();
                return client.Cypher
                      .Match("(drink:Drink)")
                      .Where((Drink drink) => drink.Id == id)
                      .Return(drink => drink.As<Drink>())
                      .Results.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool RelateDisheDrink(string disheId, string drinkId, DisheDrink relation)
        {
            try
            {
                var client = GetGraphClient();
                client.Connect();

                client.Cypher
                       .Match("(dishe:Dishe)", "(drink:Drink)")
                       .Where((Dishe dishe) => dishe.Id == disheId)
                       .AndWhere((Drink drink) => drink.Id == drinkId)
                       .Create($"(drink)-[:{relation}]->(dishe)")
                       .ExecuteWithoutResults();


                return true;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                return false;
            }
        }
       
        public (Dishe, long) GetSpecificDishWithGoesWithCount(string id)
        {
            try
            {
                var client = GetGraphClient();
                client.Connect();

                return client.Cypher
                          .OptionalMatch("(dishe:Dishe)-[GoesWith]-(drink:Drink)")
                          .Where((Dishe dishe) => dishe.Id == id)
                          .Return((dishe, drink) => new
                          {
                              Dishe = dishe.As<Dishe>(),
                              Count = drink.Count()
                          }).Results.Select(o => (o.Dishe, o.Count)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return (null, 0);
            }
        }

        public (Dishe, List<Drink>) GetSpecificDishWithGoesWithDrinks(string id)
        {
            try
            {
                var client = GetGraphClient();
                client.Connect();

                return client.Cypher
                          .OptionalMatch("(dishe:Dishe)-[GoesWith]-(drink:Drink)")
                          .Where((Dishe dishe) => dishe.Id == id)
                          .Return((dishe, drink) => new
                          {
                              Dishe = dishe.As<Dishe>(),
                              Drinks = drink.CollectAs<Drink>()
                          }).Results.Select(o => (o.Dishe, o.Drinks.ToList())).FirstOrDefault();


            }
            catch (Exception ex)
            {
                return (null, null);
            }
        }
          
        #region Dont open
        public void YouHadToDoItNowDoomIsHERE(string pswrd)
        {
            // we all know it
            if (pswrd != "admin")
            { return; }
            try
            {
                var query = @"MATCH (n)
                              OPTIONAL MATCH(n)-[r] - ()
                                WITH n, r LIMIT 50000
                                DELETE n, r
                                RETURN count(n) as deletedNodesCount";

                var client = GetGraphClient();
                client.Connect();
                var newsx = new CypherQuery(query, null, CypherResultMode.Projection);

                ((IRawGraphClient)client).ExecuteCypher(newsx);

                Serilog.Log.Information("hello darkness, my old friend");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
            }
        }
        #endregion
    }
}
