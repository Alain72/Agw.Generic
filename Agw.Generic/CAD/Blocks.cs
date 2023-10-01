
namespace Agw.Generic.CAD
{
    internal class Blocks
    {
        private void SynchronizeAllAtributes()
        {
            Autodesk.AutoCAD.ApplicationServices.Core.Application.SetSystemVariable("CMDECHO", 0);
            Active.Editor.Command("ATTSYNC", "N", "*");
            Autodesk.AutoCAD.ApplicationServices.Core.Application.SetSystemVariable("CMDECHO", 1);
        }

        
    }
}
