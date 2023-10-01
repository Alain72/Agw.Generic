using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
