using EatCode.Api.Services;
using Microsoft.Extensions.Options;
using Neo4jClient;
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
        public MatrixCRUD(IOptions<MatrixSettings> settings)
        {
            // Dont forget to start a services :) 
            this.matrixSettings = settings.Value;
        }

        public void Test()
        {
            try
            {
                var client = new GraphClient(new Uri(matrixSettings.ConnectionString), matrixSettings.Username, matrixSettings.Password);
                client.Connect();

                var movies = client.Cypher
                  .Match("(m:Movie)")
                  .Return(m => m.As<Movie>())
                  .Limit(10)
                  .Results;

                Serilog.Log.Information("RT");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
            }
        }
    }

    internal class Movie
    {
        public string title { get; set; }
        public long id { get; set; }
    }
}
