using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;


namespace _9mayPhotoColorizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            try
            {
                if (!Directory.Exists("Improved"))
                    Directory.CreateDirectory("Improved");
                if (!Directory.Exists("Colorized"))
                    Directory.CreateDirectory("Colorized");
                if (!Directory.Exists("ColorizedImproved"))
                    Directory.CreateDirectory("ColorizedImproved");
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    string img_name = Path.GetFileNameWithoutExtension(listBox1.Items[i].ToString());

                    toolStripStatusLabel1.Text = "Статус: Колоризирую файл '" + img_name + "'...";
                    listBox1.SelectedIndex = i;

                    Colorizer clz = new Colorizer(listBox1.Items[i].ToString());
                    
                    await clz.ColorizeAsync();
                    toolStripStatusLabel1.Text = "Статус: Сохраняю файлы...";
                    try
                    {
                        if (impChBox1.Checked)
                            clz.SaveImage("Improved/" + img_name + ".jpg", ColorizerImageType.Improved);
                        if (colChBox1.Checked)
                            clz.SaveImage("Colorized/" + img_name + ".jpg", ColorizerImageType.Colorized);
                        if (bothChBox1.Checked)
                            clz.SaveImage("ColorizedImproved/" + img_name + ".jpg", ColorizerImageType.ColorizedImproved);
                    }
                    catch
                    {
                        toolStripStatusLabel1.Text = "Статус: Плохой ответ сервера.";
                        i--;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                toolStripStatusLabel1.Text = "Статус: Ожидание...";
                button1.Enabled = true;
                button2.Enabled = true;
            }           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            if(fd.ShowDialog() == DialogResult.OK)
            {
                button1.Enabled = true;
                listBox1.Items.AddRange(fd.FileNames);
            }
        }
    }
}
