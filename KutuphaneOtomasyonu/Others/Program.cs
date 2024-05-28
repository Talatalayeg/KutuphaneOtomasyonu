using KutuphaneOtomasyonu.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyonu
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new Panelislemleri());
            Application.Run(new GirisPaneli());

            //Application.Run(new CalisanPaneli());
            //Application.Run(new UyePaneli());
            //Application.Run(new KitapPaneli());

            //Application.Run(new KiralamaPaneli());
            //Application.Run(new OdemePaneli());
            //Application.Run(new RaporPaneli());

        }
    }
}
