using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMService.Models
{
    public class ContactModel
    {
        [ScaffoldColumn(false)]
        public string ContactId { get; set; }

        [Display(Name = "Фамилия")]
        public string Lastname { get; set; }

        [Display(Name = "Имя")]
        public string Firstname { get; set; }

        [Display(Name = "Рабочий телефон")]
        public string Workphone { get; set; }

        [Display(Name = "Должность")]
        public string Workplace { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }
    }
}