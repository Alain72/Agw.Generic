using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Agw.Generic.CAD
{
    public static class Dimensions
    {
        public static void SetDefaultDimStyleCurrent(string templatePath)
        {
            var dimstylename = string.Empty;

            // First get current dimstyle from template drawing
            using (Database templatedb = new Database(false, true))
            {
                templatedb.ReadDwgFile(templatePath, FileShare.Read, false, null);
                using (Transaction trans = templatedb.TransactionManager.StartTransaction())
                {
                    ObjectId dimId = templatedb.Dimstyle;
                    var dimTabbRecord = (DimStyleTableRecord)trans.GetObject(dimId, OpenMode.ForRead);
                    if (dimTabbRecord != null) dimstylename = dimTabbRecord.Name;
                }
            }

            // set in current db
            using (Transaction trans = Active.Database.TransactionManager.StartTransaction())
            {
                DimStyleTable dimTabb = (DimStyleTable)trans.GetObject(Active.Database.DimStyleTableId, OpenMode.ForRead);

                if (!string.IsNullOrEmpty(dimstylename) && dimTabb.Has(dimstylename))
                {
                    DimStyleTableRecord dimTabbRecord = (DimStyleTableRecord)trans.GetObject(dimTabb[dimstylename], OpenMode.ForRead);
                    if (dimTabbRecord.ObjectId != Active.Database.Dimstyle)
                    {
                        Active.Database.Dimstyle = dimTabbRecord.ObjectId;
                        Active.Database.SetDimstyleData(dimTabbRecord);
                    }
                }
                trans.Commit();
            }
        }
    }
}
