using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Agw.Generic.Cad
{
    public static class Layers
    {

        public static string SetLayer(string LayerName, short Kleur = 7, bool Isplottable = true)
        {

            using (DocumentLock mylock = Active.Document.LockDocument())
            {
                using (Transaction transactie = Active.Document.TransactionManager.StartTransaction())
                {

                    try
                    {
                        if (!string.IsNullOrEmpty(LayerName))
                        {
                            LayerTable LagenTabel = default(LayerTable);
                            LagenTabel = (LayerTable)transactie.GetObject(Active.Database.LayerTableId, OpenMode.ForWrite);

                            if (!LagenTabel.Has(LayerName))
                            {
                                Autodesk.AutoCAD.Colors.Color mycolor = default(Autodesk.AutoCAD.Colors.Color);
                                mycolor = Autodesk.AutoCAD.Colors.Color.FromColorIndex(Autodesk.AutoCAD.Colors.ColorMethod.ByAci, Kleur);

                                //Then add the layer object to the layertable
                                LayerTableRecord LaagObject = new LayerTableRecord();
                                LaagObject.Color = mycolor;
                                LaagObject.Name = LayerName;
                                LaagObject.IsPlottable = Isplottable;
                                LagenTabel.Add(LaagObject);

                                transactie.AddNewlyCreatedDBObject(LaagObject, true);
                            }

                        }

                        transactie.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Something is wrong: " + Environment.NewLine + ex.Message);
                        return string.Empty;
                    }

                }
            }

            return LayerName;

        }

        public static void UnlockLayer(string layerName)
        {

            FreezeThawLockUnlockOnOffLayer(layerName, null, null, false, null);

        }

        /// <summary>
        ///  Set Layer states,  use Null for no change
        /// </summary>
        public static void FreezeThawLockUnlockOnOffLayer(string layerName, bool? frozen, bool? locked, bool? off, bool? hidden)
        {
            using (DocumentLock mylock = Active.Document.LockDocument())
            {
                using (Transaction transactie = Active.Document.TransactionManager.StartTransaction())
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(layerName))
                        {
                            LayerTable LagenTabel = default(LayerTable);
                            LagenTabel = (LayerTable)transactie.GetObject(Active.Database.LayerTableId, OpenMode.ForWrite);

                            if (LagenTabel.Has(layerName))
                            {
                                LayerTableRecord acLyrTblRec = transactie.GetObject(LagenTabel[layerName],
                                    OpenMode.ForWrite) as LayerTableRecord;

                                if (off != null) acLyrTblRec.IsOff = (bool)off;
                                if (frozen != null) acLyrTblRec.IsFrozen = (bool)frozen;
                                if (locked != null) acLyrTblRec.IsLocked = (bool)locked;
                                if (hidden != null) acLyrTblRec.IsHidden = (bool)hidden;
                            }
                        }
                        transactie.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Something is wrong: " + Environment.NewLine + ex.Message);
                    }
                }
            }
        }


        public static Dictionary<string, List<bool>> CreateLayerOnOffDict(bool thaw = false, bool unlock = false, bool on = false)
        {

            Dictionary<string, List<bool>> m_dict = new Dictionary<string, List<bool>>();
            using (DocumentLock mylock = Active.Document.LockDocument())
            {
                using (Transaction transactie = Active.Document.TransactionManager.StartTransaction())

                    try
                    {
                        LayerTable acLyrTbl = default(LayerTable);
                        acLyrTbl = (LayerTable)transactie.GetObject(Active.Database.LayerTableId, OpenMode.ForWrite);

                        foreach (ObjectId acObjId in acLyrTbl)
                        {
                            LayerTableRecord acLyrTblRec = default(LayerTableRecord);
                            acLyrTblRec = (LayerTableRecord)transactie.GetObject(acObjId, OpenMode.ForWrite);
                            List<bool> mylist = new List<bool>();
                            mylist.Add(acLyrTblRec.IsOff);

                            mylist.Add(acLyrTblRec.IsFrozen);
                            mylist.Add(acLyrTblRec.IsLocked);
                            mylist.Add(acLyrTblRec.IsHidden);
                            m_dict.Add(acLyrTblRec.Name, mylist);

                            if (on) acLyrTblRec.IsOff = false;
                            if (thaw) acLyrTblRec.IsFrozen = false;
                            if (unlock) acLyrTblRec.IsLocked = false;
                        }

                        transactie.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Something is wrong: " + Environment.NewLine + ex.Message);
                        return null;
                    }

            }
            return m_dict;

        }

        public static void SetLayerOnOffState(Dictionary<string, List<bool>> dict)
        {
            using (DocumentLock mylock = Active.Document.LockDocument())
            {
                using (Transaction transactie = Active.Document.TransactionManager.StartTransaction())
                {
                    try
                    {
                        LayerTable acLyrTbl = default(LayerTable);
                        acLyrTbl = (LayerTable)transactie.GetObject(Active.Database.LayerTableId, OpenMode.ForWrite);

                        foreach (ObjectId acObjId in acLyrTbl)
                        {
                            LayerTableRecord acLyrTblRec = default(LayerTableRecord);
                            acLyrTblRec = (LayerTableRecord)transactie.GetObject(acObjId, OpenMode.ForWrite);
                            List<bool> mylist = default(List<bool>);
                            //Reset it, but check on current layer. (can't Freeze that one :-) )

                            if (dict.Keys.Contains(acLyrTblRec.Name) & Active.Database.Clayer != acLyrTblRec.ObjectId)
                            {
                                mylist = dict[acLyrTblRec.Name];
                                acLyrTblRec.IsOff = mylist[0];
                                acLyrTblRec.IsFrozen = mylist[1];
                                acLyrTblRec.IsLocked = mylist[2];
                                acLyrTblRec.IsHidden = mylist[3];
                            }
                        }
                        transactie.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Something is wrong: " + Environment.NewLine + ex.Message);
                        return;
                    }
                }
            }
        }
    }
}
