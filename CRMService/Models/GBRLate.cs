using CRMService.Data;
using CRMService.Data.A28;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace CRMService.Models {
    [Table("New_alarmExtensionBase")]
    public class GBRLate {
        [Key]
        public Guid? new_alarmid { get; set; }
        public string new_name { get; set; }
        public bool? new_onc { get; set; }
        public bool? new_tpc { get; set; }

        private int? _new_group { get; set; }
        public int? new_group {
            get => _new_group;
            set {
                _new_group = value;
            }
        }

        private DateTime? _new_arrival { get; set; }
        public DateTime? new_arrival {
            get => _new_arrival;
            set {
                if (value.HasValue)
                    _new_arrival = DateTime.Parse(value.Value.ToString());
            }
        }

        private DateTime? _new_cancel { get; set; }
        public DateTime? new_cancel {
            get => _new_cancel;
            set {
                if (value.HasValue)
                    _new_cancel = DateTime.Parse(value.Value.ToString());
            }
        }
        private DateTime? _new_departure { get; set; }
        public DateTime? new_departure {
            get => _new_departure;
            set {
                if (value.HasValue)
                    _new_departure = DateTime.Parse(value.Value.ToString());
            }
        }

        private Guid? _new_andromeda_alarm { get; set; }
        public Guid? new_andromeda_alarm {
            get => _new_andromeda_alarm;
            set {
                _new_andromeda_alarm = value;
            }
        }

        private string _new_number {
            get {
                using (AndromedaContext andromedaContext = new AndromedaContext()) {
                    Guid guid = Guid.Parse(new_andromeda_alarm.ToString());
                    return andromedaContext.Andromeda.FirstOrDefault(x => x.New_andromedaId == guid).New_number.ToString().Replace('\"', '\'');
                }
            }
            set => value = string.IsNullOrEmpty(value) ? "" : value;
        }
        [NotMapped]
        public string new_number {
            get => _new_number;
            set {
                _new_number = value;
            }
        }

        private string _new_objname { get; set; }
        [NotMapped]
        public string new_objname {
            get => _new_objname;
            set {
                using (AndromedaContext andromedaContext = new AndromedaContext()) {
                    Guid guid = Guid.Parse(new_andromeda_alarm.ToString());
                    _new_objname = andromedaContext.Andromeda.FirstOrDefault(x => x.New_andromedaId == guid).New_name.Replace('\"', '\'');
                }
            }
        }
        [NotMapped]
        public string ObjectID {
            get => _ObjectID;
            set {
                _ObjectID = value;
            }
        }
        private string _ObjectID {
            get {
                using (ObjectContext objectContext = new ObjectContext()) {
                    int r = -1;
                    if (int.TryParse(new_number, out r)) {
                        string res = Convert.ToInt32(r.ToString(), 16).ToString();
                        return objectContext.ObjectA28.FirstOrDefault(x => x.RecordDeleted == false && x.ObjectNumber.ToString() == res).ObjectID.ToString();
                    }
                    else
                        return null;
                }
            }
            set => value = string.IsNullOrEmpty(value) ? "" : value;
        }

        private string _new_address { get; set; }
        [NotMapped]
        public string new_address {
            get => _new_address;
            set {
                using (AndromedaContext andromedaContext = new AndromedaContext()) {
                    Guid guid = Guid.Parse(new_andromeda_alarm.ToString());
                    _new_address = andromedaContext.Andromeda.FirstOrDefault(x => x.New_andromedaId == guid).New_address.Replace('\"', '\'');
                }
            }
        }

        public bool? new_owner { get; set; }
        public bool? new_police { get; set; }
        public bool? new_order { get; set; }
        public bool? new_act { get; set; }

        private DateTime? _new_alarm_dt { get; set; }
        public DateTime? new_alarm_dt {
            get => _new_alarm_dt;
            set {
                if (value.HasValue)
                    _new_alarm_dt = DateTime.Parse(value.Value.ToString());
            }
        }

        public string new_zone { get; set; }
        public bool? new_ps { get; set; }
        [NotMapped]
        public string delta { get; set; }
        [NotMapped]
        public string delta_operator { get; set; }
        [NotMapped]
        public string _delta_operator { get; set; }
        [NotMapped]
        public string result { get; set; }

        [NotMapped]
        public string os {
            get => _os;
            set {
                _os = value;
            }
        }
        private string _os {
            get {
                if (new_onc.HasValue) {
                    bool os_value = false;
                    bool.TryParse(new_onc.ToString(), out os_value);
                    if (os_value)
                        return "+";
                    else
                        return "-";
                }
                else
                    return "-";
            }
            set {
                bool _os_value;
                if (bool.TryParse(value, out _os_value))
                    if (_os_value)
                        value = "true";

            }
        }

        [NotMapped]
        public string ps {
            get => _ps;
            set {
                _ps = value;
            }
        }
        private string _ps {
            get {
                if (new_ps.HasValue) {
                    bool ps_value = false;
                    bool.TryParse(new_ps.ToString(), out ps_value);
                    if (ps_value)
                        return "+";
                    else
                        return "-";
                }
                else
                    return "-";
            }
            set {
                bool _ps_value;
                if (bool.TryParse(value, out _ps_value))
                    if (_ps_value)
                        value = "true";

            }
        }

        [NotMapped]
        public string tpc {
            get => _tpc;
            set {
                _tpc = value;
            }
        }
        private string _tpc {
            get {
                if (new_tpc.HasValue) {
                    bool tpc_value = false;
                    bool.TryParse(new_tpc.ToString(), out tpc_value);
                    if (tpc_value)
                        return "+";
                    else
                        return "-";
                }
                else
                    return "-";
            }
            set {
                bool _tpc_value;
                if (bool.TryParse(value, out _tpc_value))
                    if (_tpc_value)
                        value = "true";

            }
        }

        [NotMapped]
        public string owner {
            get => _owner;
            set {
                _owner = value;
            }
        }
        private string _owner {
            get {
                if (new_owner.HasValue) {
                    bool owner_value = false;
                    bool.TryParse(new_owner.ToString(), out owner_value);
                    if (owner_value)
                        return "+";
                    else
                        return "-";
                }
                else
                    return "-";
            }
            set {
                bool _owner_value;
                if (bool.TryParse(value, out _owner_value))
                    if (_owner_value)
                        value = "true";
            }
        }

        [NotMapped]
        public string police {
            get => _police;
            set {
                _police = value;
            }
        }
        private string _police {
            get {
                if (new_police.HasValue) {
                    bool police_value = false;
                    bool.TryParse(new_police.ToString(), out police_value);
                    if (police_value)
                        return "+";
                    else
                        return "-";
                }
                else
                    return "-";
            }
            set {
                bool _police_value;
                if (bool.TryParse(value, out _police_value))
                    if (_police_value)
                        value = "true";
            }
        }

        [NotMapped]
        public string order {
            get => _order;
            set {
                _order = value;
            }
        }
        private string _order {
            get {
                if (new_order.HasValue) {
                    bool order_value = false;
                    bool.TryParse(new_order.ToString(), out order_value);
                    if (order_value)
                        return "+";
                    else
                        return "-";
                }
                else
                    return "-";
            }
            set {
                bool _order_value;
                if (bool.TryParse(value, out _order_value))
                    if (_order_value)
                        value = "true";
            }
        }

        [NotMapped]
        public string act {
            get => _act;
            set {
                _act = value;
            }
        }
        private string _act {
            get {
                if (new_act.HasValue) {
                    bool act_value = false;
                    bool.TryParse(new_act.ToString(), out act_value);
                    if (act_value)
                        return "+";
                    else
                        return "-";
                }
                else
                    return "-";
            }
            set {
                bool _act_value;
                if (bool.TryParse(value, out _act_value))
                    if (_act_value)
                        value = "true";
            }
        }
    }
    public class GBRLateContext : DbContext {
        public GBRLateContext() : base("VityazMSCRMEntity") { }
        public DbSet<GBRLate> GBRLate { get; set; }
    }
}