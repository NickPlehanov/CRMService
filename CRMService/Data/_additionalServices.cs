using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMService.Data {
    public class _additionalServices {
        public _additionalServices(string name, bool enabled) {
            Name = name;
            Enabled = enabled;
        }

        public string Name { get; set; }
        public bool Enabled { get; set; } = false;
    }
}