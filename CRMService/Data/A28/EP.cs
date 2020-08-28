using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CRMService.Data.A28 {
    [Table("EP")]
    public class EP {
        [Key]
        public int ProcID { get; set; }
        public bool Enabled { get; set; }
        public string Params { get; set; }
        public int ProcGroupID { get; set; }

        private string _number {
            get {
                if (Params.Contains("DestAddress")) {
                    string[] c = Params.Split(';');
                    foreach (var item in c) {
                        if (item.Contains("DestAddress"))
                            return item.Substring(item.IndexOf('=') + 1, item.Length - item.IndexOf('=') - 1);
                        else
                            continue;
                    }
                }
                return null;
            }
            set { }
        }
        [NotMapped]
        public string number {
            get => _number;
            set {
                _number = value;
            }
        }
    }
    public class EPContext : DbContext {
        public EPContext() : base("A28Entity") { }
        public DbSet<EP> EP { get; set; }
    }
}