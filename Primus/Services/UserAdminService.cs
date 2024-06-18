using System;
using System.Collections.Generic;
using System.Linq;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class UserAdminService : IUserAdminService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public UserAdminService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<UserAdminViewModel> Read()
        {
            var admins = (from d in entities.USER_ADMINS
                          orderby d.FULLNAME
                          select new UserAdminViewModel
                          {
                              USER_ID = d.USER_ID,
                              USERNAME = d.USERNAME,
                              PASSWORD = d.PASSWORD,
                              ADMIN_LEVEL = d.ADMIN_LEVEL ?? 2,
                              FULLNAME = d.FULLNAME,
                              CREATEDATE = d.CREATEDATE,
                              ISACTIVE = d.ISACTIVE ?? false
                          }).ToList();
            return admins;
        }

        public void Create(UserAdminViewModel data)
        {
            USER_ADMINS entity = new USER_ADMINS()
            {
                USERNAME = data.USERNAME,
                PASSWORD = data.PASSWORD,
                FULLNAME = data.FULLNAME,
                ADMIN_LEVEL = data.ADMIN_LEVEL,
                ISACTIVE = data.ISACTIVE,
                CREATEDATE = data.CREATEDATE
            };
            entities.USER_ADMINS.Add(entity);
            entities.SaveChanges();

            data.USER_ID = entity.USER_ID;
        }

        public void Update(UserAdminViewModel data)
        {
            USER_ADMINS entity = entities.USER_ADMINS.Find(data.USER_ID);

            entity.USERNAME = data.USERNAME;
            entity.PASSWORD = data.PASSWORD;
            entity.FULLNAME = data.FULLNAME;
            entity.ADMIN_LEVEL = data.ADMIN_LEVEL;
            entity.ISACTIVE = data.ISACTIVE;
            entity.CREATEDATE = data.CREATEDATE;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(UserAdminViewModel data)
        {
            USER_ADMINS entity = entities.USER_ADMINS.Find(data.USER_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.USER_ADMINS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}