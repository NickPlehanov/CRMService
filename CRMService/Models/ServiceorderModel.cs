using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMService.Models
{
    public class ServiceorderModel
    {
        [ScaffoldColumn(false)]
        public string ServiceorderId { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Причина посещения")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 200 символов")]
        public string Name { get; set; }
        
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Техник (назначен)")]
        public string ServicemanStart { get; set; }

        [Display(Name = "Техник (выполнил)")]
        public string ServicemanEnd { get; set; }

        [Display(Name = "Пришел")]
        [DataType(DataType.DateTime)]
        public DateTime Income { get; set; }

        [Display(Name = "Ушел")]
        [DataType(DataType.DateTime)]
        public DateTime Outgone { get; set; }

        [Display(Name = "Категория")]
        public int Category { get; set; }

        [Display(Name = "Примечание")]
        public string Comment { get; set; }

        [Display(Name = "Кто дал заявку")]
        public string WhoInit { get; set; }

        [Display(Name = "Перенос")]
        [DataType(DataType.Date)]
        public DateTime Moved { get; set; }

        [Display(Name = "Причина переноса")]
        public string MovedReason { get; set; }

        [Display(Name = "Результат")]
        public int ResultId { get; set; }

        [Display(Name = "Результат (примечание)")]
        public string Result { get; set; }
    }
}