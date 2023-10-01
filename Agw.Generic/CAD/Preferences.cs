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
