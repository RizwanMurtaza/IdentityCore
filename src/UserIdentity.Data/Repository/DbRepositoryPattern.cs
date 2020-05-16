using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Core.Domain;

namespace UserIdentity.Data.Repository
{
    public partial class DbRepositoryPattern<T> : IDbRepositoryPattern<T> where T : class, IAuditableEntity
    {
        #region Fields

        private readonly QuestOrAssessIdentityDbContext _context;
        private DbSet<T> _entities;

        #region Properties

        public virtual IQueryable<T> Table => this.Entities;
        public virtual IQueryable<T> TableNoTracking => this.Entities.AsNoTracking();
        protected virtual DbSet<T> Entities
        {
            get { return _entities ??= _context.Set<T>(); }
        }
        #endregion
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
        public virtual async Task<T> GetById(object id)
        {
            return await this.Entities.FindAsync(id);
        }
        public virtual async Task<OutResult> Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    return OutResult.Error_TryingToInsertNull();
                }

                AddInsertEntityWithDateTime(entity);
                await this.Entities.AddAsync(entity);
                await this._context.SaveChangesAsync();
            }
            catch (Exception dbEx)
            {
                var error = $"Failed while inserting " + typeof(T).FullName + "::  " + GetFullErrorText(dbEx);
                return new OutResult(error, dbEx.ToString(), false);
            }
            return OutResult.Success_Created();
        }
        public virtual async Task<OutResult> Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    return OutResult.Error_TryingToInsertNull();

                AddInsertEntityWithDateTime(entities);
                foreach (var entity in entities)
                    await this.Entities.AddAsync(entity);

                await this._context.SaveChangesAsync();
            }
            catch (Exception dbEx)
            {
                var error = $"Failed while inserting " + typeof(T).FullName + "::  " + GetFullErrorText(dbEx);
                return new OutResult(error, dbEx.ToString(), false);
            }
            return OutResult.Success_Created();
        }
        public virtual async Task<OutResult> Update(T entity)
        {
            try
            {
                if (entity == null)
                    return OutResult.Error_TryingToUpdateNull();
                AddUpdateEntityWithDateTime(entity);
                this.Entities.Update(entity);
                await this._context.SaveChangesAsync();
            }
            catch (Exception dbEx)
            {
                var error = $"Failed while updating  " + typeof(T).FullName + "::  " + GetFullErrorText(dbEx);
                return new OutResult(error, dbEx.ToString(), false);
            }

            return OutResult.Success_Updated();
        }
        public virtual async Task<OutResult> Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    return OutResult.Error_TryingToUpdateNull();

                AddUpdateEntityWithDateTime(entities);
                this.Entities.UpdateRange(entities);
                await this._context.SaveChangesAsync();
            }
            catch (Exception dbEx)
            {
                var error = $"Failed while updating  " + typeof(T).FullName + "::  " + GetFullErrorText(dbEx);
                return new OutResult(error, dbEx.ToString(), false);
            }

            return OutResult.Success_Updated();
        }
        public virtual async Task<OutResult> Delete(T entity)
        {
            try
            {
                if (entity == null)
                    return OutResult.Error_TryingToDeleteNull();

                this.Entities.Remove(entity);

                await this._context.SaveChangesAsync();
            }
            catch (Exception dbEx)
            {
                var error = $"Failed while Deleting  " + typeof(T).FullName + "::  " + GetFullErrorText(dbEx);
                return new OutResult(error, dbEx.ToString(), false);
            }

            return OutResult.Success_Deleted();
        }
        public virtual async Task<OutResult> Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    return OutResult.Error_TryingToDeleteNull();

                foreach (var entity in entities)
                    this.Entities.Remove(entity);

                await this._context.SaveChangesAsync();
            }
            catch (Exception dbEx)
            {
                var error = $"Failed while Deleting  " + typeof(T).FullName + "::  " + GetFullErrorText(dbEx);
                return new OutResult(error, dbEx.ToString(), false);
            }

            return OutResult.Success_Deleted();
        }

        private void AddInsertEntityWithDateTime(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
        }
        private void AddInsertEntityWithDateTime(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                AddInsertEntityWithDateTime(entity);
            }
        }
        private void AddUpdateEntityWithDateTime(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
        }
        private void AddUpdateEntityWithDateTime(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                AddUpdateEntityWithDateTime(entity);
            }
        }



        #endregion


    }
}
