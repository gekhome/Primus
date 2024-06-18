using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class EpidomaSitisi2Service : IEpidomaSitisi2Service, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public EpidomaSitisi2Service(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<EpidomaSitisi2ViewModel> Read(EpidomaParameters2 ep)
        {
            List<EpidomaSitisi2ViewModel> data = new List<EpidomaSitisi2ViewModel>();

            if (ep.apofasiId > 0)
            {
                data = (from d in entities.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ep.apofasiId
                        orderby d.ΜΑΘΗΤΗΣ_ΕΠΩΝΥΜΟ, d.ΜΑΘΗΤΗΣ_ΟΝΟΜΑ, d.ΜΑΘΗΤΗΣ_ΤΑΞΗ
                        select new EpidomaSitisi2ViewModel
                        {
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            AITISI_ID = d.AITISI_ID,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ = d.ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ,
                            ΜΑΘΗΤΗΣ_ΕΠΩΝΥΜΟ = d.ΜΑΘΗΤΗΣ_ΕΠΩΝΥΜΟ,
                            ΜΑΘΗΤΗΣ_ΟΝΟΜΑ = d.ΜΑΘΗΤΗΣ_ΟΝΟΜΑ,
                            ΜΑΘΗΤΗΣ_ΤΑΞΗ = d.ΜΑΘΗΤΗΣ_ΤΑΞΗ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΣΥΝΕΧΙΣΗ = d.ΣΥΝΕΧΙΣΗ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΜΗΝΑΣ = d.ΜΗΝΑΣ
                        }).ToList();
            }
            return data;
        }

        public void Create(EpidomaSitisi2ViewModel data, EpidomaParameters2 ep, Aitisi2ViewModel aitisi)
        {
            var apofasi = (from d in entities.ΑΠΟΦΑΣΗ_ΣΙΤΙΣΗ2 where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ep.apofasiId select d).FirstOrDefault();

            ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2 entity = new ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2()
            {
                ΑΠΟΦΑΣΗ_ΚΩΔ = apofasi.ΑΠΟΦΑΣΗ_ΚΩΔ,
                ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΙΤΙΣΗ-ΣΧΟΛΕΙΟ",
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΣΧΟΛΕΙΟ = ep.schoolId,
                ΣΥΝΕΧΙΣΗ = ep.synexeia,
                AITISI_ID = aitisi.ΑΙΤΗΣΗ_ΚΩΔ,
                ΣΤΕΓΑΣΗ_ΠΟΣΟ = aitisi.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                ΣΙΤΙΣΗ_ΠΟΣΟ = aitisi.ΣΙΤΙΣΗ_ΠΟΣΟ,
                ΜΑΘΗΤΗΣ_ΑΦΜ = aitisi.ΜΑΘΗΤΗΣ.ΑΦΜ,
                ΗΜΝΙΑ_ΑΠΟ = aitisi.ΗΜΝΙΑ_ΑΙΤΗΣΗ > aitisi.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ ? aitisi.ΗΜΝΙΑ_ΑΙΤΗΣΗ : aitisi.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ,
                ΜΑΘΗΤΗΣ_ΕΠΩΝΥΜΟ = aitisi.ΜΑΘΗΤΗΣ.ΕΠΩΝΥΜΟ,
                ΜΑΘΗΤΗΣ_ΟΝΟΜΑ = aitisi.ΜΑΘΗΤΗΣ.ΟΝΟΜΑ,
                ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ = data.ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ,
                ΜΑΘΗΤΗΣ_ΤΑΞΗ = data.ΜΑΘΗΤΗΣ_ΤΑΞΗ,
                ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = data.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ != ep.epidomaId ? ep.epidomaId : data.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                ΜΗΝΑΣ = ep.monthId
            };
            entities.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2.Add(entity);
            entities.SaveChanges();

            data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = entity.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ;
        }

        public void Update(EpidomaSitisi2ViewModel data, EpidomaParameters2 ep)
        {
            ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2 entity = entities.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2.Find(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);

            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΙΤΙΣΗ-ΣΧΟΛΕΙΟ";
            entity.ΑΠΟΦΑΣΗ_ΚΩΔ = ep.apofasiId;
            entity.ΣΧΟΛΕΙΟ = ep.schoolId;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = ep.schoolyearId;
            entity.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = ep.epidomaId;
            entity.ΣΥΝΕΧΙΣΗ = ep.synexeia;
            entity.ΜΗΝΑΣ = ep.monthId;
            // user data
            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ;
            entity.ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ = data.ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ;
            entity.ΜΑΘΗΤΗΣ_ΤΑΞΗ = data.ΜΑΘΗΤΗΣ_ΤΑΞΗ;
            entity.ΣΙΤΙΣΗ_ΠΟΣΟ = data.ΣΙΤΙΣΗ_ΠΟΣΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(EpidomaSitisi2ViewModel data)
        {
            ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2 entity = entities.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2.Find(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2.Remove(entity);
                entities.SaveChanges();
            }
        }

        public EpidomaSitisi2ViewModel Refresh(int entityId)
        {
            return entities.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2.Select(d => new EpidomaSitisi2ViewModel
            {
                ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                // these are set in the grid by the user
                ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ = d.ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ,
                ΜΑΘΗΤΗΣ_ΤΑΞΗ = d.ΜΑΘΗΤΗΣ_ΤΑΞΗ,
                ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                ΜΗΝΑΣ = d.ΜΗΝΑΣ
            }).Where(d => d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public EpidomaSitisi2ViewModel GetRecord(int entityId)
        {
            var data = (from d in entities.ΕΠΙΔΟΜΑ_ΣΙΤΙΣΗ2
                        where d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ == entityId
                        select new EpidomaSitisi2ViewModel
                        {
                            ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ,
                            AITISI_ID = d.AITISI_ID,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΗΜΝΙΑ_ΑΠΟ = d.ΗΜΝΙΑ_ΑΠΟ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΑΦΜ = d.ΜΑΘΗΤΗΣ_ΑΦΜ,
                            ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ = d.ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ,
                            ΜΑΘΗΤΗΣ_ΕΠΩΝΥΜΟ = d.ΜΑΘΗΤΗΣ_ΕΠΩΝΥΜΟ,
                            ΜΑΘΗΤΗΣ_ΟΝΟΜΑ = d.ΜΑΘΗΤΗΣ_ΟΝΟΜΑ,
                            ΜΑΘΗΤΗΣ_ΤΑΞΗ = d.ΜΑΘΗΤΗΣ_ΤΑΞΗ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΣΥΝΕΧΙΣΗ = d.ΣΥΝΕΧΙΣΗ,
                            ΣΧΟΛΕΙΟ = d.ΣΧΟΛΕΙΟ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΜΗΝΑΣ = d.ΜΗΝΑΣ
                        }).FirstOrDefault();
            return (data);
        }

        public Aitisi2ViewModel GetRelatedAitisi2(int studentId, EpidomaParameters2 ep)
        {
            Aitisi2ViewModel data = new Aitisi2ViewModel();

            if (RelatedAitisi2Exists(studentId, ep))
            {
                data = (from d in entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2
                        where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ep.schoolyearId &&
                              d.ΜΑΘΗΤΗΣ.ΣΧΟΛΗ == ep.schoolId && d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ == ep.epidomaId && d.ΣΥΝΕΧΕΙΑ == ep.synexeia
                        select new Aitisi2ViewModel
                        {
                            ΑΙΤΗΣΗ_ΚΩΔ = d.ΑΙΤΗΣΗ_ΚΩΔ,
                            ΜΑΘΗΤΗΣ_ΚΩΔ = d.ΜΑΘΗΤΗΣ_ΚΩΔ ?? 0,
                            ΗΜΝΙΑ_ΑΙΤΗΣΗ = d.ΗΜΝΙΑ_ΑΙΤΗΣΗ,
                            ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ = d.ΜΙΣΘΩΤΗΡΙΟ_ΗΜΝΙΑ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΤΑΞΗ = d.ΤΑΞΗ,
                            ΣΥΝΕΧΕΙΑ = d.ΣΥΝΕΧΕΙΑ,
                            ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ,
                            ΣΤΕΓΑΣΗ_ΠΟΣΟ = d.ΣΤΕΓΑΣΗ_ΠΟΣΟ,
                            ΣΙΤΙΣΗ_ΠΟΣΟ = d.ΣΙΤΙΣΗ_ΠΟΣΟ,
                            ΜΑΘΗΤΗΣ = d.ΜΑΘΗΤΗΣ
                        }).FirstOrDefault();
                return data;
            }
            return null;
        }

        public bool RelatedAitisi2Exists(int studentId, EpidomaParameters2 ep)
        {
            var count = (from d in entities.ΜΑΘΗΤΗΣ_ΑΙΤΗΣΗ2
                         where d.ΜΑΘΗΤΗΣ_ΚΩΔ == studentId && d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == ep.schoolyearId &&
                               d.ΜΑΘΗΤΗΣ.ΣΧΟΛΗ == ep.schoolId && d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ == ep.epidomaId && d.ΣΥΝΕΧΕΙΑ == ep.synexeia
                         select d).Count();

            if (count > 0) return true;
            else return false;
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}