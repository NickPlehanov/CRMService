using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMService.Models
{
    public class ServicemanModel
    {
        [ScaffoldColumn(false)]
        public string ServicemanId { get; set; }

        [Required(ErrorMessage="Обязательное поле")]
        [Display(Name="Имя")]
        [StringLength(50, MinimumLength=1, ErrorMessage="Длина строки должна быть от 1 до 50 символов")]
        public string Name { get; set; }
        
        
        [Display(Name="Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Пароль для мобильного приложения")]
        public string password { get; set; }
    }
}



