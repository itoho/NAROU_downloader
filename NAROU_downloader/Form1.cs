using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            if (urlstring != "")
            {
                // 指定したサイトのHTMLをストリームで取得する
                var doc = default(IHtmlDocument);
                using (var client = new HttpClient())
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
            }
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
            
        }

        public class Datas
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Date { get; set; }
           // public string Yobi1 { get; set; }
        }
    }
}
