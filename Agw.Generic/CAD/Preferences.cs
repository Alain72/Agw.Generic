using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Interop;

namespace Agw.Generic.CAD
{
    public class AgwPreferences
    {
        private readonly AcadPreferences acPrefComObj;

        public AgwPreferences()
        {
            acPrefComObj = (AcadPreferences)Application.Preferences;
        }

        public string QnewTemplate()
        {
            return acPrefComObj.Files.QNewTemplateFile.ToString();
        }
       
    }
}
