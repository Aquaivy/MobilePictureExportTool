using Aquaivy.Core.Utilities;
using Newtonsoft.Json;
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
    public partial class FrmPreview : Form
    {
        private PreviewController controller;

        public FrmPreview()
        {
            InitializeComponent();
        }

        private void FrmPreview_Load(object sender, EventArgs e)
        {
            var setting = SettingController.Setting;

            controller = PreviewController.Create(setting);

            InitComboBoxData(setting);
        }

        private void InitComboBoxData(Settings setting)
        {
            //新方案
            comboBox.Items.Clear();

            var map = setting.Device.PathMap;
            foreach (var dir in map)
            {
                var path = Path.Combine(setting.LocalBackupPath, dir.Name);
                comboBox.Items.Add(path);
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

        private void InitListViewData(List<FileData> files)
        {
            ClearForm();

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var image = ImageTools.LoadPictureToImage(file.Path);

                imageList.Images.Add(file.Name, image);

                ListViewItem viewItem = new ListViewItem(file.Name);
                viewItem.ToolTipText = file.Name;
                viewItem.ImageIndex = i;
                viewItem.Tag = file;

                listView1.Items.Add(viewItem);
            }

            //设置项图片展示大小与像素
            imageList.ImageSize = new Size(64, 64);
            imageList.ColorDepth = ColorDepth.Depth24Bit;

            //将项与列表绑定并设置部分属性控制列表展示及操作的类型
            listView1.LargeImageList = imageList;
            listView1.SmallImageList = imageList;
            listView1.ItemSelectionChanged += ListView1_ItemSelectionChanged;
        }

        private void ClearForm()
        {
            listView1.Items.Clear();
            imageList.Images.Clear();
            pictureBox.Image = null;
        }

        private void ListView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                var file = e.Item.Tag as FileData;
                pictureBox.Image = ImageTools.LoadPictureToImage(file.Path);
            }
            else
            {
                //var file = e.Item.Tag as FileData;
                //this.Text = file.Name;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            RemoveCurrentSelectImage();
        }

        private void RemoveCurrentSelectImage()
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }

            try
            {
                var selectItem = listView1.SelectedItems[0];
                var imageIndex = listView1.Items.IndexOf(selectItem);
                var file = selectItem.Tag as FileData;

                listView1.Items.Remove(selectItem);
                controller.RemoveFile(file);

                if (imageIndex >= listView1.Items.Count)
                {
                    imageIndex = listView1.Items.Count - 1;
                }
                if (imageIndex >= 0)
                {
                    listView1.Items[imageIndex].Selected = true;
                }
                else
                {
                    pictureBox.Image = null;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "文件已存在");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误");
            }
        }

        private void FrmPreview_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Left)
            //{
            //    SelectPrevImage();
            //}
            //else if (e.KeyCode == Keys.Right)
            //{
            //    SelectNextImage();
            //}
            //else 
            if (e.KeyCode == Keys.Delete)
            {
                RemoveCurrentSelectImage();
            }
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                controller.ReadFiles(comboBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            InitListViewData(controller.Files);

            if (listView1.Items.Count > 0)
                listView1.Items[0].Selected = true;
        }

        //private void SelectPrevImage()
        //{
        //    var selectItem = listView1.SelectedItems[0];
        //    var imageIndex = listView1.Items.IndexOf(selectItem);

        //    if (imageIndex > 0)
        //    {
        //        listView1.Items[imageIndex - 1].Selected = true;
        //    }
        //}

        //private void SelectNextImage()
        //{
        //    var selectItem = listView1.SelectedItems[0];
        //    var imageIndex = listView1.Items.IndexOf(selectItem);

        //    if (imageIndex < listView1.Items.Count - 1)
        //    {
        //        listView1.Items[imageIndex + 1].Selected = true;
        //    }
        //}
    }
}
