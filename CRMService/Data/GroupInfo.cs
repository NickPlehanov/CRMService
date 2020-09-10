using CRMService.Data.A28;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CRMService.Data {
    [Table("New_alarmExtensionBase")]
    public class GroupInfo {
        [Key]
        public Guid? new_alarmid { get; set; }

        private DateTime? _new_arrival { get; set; }
        public DateTime? new_arrival {
            get => _new_arrival;
            set {
                if (value.HasValue)
                    _new_arrival = DateTime.Parse(value.Value.ToString()).AddHours(-5);
            }
        }

        private DateTime? _new_cancel { get; set; }
        public DateTime? new_cancel {
            get => _new_cancel;
            set {
                if (value.HasValue)
                    _new_cancel = DateTime.Parse(value.Value.ToString()).AddHours(-5);
            }
        }

        private DateTime? _new_departure { get; set; }
        public DateTime? new_departure {
            get => _new_departure;
            set {
                if (value.HasValue)
                    _new_departure = DateTime.Parse(value.Value.ToString()).AddHours(-5);
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
        [NotMapped]
        public string ObjectID {
            get => _ObjectID;
            set {
                _ObjectID = value;
            }
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

        [NotMapped]
        public bool IsDel { get; set; }

        private int? _new_group { get; set; }
        public int? new_group {
            get => _new_group;
            set {
                _new_group = value;
            }
        }

        private DateTime? _new_alarm_dt { get; set; }
        public DateTime? new_alarm_dt {
            get => _new_alarm_dt;
            set {
                if (value.HasValue)
                    _new_alarm_dt = DateTime.Parse(value.Value.ToString()).AddHours(-5);
            }
        }
    }
    public class GroupInfoContext : DbContext {
        public GroupInfoContext() : base("VityazMSCRMEntity") { }
        public DbSet<GroupInfo> GroupInfo { get; set; }
    }
}