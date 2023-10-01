using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Interop;

namespace AvgW.Generic.CAD
{
    public class AvgWPreferences
    {
        private readonly AcadPreferences acPrefComObj;

        public AvgWPreferences()
        {
            acPrefComObj = (AcadPreferences)Application.Preferences;
        }

        public string QnewTemplate()
        {
            return acPrefComObj.Files.QNewTemplateFile.ToString();
        }
       
    }
}
