using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MclApp.Core;
using MclApp.Data.DataContext;
using Microsoft.EntityFrameworkCore;

namespace MclApp.Data.Repository
{
    public  class DbRepositoryPattern<T> : IDbRepositoryPattern<T> where T :  BaseEntity
    {
        #region Fields

        private readonly MclAppDataObjectContext _context;
        private DbSet<T> _entities;

        #region Properties

        public virtual IQueryable<T> Table => this.Entities;
        public virtual IQueryable<T> TableNoTracking => this.Entities.AsNoTracking();
        protected virtual DbSet<T> Entities
        {
            get { return _entities ??= _context.Set<T>(); }
        }
        #endregion
        public DbRepositoryPattern(MclAppDataObjectContext context)
        {
            this._context = context;
        }

        protected string GetFullErrorText(Exception exc)
        {
            var msg = string.Empty;
                    msg += $" Error: {exc.InnerException}" + Environment.NewLine;
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

                this.Entities.UpdateRange(entities);
                AddUpdateEntityWithDateTime(entities);
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

        #endregion

        private void AddInsertEntityWithDateTime(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.LastModifiedDate = DateTime.Now;
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
            entity.LastModifiedDate = DateTime.Now;
        }
        private void AddUpdateEntityWithDateTime(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                AddUpdateEntityWithDateTime(entity);
            }
        }
    }
}
