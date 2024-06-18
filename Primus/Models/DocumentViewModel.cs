using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Primus.Models;
using Primus.DAL;
using System.Web.Mvc;

namespace Primus.Models
{
    public class UploadsViewModel
    {
        public int UPLOAD_ID { get; set; }

        [Display(Name = "ΒΝΣ")]
        public Nullable<int> SCHOOL_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [Display(Name = "Σχολ. έτος")]
        public Nullable<int> SCHOOLYEAR_ID { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ημερομηνία")]
        public Nullable<System.DateTime> UPLOAD_DATE { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Χειριστής")]
        public string UPLOAD_NAME { get; set; }

        [Required(ErrorMessage = "Υποχρεωτική συμπλήρωση")]
        [StringLength(500, ErrorMessage = "Πρέπει να είναι μέχρι 500 χαρακτήρες.")]
        [Display(Name = "Περιγραφή αρχείων")]
        public string UPLOAD_SUMMARY { get; set; }
    }

    public class UploadsFilesViewModel
    {
        [Display(Name = "Κωδικός")]
        public System.Guid ID { get; set; }

        [StringLength(50, ErrorMessage = "Πρέπει να είναι μέχρι 50 χαρακτήρες.")]
        [Display(Name = "Όνομα χρήστη")]
        public string SCHOOL_USER { get; set; }

        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "Σχολ. έτος")]
        public string SCHOOLYEAR_TEXT { get; set; }

        [StringLength(120, ErrorMessage = "Πρέπει να είναι μέχρι 120 χαρακτήρες.")]
        [Display(Name = "Όνομα αρχείου")]
        public string FILENAME { get; set; }

        [StringLength(10, ErrorMessage = "Πρέπει να είναι μέχρι 10 χαρακτήρες.")]
        [Display(Name = "Επέκταση")]
        public string EXTENSION { get; set; }

        public Nullable<int> UPLOAD_ID { get; set; }

        public virtual UPLOADS UPLOADS { get; set; }
    }

}