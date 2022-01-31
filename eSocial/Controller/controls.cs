using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eSocial.Controller {
    sealed partial class controller {

        internal static class controls {

            internal enum enControls { btStart, btStop, chkCleanLog, chkEnviar, chkConsultar, chkTabela, chkNaoPeriodico, chkPeriodico, lblStatus, txtLog, txtIntervalo }

            private static Dictionary<enControls, Control> iList = new Dictionary<enControls, Control>();
            private static StringBuilder log = new StringBuilder();

            public static bool addControl(enControls selControl, Control control) {
                try { iList.Add(selControl, control); } catch (Exception e) { addError("controller.controls.addControl", e.Message); return false; }
                return true;
            }

            public static Control getControl(enControls selControl) { return iList[selControl]; }

        }
    }
}