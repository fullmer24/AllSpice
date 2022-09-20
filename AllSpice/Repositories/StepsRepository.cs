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
        internal Steps Create(Steps newStep)
        {
            string sql = @"
            INSERT INTO steps
            (position, body, recipeId, CreatorId)
            VALUES
            (@position, @body, @recipeId, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newStep);
            newStep.Id = id;
            return newStep;
        }
        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM steps WHERE id = @id
            ";
            _db.Execute(sql, new { id });
        }
        internal Steps GetById(int id)
        {
            string sql = @"
            SELECT * FROM steps
            WHERE id = @id
            ";
            Steps steps = _db.Query<Steps>(sql, new { id }).FirstOrDefault();
            return steps;
        }
        internal Steps Update(Steps stepData)
        {
            string sql = @"
            UPDATE steps SET
            position = @position,
            body = @body
            WHERE id = @id
            ";
            _db.Execute(sql, stepData);
            return stepData;
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