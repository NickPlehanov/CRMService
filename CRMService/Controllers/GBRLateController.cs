using CRMService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMService.Controllers
{
    public class GBRLateController : Controller
    {
        //
        // GET: /GBRLate/

        public ActionResult Index(string begin, string end, string late)
        {
            List<GBRLate> lates = new List<GBRLate>();
            DateTime _begin=new DateTime(), _end = new DateTime();
            int _late;
            try {
                _begin = DateTime.Parse(begin, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
            }
            catch { }
            try {
                _end = DateTime.Parse(end, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
            }
            catch { }
            if (DateTime.TryParse(_begin.ToString(), out _begin) && DateTime.TryParse(_end.ToString(), out _end) && int.TryParse(late, out _late)) {
                _begin = _begin.AddHours(-5);
                _end = _end.AddHours(-5);
                List<GBRLate> aeb = new List<GBRLate>();
                using (GBRLateContext context = new GBRLateContext()) {
                    var d = context.GBRLate.Where(x => x.new_alarm_dt != null && x.new_alarm_dt > _begin
                              && x.new_alarm_dt <= _end);
                    foreach (GBRLate item in d) {
                        if (item.new_arrival.HasValue && item.new_alarm_dt.HasValue)
                            if ((item.new_arrival - item.new_alarm_dt).Value.TotalMinutes >= _late)
                                lates.Add(new GBRLate() {
                                    new_act = item.new_act,
                                    new_alarmid = item.new_alarmid,
                                    new_alarm_dt = item.new_alarm_dt,
                                    new_andromeda_alarm = item.new_andromeda_alarm,
                                    new_address = item.new_address,
                                    new_number = item.new_number,
                                    new_objname = item.new_objname,
                                    new_arrival = item.new_arrival,
                                    new_cancel = item.new_cancel,
                                    new_departure = item.new_departure,
                                    new_group = item.new_group,
                                    new_name = item.new_name,
                                    new_onc = item.new_onc,
                                    new_order = item.new_order,
                                    new_owner = item.new_owner,
                                    new_police = item.new_police,
                                    new_ps = item.new_ps,
                                    new_tpc = item.new_tpc,
                                    new_zone = item.new_zone,
                                    //delta = (item.new_arrival - item.new_alarm_dt).Value.TotalMinutes.ToString(),
                                    delta = TimeSpan.FromMinutes(Convert.ToDouble((item.new_arrival - item.new_alarm_dt).Value.TotalMinutes.ToString())).ToString(@"hh\:mm\:ss"),
                                    result = "OK"
                                });
                    }
                }
            }
            else
                lates.Add(new GBRLate() { result = null });
            return View(lates);
        }

    }
}
