using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAROU_downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Button_getlist_Click(object sender, EventArgs e)
        {
            var urlstring = Linkbox.Text;
            if (urlstring.Length == 7)
            {
                urlstring = "https://ncode.syosetu.com/" + urlstring + "/";
                Linkbox.Text = urlstring;
            }
            if (checkBox1.Checked == true)
            {
                urlstring = Properties.Settings.Default.bridge_school + Linkbox.Text;
            }
            
            if (urlstring != "")
            {
                // 指定したサイトのHTMLをストリームで取得する
                var doc = default(IHtmlDocument);
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Referer", "https://ncode.syosetu.com/novelview/infotop/ncode/n5993ba/");
                client.DefaultRequestHeaders.Add("Host", "ncode.syosetu.com");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
                client.DefaultRequestHeaders.Add("Accept-Language", "ja,en-US;q=0.9,en;q=0.8");
                client.DefaultRequestHeaders.Add("DNT","1");
                using (var stream = await client.GetStreamAsync(new Uri(urlstring)))
                {
                    // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                    var parser = new HtmlParser();
                    doc = await parser.ParseAsync(stream);
                }

                // HTMLからtitleタグの値(サイトのタイトルとして表示される部分)を取得する
                var title = doc.Title;
                //var a = doc.GetElementsByClassName("novel_writername");("#container > #novel_contents > #novel_color > #novel_writername  a");
                var auther_Text = doc.QuerySelector(".novel_writername");
                Title_text.Text = title;

                string auther_raw = auther_Text.InnerHtml;
                auther_raw = auther_raw.Substring(auther_raw.IndexOf("/\">") + 3, auther_raw.IndexOf("</a>") - auther_raw.IndexOf("/\">") - 3);
                auther.Text = auther_raw;
                var wasuu_tmp = doc.QuerySelectorAll(".novel_sublist2");
                var kousinji = doc.QuerySelectorAll(".long_update");
                Wasuu_text.Text = wasuu_tmp.Length.ToString();
                var data = wasuu_tmp[0];
                //Wasuu_text.Text = data.TextContent;
                List<Datas> list = new List<Datas>();

                for (int i = 0; i < wasuu_tmp.Length; i++) {
                    list.Add(new Datas
                    {
                        Id = i + 1,
                        Title = wasuu_tmp[i].TextContent,
                        Date = kousinji[i].TextContent,
                        //Yobi1 = " "

                    });
                }
                
                dataGridView1.DataSource = list;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            Linkbox.Clear();
            Title_text.Clear();
            auther.Clear();
            Wasuu_text.Clear();
            checkBox1.Checked = false;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
        }

        public class Datas
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Date { get; set; }
           // public string Yobi1 { get; set; }
        }

        private async void Dl_button_Click(object sender, EventArgs e)
        {
            int addtime=0;
            if (checkBox1.Checked == true) addtime = 400;
            DirectoryUtils.SafeCreateDirectory("Downloads\\"+Title_text.Text);
            if (comboBox2.SelectedIndex == 1)
            {
                List<Datas> list = new List<Datas>();
                int[]array=new int[dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected)];
                int j = -1;
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                    
                {
                    j++; 
                    Console.WriteLine(r.Index);
                    array[j] = r.Index ;
                    


                }
                Array.Sort(array);
                progressBar1.Maximum = array.Length ;
                for (int k = 0; k < array.Length; k++)
                {
                    await Task.Delay(700+addtime);
                    Console.WriteLine(array[k]);
                    downloadmethod(array[k] + 1, array[k]);

                }
                await Task.Delay(3200);
                progressBar1.Value = 0;
            }
            if (comboBox2.SelectedIndex == 0)
            {
                int[] array = new int[Int32.Parse(Wasuu_text.Text)];
                progressBar1.Maximum = array.Length;
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = i;
                    await Task.Delay(850+addtime);
                    downloadmethod(i+1,i);
                }
                await Task.Delay(3200);

                progressBar1.Value = 0;
            }



           


        }
        public async void downloadmethod(int syou,int mode)
        {

            var urlstring = Linkbox.Text+syou+"/";
            if (checkBox1.Checked == true)
            {
                urlstring =Properties.Settings.Default.bridge_school + Linkbox.Text + syou + "/";
                Console.WriteLine(urlstring);
            }
            if (urlstring != "")
            {
                // 指定したサイトのHTMLをストリームで取得する
                var doc = default(IHtmlDocument);
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Referer", "https://ncode.syosetu.com/novelview/infotop/ncode/n5993ba/");
                client.DefaultRequestHeaders.Add("Host", "ncode.syosetu.com");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
                client.DefaultRequestHeaders.Add("Accept-Language", "ja,en-US;q=0.9,en;q=0.8");
                client.DefaultRequestHeaders.Add("DNT", "1");
                using (var stream = await client.GetStreamAsync(new Uri(urlstring)))
                {
                    // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                    var parser = new HtmlParser();
                    doc = await parser.ParseAsync(stream);
                }



                //var a = doc.GetElementsByClassName("novel_writername");("#container > #novel_contents > #novel_color > #novel_writername  a");
                var midasi = doc.QuerySelector(".novel_subtitle");
                var Texti = doc.QuerySelectorAll(".novel_view");
                //var honbun = Texti[0].TextContent;
                string honbun = "";
                honbun = midasi.TextContent + "\n\n\n";
                for (int i = 0; i < Texti.Length; i++)
                {
                    honbun += Texti[i].TextContent;
                }
                


                //Wasuu_text.Text = data.TextContent;

                var auther_Text = doc.QuerySelector(".novel_writername");
                var ffname=urlstring.Substring(urlstring.IndexOf(".com/")+5,7);
                //await Task.Delay(2000);
                Console.WriteLine(ffname + "OK");
                WiteFile(honbun,ffname+syou.ToString(),comboBox1.SelectedIndex);
                progressBar1.Value += 1;
                label7.Text = progressBar1.Value.ToString()+"/"+progressBar1.Maximum.ToString();
            }

        }
        public void WiteFile(string text,string fname,int enc)
        {
            string enco;

            if (enc == 0) enco = "shift-jis";
            if (enc == 1) enco = "utf-8";
            text.Replace("\n", "\r\n");
            StreamWriter sw = new StreamWriter("Downloads\\"+Title_text.Text+"\\" + fname+".txt", false, Encoding.GetEncoding("shift_jis"));
            sw.Write(text);
            sw.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("EXPLORER.EXE", "Downloads");
        }
        public static class DirectoryUtils//DirectoryUtils.SafeCreateDirectory( "Assets/Textures" ); http://baba-s.hatenablog.com/entry/2014/06/09/210016
        {
            /// <summary>
            /// 指定したパスにディレクトリが存在しない場合
            /// すべてのディレクトリとサブディレクトリを作成します
            /// </summary>
            public static DirectoryInfo SafeCreateDirectory(string path)
            {
                if (Directory.Exists(path))
                {
                    return null;
                }
                return Directory.CreateDirectory(path);
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var urlstring = Linkbox.Text + (e.RowIndex+1) + "/";
            var ffname = urlstring.Substring(urlstring.IndexOf(".com/") + 5, 7);

            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            /*
            messageBoxCS.AppendFormat("{0} = {1}", "ColumnIndex", e.ColumnIndex);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("{0} = {1}", "RowIndex", e.RowIndex);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("{0} = {1}", "String", "Downloads\\" + Title_text.Text + "\\" + ffname + (e.RowIndex + 1) + ".txt");
            messageBoxCS.AppendLine();
            */
            //MessageBox.Show(messageBoxCS.ToString(), "CellDoubleClick Event");
            
            if (File.Exists("Downloads\\" + Title_text.Text + "\\" + ffname + (e.RowIndex + 1) + ".txt"))
            {
                //messageBoxCS.AppendFormat("ダウンロードファイルが存在します。");
                //messageBoxCS.AppendLine();


                DialogResult result = MessageBox.Show("ダウンロード済みです。簡易リーダーで開きますか？",
                "確認",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button2);

                //何が選択されたか調べる
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    System.Diagnostics.Process.Start("wordpad.exe", @"Downloads\\" + Title_text.Text + "\\" + ffname + (e.RowIndex + 1) + ".txt");
                    return;
                }
                else if (result == DialogResult.No)
                {
                    //「いいえ」が選択された時
                    return;
                }
                else if (result == DialogResult.Cancel)
                {
                    //「キャンセル」が選択された時
                    return ;
                    
                }



            }
            else
            {
                messageBoxCS.AppendFormat("ダウンロードファイルが存在しません。");
                messageBoxCS.AppendLine();
                messageBoxCS.AppendLine();
            }
            MessageBox.Show(messageBoxCS.ToString(), "");

            
        }
    }
}
