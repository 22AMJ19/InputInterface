using System;
using System.Drawing;
using System.Windows.Forms;


class basicform
{
    static int n;
    public static void Main()
    {
        Form f = new Form();
        f.Text = "タイトル";
        f.Size = new Size(600, 200);

        f.Click += new EventHandler(f_Click);
        f.Paint += new PaintEventHandler(MyHandler);
        Application.Run(f);
    }
    static void MyHandler(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        Font font = new Font("MS ゴシック", 48);
        g.DrawString(n + "aaa", font, Brushes.Blue, new PointF(10F, 10F));
    }

    static void f_Click(object sender, EventArgs e)
    {
        Form f = (Form)sender;
        n++;
        f.Invalidate();
    }
}