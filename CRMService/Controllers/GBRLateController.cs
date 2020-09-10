using CRMService.Data;
using CRMService.Data.A28;
using CRMService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMService.Controllers {
    public class GBRLateController : Controller {
        //
        // GET: /GBRLate/
        DateTime G_begin;
        DateTime G_end;

        public ActionResult Index(string begin, string end, string late) {
            List<GBRLate> lates = new List<GBRLate>();
            DateTime _begin = new DateTime(), _end = new DateTime();
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
                G_begin = _begin;
                G_end = _end;
                List<GBRLate> aeb = new List<GBRLate>();
                using (GBRLateContext context = new GBRLateContext()) {
                    var d = context.GBRLate.Where(x => x.new_alarm_dt != null && x.new_alarm_dt > _begin
                              && x.new_alarm_dt <= _end);
                    foreach (GBRLate item in d) {
                        if (item.new_arrival.HasValue && item.new_alarm_dt.HasValue)
                            if ((item.new_arrival - item.new_alarm_dt).Value.TotalMinutes >= _late) {
                                //string id = null;
                                //using (ObjectContext objectContext = new ObjectContext()) {
                                //    int r = -1;
                                //    if (int.TryParse(item.new_number, out r)) {
                                //        string res = Convert.ToInt32(r.ToString(), 16).ToString();
                                //        id=objectContext.ObjectA28.FirstOrDefault(x => x.RecordDeleted == false && x.ObjectNumber.ToString() == res).ObjectID.ToString();
                                //    }
                                //}
                                lates.Add(new GBRLate() {
                                    ObjectID = item.ObjectID,
                                    new_act = item.new_act,
                                    new_alarmid = item.new_alarmid,
                                    new_alarm_dt = item.new_alarm_dt.Value.AddHours(5),
                                    new_andromeda_alarm = item.new_andromeda_alarm,
                                    new_address = item.new_address,
                                    new_number = item.new_number,
                                    new_objname = item.new_objname,
                                    new_arrival = item.new_arrival.Value.AddHours(5),
                                    new_cancel = item.new_cancel.Value.AddHours(5),
                                    new_departure = item.new_departure.Value.AddHours(5),
                                    new_group = item.new_group + 69,
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
                                    delta_operator = TimeSpan.FromMinutes(Convert.ToDouble((item.new_departure - item.new_alarm_dt).Value.TotalMinutes.ToString())).ToString(@"hh\:mm\:ss"),
                                    _delta_operator = TimeSpan.FromMinutes(Convert.ToDouble((item.new_departure - item.new_alarm_dt).Value.TotalMinutes.ToString())).ToString(@"mm"),
                                    result = "OK"
                                });
                            }
                    }
                }
            }
            else
                lates.Add(new GBRLate() { result = null });
            return View(lates);
        }

        public ActionResult GroupInfo(string group) {
            List<GBRLate> groupInfos = new List<GBRLate>();
            if (string.IsNullOrEmpty(group))
                groupInfos.Add(new GBRLate() { result = null });
            else {
                int _group = -1;
                if (int.TryParse(group, out _group)) {
                    using (GBRLateContext context = new GBRLateContext()) {
                        var gs = context.GBRLate.Where(x => x.new_alarm_dt != null && x.new_alarm_dt > G_begin
                              && x.new_alarm_dt <= G_end);
                        groupInfos = gs.Where(x => x.new_group == _group).OrderBy(y=>y.new_alarm_dt).ToList();
                    }
                }
                else
                    groupInfos.Add(new GBRLate() { result = null });
            }
            return View(groupInfos);
        }
    }
}
