using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class UploadService : IUploadService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public UploadService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public List<UploadsViewModel> Read(int schoolId)
        {
            var data = (from d in entities.UPLOADS
                        where d.SCHOOL_ID == schoolId
                        orderby d.SCHOOLYEAR_ID descending, d.UPLOAD_DATE descending
                        select new UploadsViewModel
                        {
                            UPLOAD_ID = d.UPLOAD_ID,
                            SCHOOLYEAR_ID = d.SCHOOLYEAR_ID,
                            SCHOOL_ID = d.SCHOOL_ID,
                            UPLOAD_DATE = d.UPLOAD_DATE,
                            UPLOAD_NAME = d.UPLOAD_NAME,
                            UPLOAD_SUMMARY = d.UPLOAD_SUMMARY
                        }).ToList();
            return data;
        }

        public void Create(UploadsViewModel data, int schoolId)
        {
            UPLOADS entity = new UPLOADS()
            {
                SCHOOL_ID = schoolId,
                SCHOOLYEAR_ID = data.SCHOOLYEAR_ID,
                UPLOAD_DATE = data.UPLOAD_DATE,
                UPLOAD_NAME = data.UPLOAD_NAME,
                UPLOAD_SUMMARY = data.UPLOAD_SUMMARY
            };
            entities.UPLOADS.Add(entity);
            entities.SaveChanges();

            data.UPLOAD_ID = entity.UPLOAD_ID;
        }

        public void Update(UploadsViewModel data, int schoolId)
        {
            UPLOADS entity = entities.UPLOADS.Find(data.UPLOAD_ID);

            entity.SCHOOL_ID = schoolId;
            entity.SCHOOLYEAR_ID = data.SCHOOLYEAR_ID;
            entity.UPLOAD_DATE = data.UPLOAD_DATE;
            entity.UPLOAD_NAME = data.UPLOAD_NAME;
            entity.UPLOAD_SUMMARY = data.UPLOAD_SUMMARY;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(UploadsViewModel data)
        {
            UPLOADS entity = entities.UPLOADS.Find(data.UPLOAD_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.UPLOADS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public UploadsViewModel Refresh(int entityId)
        {
            return entities.UPLOADS.Select(d => new UploadsViewModel
            {
                UPLOAD_ID = d.UPLOAD_ID,
                SCHOOLYEAR_ID = d.SCHOOLYEAR_ID,
                SCHOOL_ID = d.SCHOOL_ID,
                UPLOAD_DATE = d.UPLOAD_DATE,
                UPLOAD_NAME = d.UPLOAD_NAME,
                UPLOAD_SUMMARY = d.UPLOAD_SUMMARY
            }).Where(d => d.UPLOAD_ID.Equals(entityId)).FirstOrDefault();
        }

        public List<UploadsFilesViewModel> GetFiles(int uploadId)
        {
            var data = (from d in entities.UPLOADS_FILES
                        where d.UPLOAD_ID == uploadId
                        orderby d.SCHOOL_USER, d.SCHOOLYEAR_TEXT, d.FILENAME
                        select new UploadsFilesViewModel
                        {
                            ID = d.ID,
                            UPLOAD_ID = d.UPLOAD_ID,
                            SCHOOL_USER = d.SCHOOL_USER,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            FILENAME = d.FILENAME,
                            EXTENSION = d.EXTENSION
                        }).ToList();
            return data;
        }

        public void DeleteFile(UploadsFilesViewModel data)
        {
            UPLOADS_FILES entity = entities.UPLOADS_FILES.Find(data.ID);
            if (entity != null)
            {

                entities.Entry(entity).State = EntityState.Deleted;
                entities.UPLOADS_FILES.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}