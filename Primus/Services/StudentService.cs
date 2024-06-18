using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class StudentService : IStudentService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public StudentService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<StudentGridViewModel> Read()
        {
            var data = (from s in entities.ΜΑΘΗΤΗΣ
                        orderby s.ΕΠΩΝΥΜΟ, s.ΟΝΟΜΑ
                        select new StudentGridViewModel
                        {
                            ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = s.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                            ΑΦΜ = s.ΑΦΜ,
                            ΕΠΩΝΥΜΟ = s.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = s.ΟΝΟΜΑ,
                            ΕΙΔΙΚΟΤΗΤΑ = s.ΕΙΔΙΚΟΤΗΤΑ,
                            ΣΧΟΛΗ = s.ΣΧΟΛΗ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = s.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).ToList();
            return (data);
        }

        public IEnumerable<StudentGridViewModel> Read(int schoolId)
        {
            var data = (from s in entities.ΜΑΘΗΤΗΣ
                        where s.ΣΧΟΛΗ == schoolId
                        orderby s.ΕΠΩΝΥΜΟ, s.ΟΝΟΜΑ
                        select new StudentGridViewModel
                        {
                            ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = s.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                            ΑΦΜ = s.ΑΦΜ,
                            ΕΠΩΝΥΜΟ = s.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = s.ΟΝΟΜΑ,
                            ΕΙΔΙΚΟΤΗΤΑ = s.ΕΙΔΙΚΟΤΗΤΑ,
                            ΣΧΟΛΗ = s.ΣΧΟΛΗ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = s.ΣΧΟΛΗ_ΤΥΠΟΣ
                        }).ToList();
            return (data);
        }

        public void Create(StudentGridViewModel data)
        {
            ΜΑΘΗΤΗΣ entity = new ΜΑΘΗΤΗΣ()
            {
                ΑΦΜ = data.ΑΦΜ.Trim(),
                ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim(),
                ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim(),
                ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ,
                ΣΧΟΛΗ = data.ΣΧΟΛΗ,
                ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType((int)data.ΣΧΟΛΗ)
            };
            entities.ΜΑΘΗΤΗΣ.Add(entity);
            entities.SaveChanges();

            data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = entity.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ;
        }

        public void Create(StudentGridViewModel data, int schoolId)
        {
            ΜΑΘΗΤΗΣ entity = new ΜΑΘΗΤΗΣ()
            {
                ΑΦΜ = data.ΑΦΜ.Trim(),
                ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim(),
                ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim(),
                ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ,
                ΣΧΟΛΗ = schoolId,
                ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType(schoolId)
            };
            entities.ΜΑΘΗΤΗΣ.Add(entity);
            entities.SaveChanges();

            data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = entity.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ;
        }

        public void Update(StudentGridViewModel data)
        {
            ΜΑΘΗΤΗΣ entity = entities.ΜΑΘΗΤΗΣ.Find(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ;
            entity.ΑΦΜ = data.ΑΦΜ;
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ;
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ;
            entity.ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ;
            entity.ΣΧΟΛΗ = data.ΣΧΟΛΗ;
            entity.ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType((int)data.ΣΧΟΛΗ);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Update(StudentGridViewModel data, int schoolId)
        {
            ΜΑΘΗΤΗΣ entity = entities.ΜΑΘΗΤΗΣ.Find(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ);

            entity.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ;
            entity.ΑΦΜ = data.ΑΦΜ;
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ;
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ;
            entity.ΕΙΔΙΚΟΤΗΤΑ = data.ΕΙΔΙΚΟΤΗΤΑ;
            entity.ΣΧΟΛΗ = schoolId;
            entity.ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType(schoolId);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(StudentGridViewModel data)
        {
            ΜΑΘΗΤΗΣ entity = entities.ΜΑΘΗΤΗΣ.Find(data.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ);
            try
            {
                if (entity != null)
                {
                    entities.Entry(entity).State = EntityState.Deleted;
                    entities.ΜΑΘΗΤΗΣ.Remove(entity);
                    entities.SaveChanges();
                }
            }
            catch { }
        }

        public StudentGridViewModel Refresh(int entityId)
        {
            return entities.ΜΑΘΗΤΗΣ.Select(d => new StudentGridViewModel
            {
                ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                ΑΦΜ = d.ΑΦΜ,
                ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ
            }).Where(d => d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == entityId).FirstOrDefault();
        }

        public void CopyStudentData(int targetStudentId, string afm)
        {
            var source = (from d in entities.ΜΑΘΗΤΗΣ where d.ΑΦΜ == afm && d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ != targetStudentId select d).FirstOrDefault();
            if (source == null) return;

            ΜΑΘΗΤΗΣ entity = entities.ΜΑΘΗΤΗΣ.Find(targetStudentId);

            entity.ΑΔΤ = source.ΑΔΤ;
            entity.ΑΜΚΑ = source.ΑΜΚΑ;
            entity.ΕΠΩΝΥΜΟ = source.ΕΠΩΝΥΜΟ.Trim();
            entity.ΟΝΟΜΑ = source.ΟΝΟΜΑ.Trim();
            entity.ΜΑΘΗΤΗΣ_ΑΜ = source.ΜΑΘΗΤΗΣ_ΑΜ;
            entity.ΠΑΤΡΩΝΥΜΟ = source.ΠΑΤΡΩΝΥΜΟ.Trim();
            entity.ΜΗΤΡΩΝΥΜΟ = source.ΜΗΤΡΩΝΥΜΟ.Trim();
            entity.ΦΥΛΟ = source.ΦΥΛΟ;
            entity.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = source.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ;
            entity.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ = source.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ;
            entity.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ = source.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ;
            entity.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ = source.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ;
            entity.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ = source.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ;
            entity.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΑ = source.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΑ;
            entity.ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ = source.ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ;
            entity.ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ = source.ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ;
            entity.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ = source.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ;
            entity.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ = source.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ;
            entity.ΤΗΛΕΦΩΝΑ = source.ΤΗΛΕΦΩΝΑ;
            entity.EMAIL = source.EMAIL;
            entity.ΠΑΡΑΤΗΡΗΣΕΙΣ = source.ΠΑΡΑΤΗΡΗΣΕΙΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public StudentViewModel GetRecord(int studentId)
        {
            var data = (from d in entities.ΜΑΘΗΤΗΣ
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ == studentId
                        select new StudentViewModel
                        {
                            ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ = d.ΜΑΘΗΤΗΣ_ΚΩΔΙΚΟΣ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΑΜΚΑ = d.ΑΜΚΑ,
                            ΑΔΤ = d.ΑΔΤ,
                            ΜΑΘΗΤΗΣ_ΑΜ = d.ΜΑΘΗΤΗΣ_ΑΜ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΕΙΔΙΚΟΤΗΤΑ = d.ΕΙΔΙΚΟΤΗΤΑ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                            ΜΗΤΡΩΝΥΜΟ = d.ΜΗΤΡΩΝΥΜΟ,
                            ΦΥΛΟ = d.ΦΥΛΟ,
                            ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = d.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            EMAIL = d.EMAIL,
                            ΔΙΑΜΟΝΗ_ΔΗΜΟΣ = d.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ,
                            ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ = d.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ,
                            ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ = d.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ,
                            ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ = d.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ,
                            ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΑ = d.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΑ,
                            ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ = d.ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ,
                            ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ = d.ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ,
                            ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ = d.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ,
                            ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ = d.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ,
                            ΠΑΡΑΤΗΡΗΣΕΙΣ = d.ΠΑΡΑΤΗΡΗΣΕΙΣ
                        }
                        ).FirstOrDefault();

            return (data);
        }

        public void UpdateRecord(StudentViewModel model, int studentId, int schoolId = 0)
        {
            ΜΑΘΗΤΗΣ entity = entities.ΜΑΘΗΤΗΣ.Find(studentId);

            entity.ΑΦΜ = model.ΑΦΜ.Trim();
            entity.ΑΔΤ = model.ΑΔΤ;
            entity.ΑΜΚΑ = model.ΑΜΚΑ;
            entity.ΣΧΟΛΗ_ΤΥΠΟΣ = schoolId > 0 ? Common.GetSchoolType(schoolId) : model.ΣΧΟΛΗ_ΤΥΠΟΣ;
            entity.ΣΧΟΛΗ = schoolId > 0 ? schoolId : model.ΣΧΟΛΗ;
            entity.ΜΑΘΗΤΗΣ_ΑΜ = model.ΜΑΘΗΤΗΣ_ΑΜ;
            entity.ΕΙΔΙΚΟΤΗΤΑ = model.ΕΙΔΙΚΟΤΗΤΑ;
            entity.ΕΠΩΝΥΜΟ = model.ΕΠΩΝΥΜΟ.Trim();
            entity.ΟΝΟΜΑ = model.ΟΝΟΜΑ.Trim();
            entity.ΠΑΤΡΩΝΥΜΟ = model.ΠΑΤΡΩΝΥΜΟ.Trim();
            entity.ΜΗΤΡΩΝΥΜΟ = model.ΜΗΤΡΩΝΥΜΟ.Trim();
            entity.ΦΥΛΟ = model.ΦΥΛΟ;
            entity.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ = model.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ;
            entity.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ = model.ΔΙΑΜΟΝΗ_ΔΗΜΟΣ;
            entity.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ = model.ΔΙΑΜΟΝΗ_ΔΙΕΥΘΥΝΣΗ;
            entity.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ = model.ΔΙΑΜΟΝΗ_ΠΕΡΙΟΧΗ;
            entity.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ = model.ΔΙΑΜΟΝΗ_ΠΕΡΙΦΕΡΕΙΑ;
            entity.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΑ = model.ΔΙΑΜΟΝΗ_ΤΗΛΕΦΩΝΑ;
            entity.ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ = model.ΚΑΤΟΙΚΙΑ_ΔΗΜΟΣ;
            entity.ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ = model.ΚΑΤΟΙΚΙΑ_ΔΙΕΥΘΥΝΣΗ;
            entity.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ = model.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΟΧΗ;
            entity.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ = model.ΚΑΤΟΙΚΙΑ_ΠΕΡΙΦΕΡΕΙΑ;
            entity.ΤΗΛΕΦΩΝΑ = model.ΤΗΛΕΦΩΝΑ;
            entity.EMAIL = model.EMAIL;
            entity.ΠΑΡΑΤΗΡΗΣΕΙΣ = model.ΠΑΡΑΤΗΡΗΣΕΙΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}