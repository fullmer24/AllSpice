using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
    public class FavoritesRepository
    {
        private readonly IDbConnection _db;

        public FavoritesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<FavoritesVM> GetFavorites(string accountId)
        {
            string sql = @"
            SELECT 
                r.*,
                f.*
            FROM favorites f
            JOIN recipes r ON r.creatorId = f.accountId
            WHERE f.accountId = @accountId;
            ";
            List<FavoritesVM> favorites = _db.Query<FavoritesVM, Favorite, FavoritesVM>(sql, (r, f) =>
            {
                r.MyFavoritesId = f.FavoriteId;
                return r;
            }, new { accountId }).ToList();
            return favorites;
        }
    }
}