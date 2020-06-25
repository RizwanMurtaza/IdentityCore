using System.Linq;
using MclApp.Core;
using Microsoft.EntityFrameworkCore;

namespace MclApp.Data.DataContext
{
    public partial interface IMclAppDataObjectContext
    {
        #region Methods

        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
        string GenerateCreateScript();
        IQueryable<TQuery> QueryFromSql<TQuery>(string sql, params object[] parameters) where TQuery : class;
        IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity;
        int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null,
            params object[] parameters);
        void Detach<TEntity>(TEntity entity) where TEntity : BaseEntity;
        #endregion
    }
}
