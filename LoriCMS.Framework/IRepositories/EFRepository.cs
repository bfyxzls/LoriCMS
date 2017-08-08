using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace LoriCMS.IRepositories
{
    /// <summary>
    /// 使用EF进行持久化
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EFRepository<TEntity> : IRepository<TEntity>
      where TEntity : class
     {
        #region Constructors
        /// <summary>
        /// 这个在IoC注入时走它
        /// </summary>
        public EFRepository() : this(null)
        {

        }

    
        public EFRepository(DbContext db)
        {
            Db = db;
         }
        #endregion

        #region Fields
         
        /// <summary>
        /// EF数据上下文
        /// </summary>
        private DbContext Db;
        #endregion

        /// <summary>
        /// 提交到数据库
        /// 有异常必须throw，否则会影响分布式事务的回滚失效
        /// </summary>
        protected virtual void SaveChanges()
        {
            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                 throw new DbUpdateConcurrencyException("Lind.DDD框架在更新时引起了乐观并发，后修改的数据不会被保存");
            }
            catch (DbEntityValidationException ex)
            {
                List<string> errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    string entityName = validationResult.Entry.Entity.GetType().Name;
                    foreach (DbValidationError error in validationResult.ValidationErrors)
                    {
                        errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                    }
                }
                
                throw;
            }
            catch (Exception ex)
            {
               
                throw;
            }

        }

        #region IRepository<TEntity> 成员

        public void Delete(TEntity item)
        {
            if (item != null)
            {

                //物理删除
                Db.Set<TEntity>().Attach(item as TEntity);
                Db.Entry(item).State = EntityState.Deleted;
                Db.Set<TEntity>().Remove(item as TEntity);
                this.SaveChanges();
            }
        }
        public TEntity Find(params object[] id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetModel()
        {
            return Db.Set<TEntity>();
        }
        public void Insert(TEntity item)
        {
            if (item != null)
            {
                Db.Entry<TEntity>(item as TEntity);
                Db.Set<TEntity>().Add(item as TEntity);
                this.SaveChanges();
            }

        }
        public void Update(TEntity item)
        {
            if (item != null)
            {
                Db.Set<TEntity>().Attach(item);
                Db.Entry(item).State = EntityState.Modified;
                this.SaveChanges();
            }
        }
        public void Insert(IEnumerable<TEntity> item)
        {
            foreach (var entity in item)
            {
                Db.Entry<TEntity>(entity as TEntity);
                Db.Set<TEntity>().Add(entity as TEntity);
            }
            this.SaveChanges();
        }


        public void Update(IEnumerable<TEntity> item)
        {
            #region 1个SQL连接,发N条语句，事务级
            foreach (var entity in item)
            {
                Db.Set<TEntity>().Attach(entity as TEntity);
                Db.Entry(entity).State = EntityState.Modified;
            }
            this.SaveChanges();
            #endregion
        }
        public void Delete(IEnumerable<TEntity> item)
        {
            foreach (var entity in item)
            {
                Db.Set<TEntity>().Attach(entity as TEntity);
                Db.Set<TEntity>().Remove(entity as TEntity);
                this.SaveChanges();
            }
        }
        public void SetDataContext(object db)
        {
            try
            {
                Db = (DbContext)db;
            }
            catch (Exception)
            {
                throw new ArgumentException("EF.SetDataContext要求上下文为DbContext类型");
            }

        }

        #endregion


    }
}
