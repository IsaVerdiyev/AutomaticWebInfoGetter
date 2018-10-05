using AutomaticWebInfoGetterWpfLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AutomaticWebInfoGetter
{
    class CustomApplicationContext: ApplicationContext
    {
        Mutex mutex;

        IContainer container;

        NotifyIcon notifyIcon;

        ControllerClass controllerClass;

        public CustomApplicationContext()
        {
            if (IsAlreadyExecuting())
            {
                Environment.Exit(1);
            }
            else
            {
                InitializeContext();
                controllerClass = new ControllerClass();
            }
            
        }

        bool IsAlreadyExecuting()
        {
            bool isCreated;
            string mutexName = System.Reflection.Assembly.GetExecutingAssembly().GetType().GUID.ToString();
            mutex = new Mutex(false, mutexName, out isCreated);

            return !isCreated;
        }

        private void InitializeContext()
        {
            container = new Container();
            notifyIcon = new NotifyIcon(container)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new Icon("Icon.ico"),
                Text = "AutomaticWebInfoGetter",
                Visible = true
            };
            notifyIcon.DoubleClick += ContextMenuStripOpen_Click;
            notifyIcon.ContextMenuStrip.Items.Add("Open").Click += ContextMenuStripOpen_Click;
            notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += ContextMenuStripExit_Click;
        }

        void ContextMenuStripOpen_Click(object sender, EventArgs e)
        {
            controllerClass.OpenWindow();
        }

        void ContextMenuStripExit_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }

    }
}
