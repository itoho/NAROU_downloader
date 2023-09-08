using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using AngleSharp.Scripting;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace NAROU_downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string urlmode = "";
        public string[] kakuyomuno = new string[2048];
        private async void Button_getlist_Click(object sender, EventArgs e)
        {
            var urlstring = Linkbox.Text;
            List<Datas> list = new List<Datas>();
            // 指定したサイトのHTMLをストリームで取得する
            var doc = default(IHtmlDocument);
            var client = new HttpClient();
            if (Regex.IsMatch(urlstring, "ncode"))
            {//なろう陽
                if (urlstring != "")
                {
                    urlmode = "narou";
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                    client.DefaultRequestHeaders.Add("Accept-Language", "ja,en-US;q=0.9,en;q=0.8");
                    client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    //client.DefaultRequestHeaders.Add("Cookie", "ks2=t2v7btk1n9b; sasieno=0; lineheight=0; fontsize=0; novellayout=0; fix_menu_bar=1; _ga=GA1.2.1902699348.1547314490; _td=9ee358d4-fce9-4a46-b773-5219be007c16; _gid=GA1.2.1913627487.1565845692; OX_plg=pm; nlist1=6h7h.ai-a99g.9m-ovfz.5g-8kjb.gn-gdxz.0-etft.1c; _gat=1");
                    client.DefaultRequestHeaders.Add("DNT", "1");
                    client.DefaultRequestHeaders.Add("Host", "ncode.syosetu.com");
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");
                    //client.DefaultRequestHeaders.Add("Referer:", urlstring);

                    using (var stream = await client.GetStreamAsync(new Uri(urlstring)))
                    {
                        // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                        var parser = new HtmlParser();
                        doc = await parser.ParseAsync(stream);
                    }

                    // HTMLからtitleタグの値(サイトのタイトルとして表示される部分)を取得する
                    var title = doc.Title;
                    //var a = doc.GetElementsByClassName("novel_writername");("#container > #novel_contents > #novel_color > #novel_writername  a");
                    var auther_Text = doc.QuerySelector(".novel_writername");//作者名の抽出
                    Title_text.Text = title;

                    string auther_raw = auther_Text.OuterHtml;
                    auther_raw = auther_raw.Substring(auther_raw.IndexOf("\">") + 6, auther_raw.IndexOf("</div>") - auther_raw.IndexOf("\">") - 6);
                    auther.Text = auther_raw;
                    var wasuu_tmp = doc.QuerySelectorAll(".novel_sublist2");//リストいっこ一戸
                    var kousinji = doc.QuerySelectorAll(".long_update");
                    Wasuu_text.Text = wasuu_tmp.Length.ToString();
                    var data = wasuu_tmp[0];//小説の章名
                                            //Wasuu_text.Text = data.TextContent;


                    for (int i = 0; i < wasuu_tmp.Length; i++)
                    {
                        list.Add(new Datas
                        {
                            Id = i + 1,
                            Title = wasuu_tmp[i].TextContent,
                            Date = kousinji[i].TextContent,
                            //Yobi1 = " "

                        });

                    }
                }

            }

            if (Regex.IsMatch(urlstring, "kakuyomu"))//カクヨム用
            {
                if (urlstring != "")
                {
                    urlmode = "kakuyomu";
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                    client.DefaultRequestHeaders.Add("Accept-Language", "ja,en-US;q=0.9,en;q=0.8");
                    client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    //client.DefaultRequestHeaders.Add("Cookie", "ks2=t2v7btk1n9b; sasieno=0; lineheight=0; fontsize=0; novellayout=0; fix_menu_bar=1; _ga=GA1.2.1902699348.1547314490; _td=9ee358d4-fce9-4a46-b773-5219be007c16; _gid=GA1.2.1913627487.1565845692; OX_plg=pm; nlist1=6h7h.ai-a99g.9m-ovfz.5g-8kjb.gn-gdxz.0-etft.1c; _gat=1");
                    client.DefaultRequestHeaders.Add("DNT", "1");
                    client.DefaultRequestHeaders.Add("Host", "kakuyomu.jp");
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");
                    //client.DefaultRequestHeaders.Add("Referer:", "https:\/\/kakuyomu.jp\/search?q=%E8%B1%9A&order=popular");

                    using (var stream = await client.GetStreamAsync(new Uri(urlstring)))
                    {
                        // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                        var parser = new HtmlParser();
                        doc = await parser.ParseAsync(stream);
                    }

                    // HTMLからtitleタグの値(サイトのタイトルとして表示される部分)を取得する
                    var title = doc.Title;
                    //var a = doc.GetElementsByClassName("novel_writername");("#container > #novel_contents > #novel_color > #novel_writername  a");
                    var auther_Text = doc.GetElementById("workAuthor-activityName");//作者名の抽出#workAuthor-activityName
                    Title_text.Text = title;

                    string auther_raw = auther_Text.InnerHtml;//
                    auther_raw = auther_raw.Substring(auther_raw.IndexOf("href") + 20, auther_raw.IndexOf("</a>") - auther_raw.IndexOf("/\">") - 24);
                    auther.Text = auther_raw;
                    var wasuu_tmp = doc.QuerySelectorAll(".widget-toc-episode span");//リストいっこ一戸#table-of-contents > section > div > ol > li:nth-child(2)
                   
                    var kousinji = doc.QuerySelectorAll(".widget-toc-episode-datePublished");
                    Wasuu_text.Text = wasuu_tmp.Length.ToString();
                    var data = wasuu_tmp[0];//小説の章名
                                            //Wasuu_text.Text = data.TextContent;
                    var blueListItemsCSS = doc.QuerySelectorAll("a[class='widget-toc-episode-episodeTitle']");

                    //print href attributes value to console
                    //foreach (var item in blueListItemsCSS)
                    //{
                    //    Console.WriteLine(item.GetAttribute("href"));
                    //}
                    Console.WriteLine(doc);

                    for (int i = 0; i < wasuu_tmp.Length; i++)
                    {
                        list.Add(new Datas
                        {
                            Id = i + 1,
                            Title = wasuu_tmp[i].TextContent,
                            Date = kousinji[i].TextContent,
                            //Yobi1 = " "

                        });
                        var tmpstring = blueListItemsCSS[i].GetAttribute("href").Split('/');
                        kakuyomuno[i] = tmpstring[tmpstring.Length-1];
                    }
                    Console.WriteLine(kakuyomuno[3]);
                }
            }

                dataGridView1.DataSource = list;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            Linkbox.Clear();
            Title_text.Clear();
            auther.Clear();
            Wasuu_text.Clear();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 2;
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
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                TaskbarManager.Instance.SetProgressValue(0, progressBar1.Maximum);
                for (int k = 0; k < array.Length; k++)
                {
                    await Task.Delay(600);
                    Console.WriteLine(array[k]);
                    downloadmethod(array[k] + 1, array[k]);

                }
                await Task.Delay(3200);
                progressBar1.Value = 0;
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            }
            if (comboBox2.SelectedIndex == 0)
            {
                int[] array = new int[Int32.Parse(Wasuu_text.Text)];
                progressBar1.Maximum = array.Length;
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                TaskbarManager.Instance.SetProgressValue(0, progressBar1.Maximum);
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = i;
                    await Task.Delay(800);
                    downloadmethod(i+1,i);
                }
                await Task.Delay(3200);

                progressBar1.Value = 0;
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            }



           


        }
        public async void downloadmethod(int syou,int mode)
        {

            string urlstring="sample";
            if (urlstring != "")
            {
                // 指定したサイトのHTMLをストリームで取得する
                var doc = default(IHtmlDocument);
                var client = new HttpClient();
                if (urlmode.Equals("narou"))
                {
                    urlstring = Linkbox.Text + syou + "/";
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                    client.DefaultRequestHeaders.Add("Accept-Language", "ja,en-US;q=0.9,en;q=0.8");
                    client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    //client.DefaultRequestHeaders.Add("Cookie", "ks2=t2v7btk1n9b; sasieno=0; lineheight=0; fontsize=0; novellayout=0; fix_menu_bar=1; _ga=GA1.2.1902699348.1547314490; _td=9ee358d4-fce9-4a46-b773-5219be007c16; _gid=GA1.2.1913627487.1565845692; OX_plg=pm; nlist1=6h7h.ai-a99g.9m-ovfz.5g-8kjb.gn-gdxz.0-etft.1c; _gat=1");
                    client.DefaultRequestHeaders.Add("DNT", "1");
                    client.DefaultRequestHeaders.Add("Host", "ncode.syosetu.com");
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");
                    //client.DefaultRequestHeaders.Add("Sec-Fetch-Mode:", "navigate");
                    client.DefaultRequestHeaders.Add("Sec-Fetch-User", " ?1");
                    client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                }
                if (urlmode.Equals("kakuyomu"))
                {
                    urlstring = Linkbox.Text+ "/episodes/"+kakuyomuno[syou - 1];
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                    client.DefaultRequestHeaders.Add("Accept-Language", "ja,en-US;q=0.9,en;q=0.8");
                    client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    //client.DefaultRequestHeaders.Add("Cookie", "ks2=t2v7btk1n9b; sasieno=0; lineheight=0; fontsize=0; novellayout=0; fix_menu_bar=1; _ga=GA1.2.1902699348.1547314490; _td=9ee358d4-fce9-4a46-b773-5219be007c16; _gid=GA1.2.1913627487.1565845692; OX_plg=pm; nlist1=6h7h.ai-a99g.9m-ovfz.5g-8kjb.gn-gdxz.0-etft.1c; _gat=1");
                    client.DefaultRequestHeaders.Add("DNT", "1");
                    client.DefaultRequestHeaders.Add("Host", "kakuyomu.jp");
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");

                }
                //client.DefaultRequestHeaders.Add("Referer:", urlstring);

                using (var stream = await client.GetStreamAsync(new Uri(urlstring)))
                {
                    // AngleSharp.Parser.Html.HtmlParserオブジェクトにHTMLをパースさせる
                    var parser = new HtmlParser();
                    doc = await parser.ParseAsync(stream);
                }

                string honbun = "";
                string ffname = "";
                // HTMLからtitleタグの値(サイトのタイトルとして表示される部分)を取得する
                if (urlmode.Equals("narou"))
                {
                    //var a = doc.GetElementsByClassName("novel_writername");("#container > #novel_contents > #novel_color > #novel_writername  a");
                    var midasi = doc.QuerySelector(".novel_subtitle");
                    var Texti = doc.QuerySelectorAll(".novel_view");
                    honbun = midasi.TextContent + "\n\n";
                    var auther_Text = doc.QuerySelector(".novel_writername");
                    ffname = urlstring.Substring(urlstring.IndexOf(".com/") + 5, 7);
                    //var honbun = Texti[0].TextContent;
                    for (int i = 0; i < Texti.Length; i++)
                    {
                        honbun += "\r";
                        honbun += Texti[i].TextContent;

                    }
                }
                if (urlmode.Equals("kakuyomu"))
                {
                    var midasi = doc.QuerySelector("#contentMain-header");
                    var Texti = doc.QuerySelectorAll(".widget-episode-inner");
                    honbun = midasi.TextContent + "\n\n";
                    var auther_Text = doc.QuerySelector(".novel_writername");
                    ffname = auther.Text;
                    //var honbun = Texti[0].TextContent;
                    for (int i = 0; i < Texti.Length; i++)
                    {
                        honbun += "\r";
                        honbun += Texti[i].TextContent;

                    }
                }



                    //Wasuu_text.Text = data.TextContent;

                    
                await Task.Delay(2000);
                Console.WriteLine(ffname + "OK");
                string filename = "";
                WiteFile(honbun,ffname+ String.Format("{0:D3}", syou),comboBox1.SelectedIndex,syou);
                progressBar1.Value += 1;
                TaskbarManager.Instance.SetProgressValue(progressBar1.Value, progressBar1.Maximum);
                
            }

        }
        public void WiteFile(string text,string fname,int enc,int num)
        {
            DirectoryUtils.SafeCreateDirectory("Downloads\\"+Title_text.Text);
            string enco="";
            StreamWriter sw;
            if (enc == 0)
            {
                enco = "shift-jis";
                text.Replace("\n", "\r");
            }

            if (enc == 1)
            {
                enco = "utf-16";
            }
            int folderno = 1;
            folderno = ((num - 1) -( (num - 1) % 200)+200)/200;
            DirectoryUtils.SafeCreateDirectory("Downloads\\" + Title_text.Text +"\\"+folderno);
            sw = new StreamWriter("Downloads\\"+Title_text.Text + "\\" + folderno.ToString()+"\\"+ fname + ".txt", false, Encoding.GetEncoding(enco));
            
            //text.Replace("\n", "\r");
            //string fname = String.Format("{0:D3}", num);
            sw.Write(text);
            sw.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("EXPLORER.EXE", "Downloads");
        }

        /// <summary>
        /// Directory クラスに関する汎用関数を管理するクラス
        /// </summary>
        public static class DirectoryUtils
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
    }
}
