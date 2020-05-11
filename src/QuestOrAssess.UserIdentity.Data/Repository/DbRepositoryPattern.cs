using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuestOrAssess.UserIdentity.Core.Domain;

namespace QuestOrAssess.UserIdentity.Data.Repository
{
    public partial class DbRepositoryPattern<T> : IDbRepositoryPattern<T> where T : class, IAuditableEntity
    {
        #region Fields

        private readonly QuestOrAssessIdentityDbContext _context;
        private DbSet<T> _entities;

        public DbRepositoryPattern(QuestOrAssessIdentityDbContext context)
        {
            this._context = context;
        }

        protected string GetFullErrorText(Exception exc)
        {
            var msg = string.Empty;
            foreach (var validationErrors in exc.Data)
                    msg += string.Format("Property: {0} Error: {1}", exc.InnerException) + Environment.NewLine;
            return msg;
        }

        public virtual T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public virtual OutResult Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    return OutResult.Error_TryingToInsertNull();
                }

                this.Entities.Add(entity);
                this._context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                var error = $"Failed while inserting " + typeof(T).FullName + "::  " + GetFullErrorText(dbEx);
                return new OutResult(error, dbEx.ToString(), false);
            }
            return OutResult.Success_Created();
        }

        public virtual OutResult Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    return OutResult.Error_TryingToInsertNull();

                foreach (var entity in entities)
                    this.Entities.Add(entity);

                this._context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                var error = $"Failed while inserting " + typeof(T).FullName + "::  " + GetFullErrorText(dbEx);
                return new OutResult(error, dbEx.ToString(), false);
            }
            return OutResult.Success_Created();
        }

        public virtual OutResult Update(T entity)
        {
            try
            {
                if (entity == null)
                    return OutResult.Error_TryingToUpdateNull();
                this.Entities.Update(entity);
                this._context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                var error = $"Failed while updating  " + typeof(T).FullName + "::  " + GetFullErrorText(dbEx);
                return new OutResult(error, dbEx.ToString(), false);
            }

            return OutResult.Success_Updated();
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual OutResult Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    return OutResult.Error_TryingToUpdateNull();
                this._context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                var error = $"Failed while updating  " + typeof(T).FullName + "::  " + GetFullErrorText(dbEx);
                return new OutResult(error, dbEx.ToString(), false);
            }

            return OutResult.Success_Updated();
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual OutResult Delete(T entity)
        {
            try
            {
                if (entity == null)
                    return OutResult.Error_TryingToDeleteNull();

                this.Entities.Remove(entity);

                this._context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                var error = $"Failed while Deleting  " + typeof(T).FullName + "::  " + GetFullErrorText(dbEx);
                return new OutResult(error, dbEx.ToString(), false);
            }

            return OutResult.Success_Deleted();
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual OutResult Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    return OutResult.Error_TryingToDeleteNull();

                foreach (var entity in entities)
                    this.Entities.Remove(entity);

                this._context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                var error = $"Failed while Deleting  " + typeof(T).FullName + "::  " + GetFullErrorText(dbEx);
                return new OutResult(error, dbEx.ToString(), false);
            }

            return OutResult.Success_Deleted();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        #endregion
    }
}
