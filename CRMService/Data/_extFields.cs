using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMService.Data {
    public class _extFields {
        public _extFields(string extFieldName, string extFieldValue) {
            ExtFieldName = extFieldName;
            ExtFieldValue = extFieldValue;
        }

        public string ExtFieldName { get; set; }
        public string ExtFieldValue { get; set; }            
    }
}