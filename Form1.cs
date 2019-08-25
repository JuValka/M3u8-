using Microsoft.VisualBasic.Devices;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace M3u8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            textBox2.Text = "Tips：请使用360急速浏览器，其它浏览器未测试";
        }

        string[] files;
        private void Button2_Click(object sender, EventArgs e)
        {
            See.Seedll.door();
            try
            {
                string cachePath = textBox1.Text;

                #region 检测
                if (cachePath == "" && comboBox1.Text == "")
                {
                    textBox2.AppendText("\r\n" + "输入浏览器缓存路径");
                }
                else if (Directory.Exists(cachePath) == false && Directory.Exists(comboBox1.Text) == false)
                {
                    textBox2.AppendText("\r\nError：Invalid directory");
                }
                else
                {
                    files = Directory.GetFiles(cachePath);
                    textBox2.AppendText("\r\n" + "检测中：搜索到碎片文件 " + files.Length + " 个");
                }
                #endregion
                pb1.Maximum = files.Length;
                pb1.Step = 1;
            }
            catch
            { }
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            pb1.Value = 0;
            try
            {
                Thread cp = new Thread(Copyfile)
                {
                    IsBackground = true
                };
                cp.Start();
            }
            catch
            {
                textBox2.AppendText("\r\n未搜索到文件");
            }
        }
        private void Copyfile()
        {
            try
            {
                if (Directory.Exists(@"M3u8_Ts") == false)
                {
                    Directory.CreateDirectory(@".\M3u8_Ts");
                }


                Computer copyf = new Computer();
                string safeName;
                for (int i = 0; i < files.Length; i++)
                {
                    safeName = Path.GetFileNameWithoutExtension(files[i]);
                    if
                    #region 判断
                           (files[i] == textBox1.Text + @"\" + "data_0" ||
                        files[i] == textBox1.Text + @"\" + "data_1" ||
                        files[i] == textBox1.Text + @"\" + "data_2" ||
                        files[i] == textBox1.Text + @"\" + "data_3" ||
                        files[i] == textBox1.Text + @"\" + "data_4" ||
                        files[i] == textBox1.Text + @"\" + "data_5" ||
                        files[i] == textBox1.Text + @"\" + "index")
                    #endregion
                    {
                        //textBox2.AppendText("\r\n不能操作data_*的文件,使用中...\r\n请无视该消息");
                        pb1.Value++;
                    }
                    else
                    {
                        //File.Copy(files[i], @".\M3u8_Ts\" + safeName);
                        // Directory.Move(files[i], @"M3u8_Ts\safeName");
                        copyf.FileSystem.CopyFile(files[i], @".\M3u8_Ts\" + safeName + ".Ts", overwrite: true);
                        pb1.Value++;
                    }
                }
                textBox2.AppendText("\r\nExport Success！！！");
            }
            catch
            {
                textBox2.AppendText("\r\nError：Invalid Files");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // See open = new See();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Process.Start(@"https:\\bbs.im-assistant.com");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.Start(@"https:\\bbs.im-assistant.com");
        }
    }
}
