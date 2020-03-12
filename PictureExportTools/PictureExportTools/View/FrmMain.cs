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
using 手机照片导出备份工具.Controller;
using 手机照片导出备份工具.Model;

namespace 手机照片导出备份工具.View
{
    /// <summary>
    /// 功能一：
    ///     1.读取手机中所有照片
    ///     2.读取云端所有照片
    ///     3.对比得出所有新增照片
    /// 功能二：
    ///     4.一键导出所有新增照片
    /// 功能三：
    ///     5.将指定照片加入忽略列表
    ///     6.同步删除指定的手机、云端以及备份出来的照片
    /// </summary>
    public partial class FrmMain : Form
    {
        private List<Form> m_forms = new List<Form>();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                //AppController.Instance.LoadReferenceAssembly();

                //SettingController.Instance.CreateDefaultSettings();
                SettingController.Instance.Load();

                //MenuPreview_Click(sender, e);
                MenuExport_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Form Show<T>() where T : Form, new()
        {
            var form = m_forms.FirstOrDefault(o => o is T);
            if (form == null)
            {
                form = new T();
                form.MdiParent = this;
                form.FormClosed += Form_FormClosed;
                m_forms.Add(form);
            }

            m_forms.ForEach(o => o.Visible = false);
            form.Show();
            form.WindowState = FormWindowState.Minimized;
            form.WindowState = FormWindowState.Maximized;

            return form;
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form form = (Form)sender;
            form.FormClosed -= Form_FormClosed;

            var found = m_forms.FirstOrDefault(o => o == form);
            if (found != null)
            {
                m_forms.Remove(found);
            }
        }

        private void MenuExport_Click(object sender, EventArgs e)
        {
            Show<FrmExport>();
        }

        private void MenuPreview_Click(object sender, EventArgs e)
        {
            Show<FrmPreview>();
        }

        private void MenuItemHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("我并不想帮助你");
        }
    }
}
