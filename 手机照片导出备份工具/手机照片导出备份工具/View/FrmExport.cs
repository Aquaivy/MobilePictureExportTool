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
using 手机照片导出备份工具.Controller;
using 手机照片导出备份工具.Model;

namespace 手机照片导出备份工具.View
{
    public partial class FrmExport : Form
    {
        private ExportController exportController;

        public FrmExport()
        {
            InitializeComponent();
        }

        private void FrmExportPicture_Load(object sender, EventArgs e)
        {
            var setting = SettingController.Setting;

            exportController = new ExportController(setting);
            exportController.ExportedOneFile += ExportController_ExportedOneFile1;

            InitComboBoxData(setting);
        }

        private void ExportController_ExportedOneFile1(object sender, ExportedOneFileEventArgs e)
        {
            float percentage = (e.Index + 1) * 1.0f / e.TotalCount * 100;
            string s = $"{e.Index + 1}     {e.FileData.Path}";

            listBox.Items.Add(s);
            progressBar.Value = (int)percentage;
        }

        private void InitComboBoxData(Settings setting)
        {
            //新方案
            comboBox.Items.Clear();

            var map = setting.Device.PathMap;
            foreach (var dir in map)
            {
                //var path = Path.Combine(setting.LocalBackupPath, dir.Name);
                comboBox.Items.Add(dir.Name);
            }


            //原始方案
            //comboBox.Items.Clear();

            //var backup_path = setting.LocalBackupPath;
            //var dirs = FileUtility.GetDirectories(backup_path, "*", SearchOption.TopDirectoryOnly);
            //foreach (var dir in dirs)
            //{
            //    var path = Path.Combine(backup_path, dir);
            //    if (!Path.Equals(path, setting.RecycleBinPath))
            //    {
            //        comboBox.Items.Add(path);
            //    }
            //}
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();

            var map = SettingController.Instance.GetPathMapWithName(comboBox.Text);

            try
            {
                exportController.Compare(map);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
