using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class StepsRepository
    {
        private readonly StepsRepository
        public StepsRepository(IDbConnection _db)
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
                steps.recipeId = recipe.id;
                return steps;
            }, new { id }).ToList();
            return steps;
        }
    }
}