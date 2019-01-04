using System;
using Autodesk.AutoCAD.Windows;
using System.Windows.Forms;

namespace Agw.Generic.ExtensionMethods
{
    public static class PaletteSetExtensions
    {
        public static void HidePalletteAndRunCommand(this PaletteSet paletteSet, Action action)
        {
            if (Active.Document == null)
            {
                MessageBox .Show("No Active Document");
                return;
            }
            paletteSet.Visible = false;
            try
            {
                action();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Oops!" + Environment.NewLine +
                                "Something went wrong." + Environment.NewLine +
                                "Error details:" +
                                Environment.NewLine +
                                e.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }
            finally
            {
                paletteSet.Visible = true;
               
            }

        }
    }
}
