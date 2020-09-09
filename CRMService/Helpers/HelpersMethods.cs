using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMService.Helpers {
    public static class HelpersMethods {
        public static string NormalizePhone(string param) {
            if (!string.IsNullOrEmpty(param) && !string.IsNullOrWhiteSpace(param)) {
                if (long.TryParse(param, out _)) {
                    string g = param.Replace("-", "").Replace(" ", "").Replace("+", "").ToLower();
                    if (g.Length == 11) {
                        if (g.Substring(0, 2) == "79")
                            return g.Remove(0, 1).Insert(0, "8");
                        else
                            return g;
                    }
                    return g;
                }
                else
                    return param.Replace("-", "").Replace(" ", "").Replace("+", "").ToLower();
            }
            else
                return param;
        }
    }
}