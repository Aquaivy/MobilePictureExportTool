using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PictureExportTools.Controller;
using PictureExportTools.Model;

namespace PictureExportTools.View
{
    public partial class FrmExport : Form
    {
        private ExportController controller;

        public FrmExport()
        {
            InitializeComponent();
        }

        private void FrmExportPicture_Load(object sender, EventArgs e)
        {
            var setting = SettingController.Setting;

            controller = ExportController.Create(setting);
            controller.ExportedOneFile += ExportController_ExportedOneFile;

            InitComboBoxData(setting);
        }

        private void ExportController_ExportedOneFile(object sender, ExportedOneFileEventArgs e)
        {
            float percentage = (e.Index + 1) * 1.0f / e.TotalCount * 100;
            string s = $"{e.Index + 1,3}     {e.FileData.Path}";

            listBox.Items.Add(s);
            progressBar.Value = (int)percentage;
        }

        private void InitComboBoxData(Settings setting)
        {
            comboBox.Items.Clear();

            var map = setting.Device.PathMap;
            foreach (var dir in map)
            {
                //var path = Path.Combine(setting.LocalBackupPath, dir.Name);
                comboBox.Items.Add(dir.Name);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();

            var map = SettingController.Instance.GetPathMapWithName(comboBox.Text);

            try
            {
                controller.Export(map);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
