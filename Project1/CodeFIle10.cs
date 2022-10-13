using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http;
//using System.Globalization;
using AngleSharp;
using AngleSharp.Html.Parser;
using System.Diagnostics;//ブラウザ起動

class MyClass : Form
{
    string str, btn_st;
    string[] stArray, akstn, title_list, url_list;
    ArrayList array_btn1 = new ArrayList();
    ArrayList array_btn2 = new ArrayList();
    Button btn16, btn17, btn18, btn19, btn20, btn21;//aiueo 戻る
    bool kanji_check = false;
    public MyClass()
    {
        stArray = new string[] { "あ", "か", "さ", "た", "な", "は", "ま", "や", "ら", "小,゛,゜", "わ", "空白", "消", "漢字", "検索" };
        this.StartPosition = FormStartPosition.Manual; //起動位置
        this.Location = new Point(0, 0); //起動位置
        Text = "視線入力キーボード";
        BackColor = SystemColors.Window;
        Size = new Size(600, 630);
        Font = new Font("", 40); ;
        int count = 0;
        title_list = new string[12];
        url_list = new string[12];

        for (int i = 0; i < 5;i++) {
            for (int j = 0; j < 3 ; j++) {
                array_btn1.Add(new Button());
                Button btn = (Button)array_btn1[count];
                btn.Parent = this;
                btn.Text = stArray[count];
                btn.Location = new Point(10+j*190, 40+i*110);
                btn.BackColor = SystemColors.Control;
                btn.Size = new Size(180, 100);
                btn.Click += new EventHandler(btn_Click);
                count++;
            }
        }

        btn16 = new Button();
        btn16.Parent = this;
        btn16.Text = "";
        btn16.Location = new Point(200, 160);
        btn16.BackColor = SystemColors.Control;
        btn16.Size = new Size(180, 100);
        btn16.Click += new EventHandler(btn_Click);

        btn17 = new Button();
        btn17.Parent = this;
        btn17.Text = "";
        btn17.Location = new Point(10, 160);
        btn17.BackColor = SystemColors.Control;
        btn17.Size = new Size(180, 100);
        btn17.Click += new EventHandler(btn_Click);

        btn18 = new Button();
        btn18.Parent = this;
        btn18.Text = "";
        btn18.Location = new Point(200, 50);
        btn18.BackColor = SystemColors.Control;
        btn18.Size = new Size(180, 100);
        btn18.Click += new EventHandler(btn_Click);

        btn19 = new Button();
        btn19.Parent = this;
        btn19.Text = "";
        btn19.Location = new Point(390, 160);
        btn19.BackColor = SystemColors.Control;
        btn19.Size = new Size(180, 100);
        btn19.Click += new EventHandler(btn_Click);

        btn20 = new Button();
        btn20.Parent = this;
        btn20.Text = "";
        btn20.Location = new Point(200, 270);
        btn20.BackColor = SystemColors.Control;
        btn20.Size = new Size(180, 100);
        btn20.Click += new EventHandler(btn_Click);

        btn21 = new Button();
        btn21.Parent = this;
        btn21.Text = "戻る";
        btn21.Location = new Point(10, 270);
        btn21.BackColor = SystemColors.Control;
        btn21.Size = new Size(180, 100);
        btn21.Click += new EventHandler(btn_Click);

        btn16.Visible = false;
        btn17.Visible = false;
        btn18.Visible = false;
        btn19.Visible = false;
        btn20.Visible = false;
        btn21.Visible = false;

        count = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                array_btn2.Add(new Button());
                Button btn = (Button)array_btn2[count];
                btn.Parent = this;
                btn.Text = "";
                btn.Location = new Point(10 + j * 190, 40 + i * 110);
                btn.BackColor = SystemColors.Control;
                btn.Size = new Size(180, 100);
                btn.Click += new EventHandler(btn_Click);
                count++;
            }
        }
        
        btn_Switching3(false);
    }
    void btn_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        akstn = new string[] {"あいうえお", "かきくけこ", "さしすせそ", "たちつてと", "なにぬねの", "はひふへほ", "まみむめも", "や　ゆ　よ", "らりるれろ"};
        MakeTrans mt = new MakeTrans("あいうえおつやゆよかきくけこさしすせそたちってとはひふへほばびぶべぼぁぃぅぇぉゃゅょがぎぐげござじずぜぞだぢづでどぱぴぷぺぽ",
                                     "ぁぃぅぇぉっゃゅょがぎぐげござじずぜぞだぢづでどばびぶべぼぱぴぷぺぽあいうえおやゆよかきくけこさしすせそたちつてとはひふへほ");
        string[] kanji_array = new string[5];

        for (int i = 0; i < 9; i++) {
            if (btn == (Button)array_btn1[i])
            {
                //str = "ボタン1";
                btn_Switching(false);
                btn_Strset(akstn[i]);
                btn16.Focus();
            }
        }
        if (btn == (Button)array_btn1[9])//小,゛,゜
        {
            btn_st = mt.Translate(str.Substring(str.Length - 1));
            str = str.Remove(str.Length - 1);
        }
        else if (btn == (Button)array_btn1[10])
        {
            btn_Switching(false);
            btn_Strset("わをんー　");
            btn16.Focus();
        }
        else if (btn == (Button)array_btn1[11])//空白
        {
            btn_st = "　";
        }
        else if (btn == (Button)array_btn1[12])
        {//戻る
            str = str.Remove(str.Length - 1);
        }
        else if (btn == (Button)array_btn1[13])
        {//漢字

            kanji_array = Ext.ToKanji(str);
            btn16.Text = kanji_array[0];
            btn17.Text = kanji_array[1];
            btn18.Text = kanji_array[2];
            btn19.Text = kanji_array[3];
            btn20.Text = kanji_array[4];
            btn_Switching(false);
            str = RemoveRight(str,"　");
            kanji_check = true;
        }
        else if (btn == (Button)array_btn1[14]) { //検索
            get_Search(str,0);
            for (int i = 0; i< url_list.Length; i++) {
                Console.WriteLine(url_list[i]);
                Console.WriteLine(title_list[i]);
            }
            btn_Urlset();
            btn_Switching(false);
            btn_Switching2(false);
            btn_Switching3(true);

        }
        if (btn == btn16)
        {
            btn_st = btn16.Text;
            btn_Switching(true);
            if (kanji_check) {
                btn_st += "　";
                kanji_check = false;
            }
        }
        else if (btn == btn17)
        {
            btn_st = btn17.Text;
            btn_Switching(true);
            if (kanji_check)
            {
                btn_st += "　";
                kanji_check = false;
            }
        }
        else if (btn == btn18)
        {
            btn_st = btn18.Text;
            btn_Switching(true);
            if (kanji_check)
            {
                btn_st += "　";
                kanji_check = false;
            }
        }
        else if (btn == btn19)
        {
            btn_st = btn19.Text;
            btn_Switching(true);
            if (kanji_check)
            {
                btn_st += "　";
                kanji_check = false;
            }
        }
        else if (btn == btn20)
        {
            btn_st = btn20.Text;
            btn_Switching(true);
            if (kanji_check)
            {
                btn_st += "　";
                kanji_check = false;
            }
        }
        else if (btn == btn21) {
            btn_Switching(true);
        }
        for (int i = 0; i < 9; i++)
        {
            if (btn == (Button)array_btn2[i])
            {
                System.Diagnostics.Process.Start(url_list[i]);
            }
        }
        if (btn == (Button)array_btn2[14]) {
            btn_Switching(true);
            btn_Switching3(false);
        }
        
        //Console.WriteLine(btn_st);
        str += btn_st;
        btn_st = "";
        Invalidate();
    }

    void btn_Switching(bool b)
    {
        for (int i = 0; i < array_btn1.Count; i++) { 
            Button btn = (Button)array_btn1[i];
            btn.Visible = b;
        }
        btn_Switching2(b = (b) == false);
    }

    void btn_Switching2(bool b)
    {
        btn16.Visible = b;
        btn17.Visible = b;
        btn18.Visible = b;
        btn19.Visible = b;
        btn20.Visible = b;
        btn21.Visible = b;
    }

    void btn_Switching3(bool b) {
        for (int i = 0; i < array_btn2.Count; i++)
        {
            Button btn = (Button)array_btn2[i];
            btn.Visible = b;
        }
    }

    void btn_Strset(String s1)
    {
        btn16.Text = s1.Substring(0, 1);
        btn17.Text = s1.Substring(1, 1);
        btn18.Text = s1.Substring(2, 1);
        btn19.Text = s1.Substring(3, 1);
        btn20.Text = s1.Substring(4, 1);
    }

    void btn_Urlset()
    {
        for (int i = 0; i < array_btn2.Count-3; i++) {
            Button btn = (Button)array_btn2[i];
            btn.Font = new Font("", 14);
            btn.Text = title_list[i];
        }
        Button btn_Back = (Button)array_btn2[14];
        btn_Back.Text = "戻る";
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.DrawString(str, new Font("", 14),
            Brushes.Red, new PointF(20.0F, 10.0F));
    }
    void get_Search(string word,int n)
    {
        //HTML取得
        int page = 10 * n; //ページ数nのところを変更すると変わるよ
        string start = "&start=" + page.ToString();
        string url = "https://www.google.com/search?q=" + word + start;
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36");
        var t = client.GetStringAsync(url);
        string source = t.Result;

        //パース
        var parser = new HtmlParser();
        var doc = parser.ParseDocument(source); //これでHTMLの取得は終わり

        var a_tag = doc.QuerySelectorAll("a");

        int i = 0;
        foreach (var item in a_tag)
        {
            var h3_tag = item.QuerySelector("h3");
            string hoge = Convert.ToString(h3_tag);
            if (hoge.Length>0) {
                title_list[i] = h3_tag.TextContent.Trim();
                url_list[i] = item.GetAttribute("href");
                i++;
                //Console.WriteLine(h3_tag.TextContent.Trim());//タイトルを取得
                //Console.WriteLine(item.GetAttribute("href"));//urlを取得
            }
        }
    }
    // <param name="remstr">対象文字列</param>
    // <param name="removeStr">指定文字列</param>
    // <returns>対象文字列から指定文字列を削除した文字列</returns>
    public static string RemoveRight(string remstr, string removeStr)
    {
        var length = remstr.LastIndexOf(removeStr);
        if (length < 0)
        {
            return "";
        }

        return remstr.Substring(0, length+1);
    }
}
class Button03
{
    public static void Main()
    {
        Application.Run(new MyClass());
    }
}