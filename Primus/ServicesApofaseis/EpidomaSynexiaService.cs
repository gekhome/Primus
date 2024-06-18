using System;
using System.Collections.Generic;
using System.Linq;
using Primus.BPM;
using Primus.DAL;
using Primus.Models;
using System.Data.Entity;

namespace Primus.Services
{
    public class EpidomaSynexiaService : IEpidomaSynexiaService, IDisposable
    {
        private readonly PrimusDBEntities entities;

        public EpidomaSynexiaService(PrimusDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<EpidomaSynexeiaViewModel> Read(EpidomaParameters ep)
        {
            List<EpidomaSynexeiaViewModel> data = new List<EpidomaSynexeiaViewModel>();

            if (ep.apofasiId > 0)
            {
                data = (from d in entities.ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ep.apofasiId
                        orderby d.ΜΑΘΗΤΗΣ_ΕΠΩΝΥΜΟ, d.ΜΑΘΗΤΗΣ_ΟΝΟΜΑ, d.ΜΑΘΗΤΗΣ_ΤΑΞΗ
                        select new EpidomaSynexeiaViewModel
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
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).ToList();
            }
            return data;
        }

        public void Create(EpidomaSynexeiaViewModel data, EpidomaParameters ep, AitisiViewModel aitisi)
        {
            var apofasi = (from d in entities.ΑΠΟΦΑΣΗ_ΣΥΝΕΧΕΙΑ where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ep.apofasiId select d).FirstOrDefault();

            ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ entity = new ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ()
            {
                ΑΠΟΦΑΣΗ_ΚΩΔ = apofasi.ΑΠΟΦΑΣΗ_ΚΩΔ,
                ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = apofasi.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΝΕΧΕΙΑ",
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
                ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = data.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ != ep.epidomaId ? ep.epidomaId : data.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ
            };
            entities.ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ.Add(entity);
            entities.SaveChanges();

            data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ = entity.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ;
        }

        public void Update(EpidomaSynexeiaViewModel data, EpidomaParameters ep)
        {
            ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ entity = entities.ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ.Find(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);

            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΝΕΧΕΙΑ";
            entity.ΑΠΟΦΑΣΗ_ΚΩΔ = ep.apofasiId;
            entity.ΣΧΟΛΕΙΟ = ep.schoolId;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = ep.schoolyearId;
            entity.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = ep.epidomaId;
            entity.ΣΥΝΕΧΙΣΗ = true;
            // user data
            entity.ΜΑΘΗΤΗΣ_ΚΩΔ = data.ΜΑΘΗΤΗΣ_ΚΩΔ;
            entity.ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ = data.ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ;
            entity.ΜΑΘΗΤΗΣ_ΤΑΞΗ = data.ΜΑΘΗΤΗΣ_ΤΑΞΗ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(EpidomaSynexeiaViewModel data)
        {
            ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ entity = entities.ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ.Find(data.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public EpidomaSynexeiaViewModel Refresh(int entityId)
        {
            return entities.ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ.Select(d => new EpidomaSynexeiaViewModel
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
                ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = d.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ
            }).Where(d => d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public EpidomaSynexeiaViewModel GetRecord(int entityId)
        {
            var data = (from d in entities.ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ
                        where d.ΕΠΙΔΟΤΗΣΗ_ΚΩΔ == entityId
                        select new EpidomaSynexeiaViewModel
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
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        }).FirstOrDefault();
            return data;
        }

        public void UpdateRecord(EpidomaSynexeiaViewModel data, int epidotisiId)
        {
            ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ entity = entities.ΕΠΙΔΟΜΑ_ΣΥΝΕΧΕΙΑ.Find(epidotisiId);

            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = data.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ;
            entity.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ = data.ΕΠΙΔΟΜΑ_ΕΙΔΟΣ;
            entity.ΗΜΝΙΑ_ΑΠΟ = data.ΗΜΝΙΑ_ΑΠΟ;
            entity.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = data.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;
            entity.ΜΑΘΗΤΗΣ_ΑΦΜ = data.ΜΑΘΗΤΗΣ_ΑΦΜ;
            entity.ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ = data.ΜΑΘΗΤΗΣ_ΕΙΔΙΚΟΤΗΤΑ;
            entity.ΜΑΘΗΤΗΣ_ΕΠΩΝΥΜΟ = data.ΜΑΘΗΤΗΣ_ΕΠΩΝΥΜΟ;
            entity.ΜΑΘΗΤΗΣ_ΟΝΟΜΑ = data.ΜΑΘΗΤΗΣ_ΟΝΟΜΑ;
            entity.ΜΑΘΗΤΗΣ_ΤΑΞΗ = data.ΜΑΘΗΤΗΣ_ΤΑΞΗ;
            entity.ΣΙΤΙΣΗ_ΠΟΣΟ = data.ΣΙΤΙΣΗ_ΠΟΣΟ;
            entity.ΣΤΕΓΑΣΗ_ΠΟΣΟ = data.ΣΤΕΓΑΣΗ_ΠΟΣΟ;
            entity.ΣΥΝΕΧΙΣΗ = data.ΣΥΝΕΧΙΣΗ;
            entity.ΣΧΟΛΕΙΟ = data.ΣΧΟΛΕΙΟ;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}