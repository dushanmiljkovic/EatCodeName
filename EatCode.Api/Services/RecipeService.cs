using AutoMapper;
using Microsoft.Extensions.Options;
using Models.Domein;
using Models.DTO;
using MongoDB.Stack;
using Settings;
using System;
using System.Collections.Generic;

namespace EatCode.Api.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMapper mapper;
        private readonly RecipeRepository repository;

        public RecipeService(IMapper mapper,IOptions<RecipesMongoDbSettings> settings)
        {
            repository = new RecipeRepository(settings);
            this.mapper = mapper;
        }

        public bool CreateRecipe(RecipeDTO model)
        {
            try
            {
                var modelDB = mapper.Map<Recipe>(model);
                repository.InsertRecord(modelDB);
                return true;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                return false;
            }
        }

        public bool DeleteRecipe(Guid id)
        {
            try
            {
                repository.DeleteRecord<Recipe>(id);
                return true;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                return false;
            }
        }

        public RecipeDTO GetRecipe(Guid id)
        {
            try
            {
                var result = repository.LoadRecordById<Recipe>(id);
                var model = mapper.Map<RecipeDTO>(result);
                return model;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                return null;
            }
        }

        public List<RecipeDTO> GetRecipes()
        {
            try
            {
                var db = repository.LoadRecords<Recipe>();
                var result = mapper.Map<List<RecipeDTO>>(db);
                return result;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                return null;
            }
        }

        public bool UpdateRecipe(RecipeDTO model)
        {
            try
            {
                var mapped = mapper.Map<Recipe>(model);
                repository.UpsertRecord<Recipe>(mapped.Id, mapped);
                return true;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                return false;
            }
        }
    }
}