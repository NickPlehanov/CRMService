using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CRMService.Data;
using CRMService.Data.A28;

namespace CRMService.Models {
    public class GuardObjectModel
    {
        [ScaffoldColumn(false)]
        public string GuardObjectId { get; set; }

        [Display(Name = "Дата ввода")]
        public string DateIn { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "НомерАндромеда")]
        public string Number { get; set; }

        [Display(Name = "Номер")]
        public string NumberHex { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Тип сигнализации")]
        public string Sygnalizations { get; set; }

        [Display(Name = "Тип объекта")]
        public string ObjectType { get; set; }

        [Display(Name = "Шаблон событий")]
        public string EventTemplate { get; set; }

        [Display(Name = "Контрольное время")]
        public string ControlTime { get; set; }

        [Display(Name = "Пароли")]
        public string ObjectPassword { get; set; }

        [Display(Name = "Телефон 1")]
        public string Phone1 { get; set; }

        [Display(Name = "Телефон 2")]
        public string Phone2 { get; set; }

        [Display(Name = "Телефон 3")]
        public string Phone3 { get; set; }

        [Display(Name = "Удаленное программирование GUID")]
        public string RemoteProgrammingGUID { get; set; }


        //public Dictionary<string, string> ExtFields { get; set; }
        public List<_extFields> ExtFields { get; set; }

        public List<ObjCust> Owners { get; set; }

        public List<ObjCust> CustAdmins { get; set; }

        public List<EP> SendSMS { get; set; }

        public List<_additionalServices> addServices { get; set; }
    }

    //public class PageInfo
    //{
    //    public int PageNumber { get; set; }
    //    public int PageSize { get; set; }
    //    public int TotalItems { get; set; }
    //    public int TotalPages
    //    {
    //        get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
    //    }
    //}

    //public class IndexViewGuardObjectModel
    //{
    //    public IEnumerable<GuardObjectModel> GuardObjects { get; set; }
    //    public PageInfo PageInfo { get; set; }
    //}
}