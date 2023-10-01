using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agw.Generic.CAD
{
    internal class Annotative
    {
        private static void PurgeAnnotativeScale()
        {

            using (Active.Document.LockDocument())
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {

                ObjectContextManager ocm = Active.Database.ObjectContextManager;
                if (ocm != null)
                {
                    ObjectContextCollection occ = ocm.GetContextCollection("ACDB_ANNOTATIONSCALES");
                    if (occ != null)
                    {
                        ObjectIdCollection oic = new ObjectIdCollection();
                        foreach (ObjectContext oc in occ)
                        {
                            if (oc is AnnotationScale)
                                oic.Add(new ObjectId(oc.UniqueIdentifier)
                                );
                        }

                        // Check the object references using Purge 
                        //(this does NOT purge the objects, it only filters the objects that are not purgable)
                        Active.Database.Purge(oic);

                        // Now let's erase each of the objects left
                        foreach (ObjectId id in oic)
                        {
                            try
                            {
                                DBObject obj = tr.GetObject(id, OpenMode.ForWrite);
                                obj.Erase();
                            }
                            catch
                            {
                                MessageBox.Show("Error in AnnotativePurge");
                            }
                        }
                        tr.Commit();
                    }
                }
            }
        }
    }
}
