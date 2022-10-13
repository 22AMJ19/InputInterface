using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

class MyClass : Form {
    string str;
    string btn_st;
    ArrayList array = new ArrayList();
    Button btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10, btn11, btn12, btn13, btn14, btn15;//akstn
    Button btn16, btn17, btn18, btn19, btn20;//aiueo
    public MyClass() {
        this.StartPosition = FormStartPosition.Manual; //起動位置
        this.Location = new Point(0, 0); //起動位置
        Text = "タイトル";
        BackColor = SystemColors.Window;
        Size = new Size(600,630);
        Font = new Font("", 40); ;

        btn1 = new Button();
        btn1.Parent = this;
        btn1.Text = "あ";
        btn1.Location = new Point(10,40);
        btn1.BackColor = SystemColors.Control;
        btn1.Size = new Size(180, 100);
        btn1.Click += new EventHandler(btn_Click);

        btn2 = new Button();
        btn2.Parent = this;
        btn2.Text = "か";
        btn2.Location = new Point(200, 40);
        btn2.BackColor = SystemColors.Control;
        btn2.Size = new Size(180, 100);
        btn2.Click += new EventHandler(btn_Click);

        btn3 = new Button();
        btn3.Parent = this;
        btn3.Text = "さ";
        btn3.Location = new Point(390, 40);
        btn3.BackColor = SystemColors.Control;
        btn3.Size = new Size(180, 100);
        btn3.Click += new EventHandler(btn_Click);

        btn4 = new Button();
        btn4.Parent = this;
        btn4.Text = "た";
        btn4.Location = new Point(10, 150);
        btn4.BackColor = SystemColors.Control;
        btn4.Size = new Size(180, 100);
        btn4.Click += new EventHandler(btn_Click);

        btn5 = new Button();
        btn5.Parent = this;
        btn5.Text = "な";
        btn5.Location = new Point(200, 150);
        btn5.BackColor = SystemColors.Control;
        btn5.Size = new Size(180, 100);
        btn5.Click += new EventHandler(btn_Click);

        btn6 = new Button();
        btn6.Parent = this;
        btn6.Text = "は";
        btn6.Location = new Point(390, 150);
        btn6.BackColor = SystemColors.Control;
        btn6.Size = new Size(180, 100);
        btn6.Click += new EventHandler(btn_Click);

        btn7 = new Button();
        btn7.Parent = this;
        btn7.Text = "ま";
        btn7.Location = new Point(10, 260);
        btn7.BackColor = SystemColors.Control;
        btn7.Size = new Size(180, 100);
        btn7.Click += new EventHandler(btn_Click);

        btn8 = new Button();
        btn8.Parent = this;
        btn8.Text = "や";
        btn8.Location = new Point(200, 260);
        btn8.BackColor = SystemColors.Control;
        btn8.Size = new Size(180, 100);
        btn8.Click += new EventHandler(btn_Click);

        btn9 = new Button();
        btn9.Parent = this;
        btn9.Text = "ら";
        btn9.Location = new Point(390, 260);
        btn9.BackColor = SystemColors.Control;
        btn9.Size = new Size(180, 100);
        btn9.Click += new EventHandler(btn_Click);

        btn10 = new Button();
        btn10.Parent = this;
        btn10.Text = "小,゛,゜";
        btn10.Location = new Point(10, 370);
        btn10.BackColor = SystemColors.Control;
        btn10.Size = new Size(180, 100);
        btn10.Click += new EventHandler(btn_Click);

        btn11 = new Button();
        btn11.Parent = this;
        btn11.Text = "わ";
        btn11.Location = new Point(200, 370);
        btn11.BackColor = SystemColors.Control;
        btn11.Size = new Size(180, 100);
        btn11.Click += new EventHandler(btn_Click);

        btn12 = new Button();
        btn12.Parent = this;
        btn12.Text = "空白";
        btn12.Location = new Point(390, 370);
        btn12.BackColor = SystemColors.Control;
        btn12.Size = new Size(180, 100);
        btn12.Click += new EventHandler(btn_Click);

        btn13 = new Button();
        btn13.Parent = this;
        btn13.Text = "戻";
        btn13.Location = new Point(10, 480);
        btn13.BackColor = SystemColors.Control;
        btn13.Size = new Size(180, 100);
        btn13.Click += new EventHandler(btn_Click);

        btn14 = new Button();
        btn14.Parent = this;
        btn14.Text = "漢字";
        btn14.Location = new Point(200, 480);
        btn14.BackColor = SystemColors.Control;
        btn14.Size = new Size(180, 100);
        btn14.Click += new EventHandler(btn_Click);

        btn15 = new Button();
        btn15.Parent = this;
        btn15.Text = "検索";
        btn15.Location = new Point(390, 480);
        btn15.BackColor = SystemColors.Control;
        btn15.Size = new Size(180, 100);
        btn15.Click += new EventHandler(btn_Click);

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

        btn16.Visible = false;
        btn17.Visible = false;
        btn18.Visible = false;
        btn19.Visible = false;
        btn20.Visible = false;

    }
    void btn_Click(object sender, EventArgs e) {
        Button btn = (Button)sender;
        
        if (btn == btn1)
        {
            //str = "ボタン1";
            btn_Switching(false);
            btn_Strset("あいうえお");
            btn16.Focus();
        }
        else if (btn == btn2)
        {
            btn_Switching(false);
            btn_Strset("かきくけこ");
            btn16.Focus();
        }
        else if (btn == btn3)
        {
            str = "ボタン3";
        }
        else if (btn == btn4)
        {
            str = "ボタン4";
        } else if (btn == btn16){
            btn_st = btn16.Text;
            btn_Switching(true);
        }
        else if (btn == btn17)
        {
            btn_st = btn17.Text;
            btn_Switching(true);
        }
        else if (btn == btn18)
        {
            btn_st = btn18.Text;
            btn_Switching(true);
        }
        else if (btn == btn19)
        {
            btn_st = btn19.Text;
            btn_Switching(true);
        }
        else if (btn == btn20)
        {
            btn_st = btn20.Text;
            btn_Switching(true);
        }
        else {
            btn_st = "";
        }
        str += btn_st;
        Invalidate();
    }

    void btn_Switching(bool b) {
        btn1.Visible = b;
        btn2.Visible = b;
        btn3.Visible = b;
        btn4.Visible = b;
        btn5.Visible = b;
        btn6.Visible = b;
        btn7.Visible = b;
        btn8.Visible = b;
        btn9.Visible = b;
        btn10.Visible = b;
        btn11.Visible = b;
        btn12.Visible = b;
        btn13.Visible = b;
        btn14.Visible = b;
        btn15.Visible = b;
        btn_Switching2(b = (b) == false);
    }

    void btn_Switching2(bool b) {
        btn16.Visible = b;
        btn17.Visible = b;
        btn18.Visible = b;
        btn19.Visible = b;
        btn20.Visible = b;
    }

    void btn_Strset(String s1) { 
        btn16.Text = s1.Substring(0, 1);
        btn17.Text = s1.Substring(1, 1);
        btn18.Text = s1.Substring(2, 1);
        btn19.Text = s1.Substring(3, 1);
        btn20.Text = s1.Substring(4, 1);
    }

    protected override void OnPaint(PaintEventArgs e) { 
        Graphics g = e.Graphics;
        g.DrawString(str, new Font("", 14),
            Brushes.Red,new PointF(20.0F, 10.0F));
    }
}
class Button03 {
    public static void Main() {
        Application.Run(new MyClass());
    }
}