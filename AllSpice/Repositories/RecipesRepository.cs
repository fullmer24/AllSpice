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
           p.*,
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
    }
}