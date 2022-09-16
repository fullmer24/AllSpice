using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class RecipesRepository
    {
        private readonly IDbConnection _db;
        public RecipesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Recipe> GetAll()
        {
            string sql = @"
           SELECT
           r.*,
           a.*
           FROM recipes r
           JOIN accounts a ON a.id = r.creatorId;
           ";
            List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
            {
                recipe.Creator = account;
                return recipe;
            }).ToList();
            return recipes;
        }

        internal Recipe Create(Recipe newRecipe)
        {
            string sql = @"
         INSERT INTO recipes
         (picture, title, subtitle, category, creatorId)
         VALUES
         (@picture, @title, @subtitle, @category, @creatorId);
         SELECT LAST_INSERT_ID();
         ";
            int id = _db.ExecuteScalar<int>(sql, newRecipe);
            newRecipe.id = id;
            return newRecipe;
        }

        internal Recipe GetById(int id)
        {
            string sql = @"
            SELECT * FROM recipes
            WHERE id = @id;
            ";
            Recipe recipe = _db.Query<Recipe>(sql, new { id }).FirstOrDefault();
            return recipe;
        }


    }
}