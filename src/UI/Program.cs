﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AITool
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]


        static void Main()
        {

            //To prevent more than one copy running in memory, all trying to access same log and settings files
            List<Global.ClsProcess> prcs = Global.GetProcessesByPath(Assembly.GetEntryAssembly().Location, true);

            if (prcs.Count > 0)
            {
                //wait for just a bit to see if it closes by itself - for example if we had just reset/restarted:
                if (!Global.WaitForProcessToClose(prcs[0].process, 500, Assembly.GetEntryAssembly().Location))
                {
                    AppSettings.AlreadyRunning = true;
                }

            }

            AppSettings.LastShutdownState = Global.GetRegSetting("LastShutdownState", "not set");
            AppSettings.LastLogEntry = Global.GetRegSetting("LastLogEntry", "not set");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new Shell());
            }
            catch (Exception ex)
            {
                Debug.Print("Error: " + ex.ToString());
            }
            //Shell frmshell = new Shell();
            //frmshell.WindowState = FormWindowState.Minimized;
            //frmshell.ShowInTaskbar = false;
            //pplication.Run(frmshell);

        }
    }
}
