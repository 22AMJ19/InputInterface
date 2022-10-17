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
    enum StateTag
    { //今どのボタンに居るかの列挙型
        vowel,//母音あいうえお
        consonant,//子音あかさたな
        search//検索
    }
    string str, btn_st;//str:表示、検索する文字 btn_st:追加する文字
    string[] stArray = new string[] { "あ", "か", "さ", "た", "な", "は", "ま", "や", "ら", "小,゛,゜", "わ", "空白", "消", "漢字", "検索" };//consonant_btnsに表示する文字
    string[] akstn = new string[] { "あいうえお", "かきくけこ", "さしすせそ", "たちつてと", "なにぬねの", "はひふへほ", "まみむめも", "や　ゆ　よ", "らりるれろ" };//bowel_btnsに表示する文字
    MakeTrans mt = new MakeTrans("あいうえおつやゆよかきくけこさしすせそたちってとはひふへほばびぶべぼぁぃぅぇぉゃゅょがぎぐげござじずぜぞだぢづでどぱぴぷぺぽ",
                                 "ぁぃぅぇぉっゃゅょがぎぐげござじずぜぞだぢづでどばびぶべぼぱぴぷぺぽあいうえおやゆよかきくけこさしすせそたちつてとはひふへほ");//小゛゜で変換する文字
    string[] title_list, url_list;//ウェブページのタイトルとurlを入れる配列
    Point[] vowel_pos = { new Point(200, 160), new Point(10, 160), new Point(200, 50), new Point(390, 160), new Point(200, 270), new Point(10, 270) };//vowel_btnsを表示する場所
    ArrayList vowel_btns = new ArrayList();//各ボタンを入れるArrayList
    ArrayList consonant_btns = new ArrayList();
    ArrayList search_btns = new ArrayList();
    StateTag state_tag = StateTag.consonant;
    bool kanji_check = false;
    int row = 5, column = 3;
    public MyClass()//コンストラクタ
    {
        this.StartPosition = FormStartPosition.Manual; //起動位置
        this.Location = new Point(0, 0); //起動位置
        Text = "視線入力キーボード";
        BackColor = SystemColors.Window;
        Size = new Size(600, 630);
        Font = new Font("", 40);

        title_list = new string[12];
        url_list = new string[12];

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Button cbtn = new Button();
                consonant_btns.Add(cbtn);
                cbtn.Parent = this;
                cbtn.Text = stArray[i * column + j];
                cbtn.Location = new Point(10 + j * 190, 40 + i * 110);
                cbtn.BackColor = SystemColors.Control;
                cbtn.Size = new Size(180, 100);
                cbtn.Click += new EventHandler(btn_Click);

                Button sbtn = new Button();
                search_btns.Add(sbtn);
                sbtn.Parent = this;
                sbtn.Text = "";
                sbtn.Location = new Point(10 + j * 190, 40 + i * 110);
                sbtn.BackColor = SystemColors.Control;
                sbtn.Size = new Size(180, 100);
                sbtn.Click += new EventHandler(btn_Click);
                sbtn.Visible = false;
            }
        }
        ((Button)search_btns[14]).Text = "戻る";

        for (int i = 0; i < 6; i++)
        {
            Button btn = new Button();
            vowel_btns.Add(btn);
            btn.Parent = this;
            btn.Text = "";
            btn.Location = vowel_pos[i];
            btn.BackColor = SystemColors.Control;
            btn.Size = new Size(180, 100);
            btn.Click += new EventHandler(btn_Click);
            btn.Visible = false;
        }
        ((Button)vowel_btns[5]).Text = "戻る";

        this.ActiveControl = (Button)consonant_btns[4];//最初に「な」にフォーカス
        ((Button)consonant_btns[4]).BackColor = Color.Yellow;

        //state_tag = StateTag.consonant;//必要ないはずだけど一応
        //SetDefaultPos();//なぜか最初に「な」のフォーカスができない
    }

    protected override bool ProcessDialogKey(Keys keyData)//十字キーでのボタン操作
    {
        int currentPos = GetFocusedPos();
        int nextPos = currentPos;
        switch (state_tag)
        {
            case StateTag.vowel:
                switch (keyData)
                {
                    case Keys.Down:
                        switch (currentPos)
                        {
                            case 0: nextPos = 4; break;
                            case 1: nextPos = 5; break;
                            case 2: nextPos = 0; break;
                            default: break;
                        }
                        break;
                    case Keys.Right:
                        switch (currentPos)
                        {
                            case 0: nextPos = 3; break;
                            case 1: nextPos = 0; break;
                            case 5: nextPos = 4; break;
                            default: break;
                        }
                        break;
                    case Keys.Up:
                        switch (currentPos)
                        {
                            case 0: nextPos = 2; break;
                            case 4: nextPos = 0; break;
                            case 5: nextPos = 1; break;
                            default: break;
                        }
                        break;
                    case Keys.Left:
                        switch (currentPos)
                        {
                            case 0: nextPos = 1; break;
                            case 3: nextPos = 0; break;
                            case 4: nextPos = 5; break;
                            default: break;
                        }
                        break;
                    default:
                        //SetFocusedPos(currentPos);
                        return base.ProcessDialogKey(keyData);
                }
                break;
            case StateTag.consonant:
            case StateTag.search:
                switch (keyData)
                {
                    case Keys.Down:
                        if (currentPos < (row - 1) * column)
                        {
                            nextPos = currentPos + column;
                        }
                        Console.WriteLine(currentPos + "A" + nextPos);
                        break;
                    case Keys.Right:
                        if (currentPos % column != column - 1)
                        {
                            nextPos = currentPos + 1;
                        }
                        Console.WriteLine(currentPos + "B" + nextPos);
                        break;
                    case Keys.Up:
                        if (currentPos >= column)
                        {
                            nextPos = currentPos - column;
                        }
                        Console.WriteLine(currentPos + "C" + nextPos);
                        break;
                    case Keys.Left:
                        if (currentPos % column != 0)
                        {
                            nextPos = currentPos - 1;
                        }
                        Console.WriteLine(currentPos + "D" + nextPos);
                        break;
                    default:
                        //SetFocusedPos(currentPos);
                        return base.ProcessDialogKey(keyData);
                }
                break;
        }
        SetFocusedPos(nextPos);
        Console.Beep();//beep音を鳴らす
        return true;
    }

    int GetFocusedPos()
    {//今どのボタンにフォーカスしているかを取得する
        int pos = 0;
        switch (state_tag)
        {
            case StateTag.vowel:
                foreach (Button btn in vowel_btns)
                {
                    if (btn.Focused)
                    {
                        break;
                    }
                    pos++;
                }
                ((Button)vowel_btns[pos]).BackColor = SystemColors.Control;
                break;
            case StateTag.consonant:
                foreach (Button btn in consonant_btns)
                {
                    if (btn.Focused)
                    {
                        break;
                    }
                    pos++;
                }
                ((Button)consonant_btns[pos]).BackColor = SystemColors.Control;
                break;
            case StateTag.search:
                foreach (Button btn in search_btns)
                {
                    if (btn.Focused)
                    {
                        break;
                    }
                    pos++;
                }
                ((Button)search_btns[pos]).BackColor = SystemColors.Control;
                break;
        }
        return pos;
    }

    void SetFocusedPos(int pos)
    {//渡された番号のボタンをフォーカスし、色も付ける
        switch (state_tag)
        {
            case StateTag.vowel:
                ((Button)vowel_btns[pos]).Focus();
                ((Button)vowel_btns[pos]).BackColor = Color.Yellow;
                break;
            case StateTag.consonant:
                ((Button)consonant_btns[pos]).Focus();
                ((Button)consonant_btns[pos]).BackColor = Color.Yellow;
                break;
            case StateTag.search:
                ((Button)search_btns[pos]).Focus();
                ((Button)search_btns[pos]).BackColor = Color.Yellow;
                break;
        }
    }


    void btn_Click(object sender, EventArgs e)//ボタンをクリックした際の挙動
    {
        Button btn = (Button)sender;
        switch (state_tag)
        {
            case StateTag.vowel:
                for (int i = 0; i < 5; i++)
                {
                    if (btn == (Button)vowel_btns[i])
                    {
                        btn_st = ((Button)vowel_btns[i]).Text;
                        btn_Switching(StateTag.consonant);
                        if (kanji_check)
                        {
                            btn_st += "　";
                            kanji_check = false;
                        }
                    }
                }

                if (btn == (Button)vowel_btns[5])
                {
                    btn_Switching(StateTag.consonant);
                }
                break;
            case StateTag.consonant:
                for (int i = 0; i < 9; i++)
                {
                    if (btn == (Button)consonant_btns[i])
                    {
                        //str = "ボタン1";
                        btn_Switching(StateTag.vowel);
                        btn_Strset(akstn[i]);
                    }
                }
                if (btn == (Button)consonant_btns[9])//小,゛,゜
                {
                    if (str != null)
                    {
                        if (str.Length > 0)
                        {//(str != null || str.Length>0)ではだめ
                            Console.WriteLine("ほげほげ");
                            btn_st = mt.Translate(str.Substring(str.Length - 1));
                            str = str.Remove(str.Length - 1);
                        }
                    }
                    ((Button)consonant_btns[9]).BackColor = Color.Yellow;
                }
                else if (btn == (Button)consonant_btns[10])
                {
                    btn_Switching(StateTag.vowel);
                    btn_Strset("わをんー　");
                }
                else if (btn == (Button)consonant_btns[11])//空白
                {
                    btn_st = "　";
                    ((Button)consonant_btns[11]).BackColor = Color.Yellow;
                }
                else if (btn == (Button)consonant_btns[12])
                {//消
                    if (str.Length > 0)
                    {
                        str = str.Remove(str.Length - 1);
                    }
                    ((Button)consonant_btns[12]).BackColor = Color.Yellow;
                }
                else if (btn == (Button)consonant_btns[13])
                {//漢字
                    string[] kanji_array = new string[5];

                    kanji_array = Ext.ToKanji(str);
                    for (int i = 0; i < 5; i++)
                    {
                        ((Button)vowel_btns[i]).Text = kanji_array[i];
                    }
                    btn_Switching(StateTag.vowel);
                    str = RemoveRight(str, "　");
                    kanji_check = true;
                }
                else if (btn == (Button)consonant_btns[14])
                { //検索
                    get_Search(str, 0);
                    for (int i = 0; i < url_list.Length; i++)
                    {
                        Console.WriteLine(url_list[i]);
                        Console.WriteLine(title_list[i]);
                    }
                    btn_Urlset();
                    btn_Switching(StateTag.search);

                }
                break;
            case StateTag.search:
                for (int i = 0; i < 9; i++)
                {
                    if (btn == (Button)search_btns[i])
                    {
                        System.Diagnostics.Process.Start(url_list[i]);
                    }
                }
                if (btn == (Button)search_btns[14])
                {
                    btn_Switching(StateTag.consonant);
                }
                break;
        }

        //Console.WriteLine(btn_st);
        str += btn_st;
        btn_st = "";
        Console.Beep();//beep音を鳴らす
        Invalidate();
    }

    void btn_Switching(StateTag tag)//ボタン表示の切り替え
    {
        switch (tag)
        {
            case StateTag.vowel:
                foreach (Button btn in vowel_btns) { btn.Visible = true; }
                foreach (Button btn in consonant_btns) { btn.Visible = false; }
                foreach (Button btn in search_btns) { btn.Visible = false; }
                break;
            case StateTag.consonant:
                foreach (Button btn in vowel_btns) { btn.Visible = false; }
                foreach (Button btn in consonant_btns) { btn.Visible = true; }
                foreach (Button btn in search_btns) { btn.Visible = false; }
                break;
            case StateTag.search:
                foreach (Button btn in vowel_btns) { btn.Visible = false; }
                foreach (Button btn in consonant_btns) { btn.Visible = false; }
                foreach (Button btn in search_btns) { btn.Visible = true; }
                break;
        }
        state_tag = tag;
        SetDefaultPos();
    }
    void SetDefaultPos()
    {//デフォルトでフォーカスするボタン
        switch (state_tag)
        {
            case StateTag.vowel:
                ((Button)vowel_btns[0]).Focus();
                ((Button)vowel_btns[0]).BackColor = Color.Yellow;
                break;
            case StateTag.consonant:
                ((Button)consonant_btns[4]).Focus();
                ((Button)consonant_btns[4]).BackColor = Color.Yellow;
                break;
            case StateTag.search:
                ((Button)search_btns[0]).Focus();
                ((Button)search_btns[0]).BackColor = Color.Yellow;
                break;
        }
    }

    void btn_Strset(String s1)//ボタンに母音をセットする
    {
        for (int i = 0; i < 5; i++)
        {
            ((Button)vowel_btns[i]).Text = s1.Substring(i, 1);
        }
    }

    void btn_Urlset()//検索画面にタイトルとurlをセットする
    {
        for (int i = 0; i < search_btns.Count - 3; i++)
        {
            Button btn = (Button)search_btns[i];
            btn.Font = new Font("", 14);
            btn.Text = title_list[i];
        }

    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.DrawString(str, new Font("", 14),
            Brushes.Red, new PointF(20.0F, 10.0F));
    }
    void get_Search(string word, int n)
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
            if (hoge.Length > 0)
            {
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

        return remstr.Substring(0, length + 1);
    }

}
class Button03
{
    public static void Main()
    {
        Application.Run(new MyClass());
    }
}