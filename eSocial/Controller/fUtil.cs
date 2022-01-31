using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using System.Collections.Concurrent;
using System.Text;

namespace eSocial.Controller {
   public static class fUtil {
      
      public static string fCleanChars(string sStringToClean) {

         byte[] bytes = Encoding.GetEncoding("iso-8859-8").GetBytes(sStringToClean);
         sStringToClean = Encoding.UTF8.GetString(bytes);
         string sReturn = "";
         var arr = sStringToClean.ToCharArray();

         foreach (var c in arr) {
            if ((c >= 32 && c <= 126) || (c >= 9 && c <= 13)) {
               sReturn += c.ToString();
            }
            else {
               sReturn += " ";
            }
         }
         return sReturn;
      }  
   }

}
