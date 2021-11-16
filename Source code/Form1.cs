using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace FBSMigracionBlazor
{
    #region XML comment
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    #endregion
    public class Form1 : System.Windows.Forms.Form
    {
        #region private declaration
        private System.Windows.Forms.Button cmdWriteAspx;
        private System.Windows.Forms.TextBox txPath;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtModeloMigrar;
        private System.Windows.Forms.Label lblModeloMigrar;
        private System.Windows.Forms.GroupBox grpName;
        #region XML comment
        /// <summary>
        /// Required designer variable.
        /// </summary>
        #endregion
        private System.ComponentModel.Container components = null;
        #endregion
        #region constructor
        #region Form1()
        public Form1()
        {
            this.InitializeComponent();
        } // end public Form1()
        #endregion
        #endregion
        #region void Dispose(bool disposing)
        #region XML comment
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                } // end if(components != null)
            } // end if(disposing)
            base.Dispose(disposing);
        } // end protected override void Dispose(bool disposing)
        #endregion
        #region Windows Form Designer generated code
        #region void InitializeComponent()
        #region XML comment
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        #endregion
        private void InitializeComponent()
        {
            this.cmdWriteAspx = new System.Windows.Forms.Button();
            this.txPath = new System.Windows.Forms.TextBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.lblModeloMigrar = new System.Windows.Forms.Label();
            this.txtModeloMigrar = new System.Windows.Forms.TextBox();
            this.grpName = new System.Windows.Forms.GroupBox();
            this.grpName.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdWriteAspx
            // 
            this.cmdWriteAspx.Location = new System.Drawing.Point(47, 163);
            this.cmdWriteAspx.Name = "cmdWriteAspx";
            this.cmdWriteAspx.Size = new System.Drawing.Size(192, 48);
            this.cmdWriteAspx.TabIndex = 4;
            this.cmdWriteAspx.Text = "Migrar";
            this.cmdWriteAspx.Click += new System.EventHandler(this.cmdWriteAspx_Click);
            // 
            // txPath
            // 
            this.txPath.Location = new System.Drawing.Point(106, 15);
            this.txPath.Name = "txPath";
            this.txPath.Size = new System.Drawing.Size(220, 20);
            this.txPath.TabIndex = 3;
            this.txPath.Text = "C:\\ProcesaMigracionBlazor";
            this.txPath.Click += new System.EventHandler(this.txPath_Click);
            // 
            // lblPath
            // 
            this.lblPath.Location = new System.Drawing.Point(8, 18);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(92, 16);
            this.lblPath.TabIndex = 2;
            this.lblPath.Text = "Ruta:";
            this.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblModeloMigrar
            // 
            this.lblModeloMigrar.Location = new System.Drawing.Point(8, 42);
            this.lblModeloMigrar.Name = "lblModeloMigrar";
            this.lblModeloMigrar.Size = new System.Drawing.Size(100, 16);
            this.lblModeloMigrar.TabIndex = 6;
            this.lblModeloMigrar.Text = "Modelo a Migrar:";
            this.lblModeloMigrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtModeloMigrar
            // 
            this.txtModeloMigrar.Location = new System.Drawing.Point(106, 41);
            this.txtModeloMigrar.Name = "txtModeloMigrar";
            this.txtModeloMigrar.Size = new System.Drawing.Size(220, 20);
            this.txtModeloMigrar.TabIndex = 7;
            this.txtModeloMigrar.Text = "Modelo a Migrar";
            // 
            // grpName
            // 
            this.grpName.Controls.Add(this.txPath);
            this.grpName.Controls.Add(this.lblPath);
            this.grpName.Controls.Add(this.txtModeloMigrar);
            this.grpName.Controls.Add(this.lblModeloMigrar);
            this.grpName.Location = new System.Drawing.Point(8, 40);
            this.grpName.Name = "grpName";
            this.grpName.Size = new System.Drawing.Size(332, 96);
            this.grpName.TabIndex = 21;
            this.grpName.TabStop = false;
            this.grpName.Text = "Name";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(352, 239);
            this.Controls.Add(this.grpName);
            this.Controls.Add(this.cmdWriteAspx);
            this.Name = "Form1";
            this.Text = "Windows forms => Web forms";
            this.grpName.ResumeLayout(false);
            this.grpName.PerformLayout();
            this.ResumeLayout(false);

        } // end private void InitializeComponent()
        #endregion
        #endregion
        #region Main
        #region void Main()
        #region XML comment
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        #endregion
        [STAThread()]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.DoEvents();
            Application.Run(new Form1());
        } // end static void Main()
        #endregion
        #endregion
        #region private methods (event handler)
        #region void cmdWriteAspx_Click(object sender, System.EventArgs e)
        private void cmdWriteAspx_Click(object sender, System.EventArgs e)
        {

            //var z = ObtenerTiposEnNamespaces(Assembly.GetExecutingAssembly(), "Generales.Mantenimiento");
            List<object> objectList = new List<object>()
            {
                new Generales.Mantenimiento.BancoInformacionAdicionalVista(),
                new Generales.Mantenimiento.EmpresaParametroMensajeria_Vista(),
                new Generales.Mantenimiento.BancoInformacionSpi_Vista(),
                new Generales.Mantenimiento.EmpresaParametroLog_Vista(),
                new Generales.Mantenimiento.Empresa_ParametroBusquedaCliente_Vista(),
                new Generales.Mantenimiento.Empresa_ParametroPersonaDatosCrediticios_Vista(),
                new Generales.Mantenimiento.Empresa_ParametroVerificaHuellaVista(),
            };
            string directory = this.txPath.Text;

            try
            {
                System.IO.DirectoryInfo directoryInfo;
                directoryInfo = new System.IO.DirectoryInfo(directory);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
            }
            catch (System.IO.IOException ioEx)
            {
                MessageBox.Show(ioEx.Message);
            }

            try
            {
                var procesaMigracion = new ProcesaMigracion();
                //objectList.ForEach(x => procesaMigracion.Procesa((Control)x, directory));
                var empresaParametroPersonaDatosCrediticia = new Generales.Mantenimiento.BancoInformacionSpi_Vista();
                procesaMigracion.Procesa(empresaParametroPersonaDatosCrediticia, directory);
            }
            catch (ApplicationException appEx)
            {
                MessageBox.Show(appEx.Message);
            }
            MessageBox.Show($"Has been written {objectList.Count} files to:\n" + directory);
        }
        #endregion

        #endregion

        private void txPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Seleccione la Ruta" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.txPath.Text = fbd.SelectedPath;
                }
            }
        }

        private List<Type> ObtenerTiposEnNamespaces(Assembly assembly, string nameSpace)
        {
            //dynamic df= Assembly.LoadFrom(@"d:\German\.net\projects\WinForms2WebForms_src\Source code\Generales.dll");            

            dynamic xcv = Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList();
            var z = assembly.GetTypes();
            var wty = xcv[4];
            var f = AppDomain.CurrentDomain.GetAssemblies();

            Assembly lpo = Assembly.Load(wty);
            try
            {
                var ty = lpo.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                // now look at ex.LoaderExceptions - this is an Exception[], so:
                foreach (Exception inner in ex.LoaderExceptions)
                {
                    Console.WriteLine(inner.Message);
                }
            }
            var s = Type.GetType("Generales.Mantenimiento.BancoInformacionAdicionalVista");
            var y = assembly.GetTypes()
                     .Where(t => String.Equals(t.Namespace, nameSpace))
                     .ToArray();
            var x = assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToList();
            return x;
        }

    }
} 