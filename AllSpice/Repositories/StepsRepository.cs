using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class StepsRepository
    {
        private readonly IDbConnection _db;
        public StepsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal List<Steps> GetStepsByRecipeId(int id)
        {
            string sql = @"
            SELECT 
            s.*,
            r.id
            FROM steps s
            JOIN recipes r ON r.id = s.recipeId
            WHERE s.recipeId =@id;
            ";
            List<Steps> steps = _db.Query<Steps, Recipe, Steps>(sql, (steps, recipe) =>
            {
                steps.RecipeId = recipe.id;
                return steps;
            }, new { id }).ToList();
            return steps;
        }

        internal List<Steps> GetAll()
        {
            string sql = @"
            SELECT
            s.*,
            a.*
            FROM steps s
            JOIN accounts a ON a.id = s.creatorId
            ";
            List<Steps> steps =
            (List<Steps>)_db.Query<Steps, Account, Steps>(sql, (steps, account) =>
            {
                steps.Creator = account;
                return steps;
            }).ToList();
            return steps;
        }




    }
}