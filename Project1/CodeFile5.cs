using System;
using System.Drawing;
using System.Windows.Forms;


class basicform
{
    static char c;
    public static void Main()
    {
        Form f = new Form();
        f.Text = "タイトル";
        f.Size = new Size(600, 200);

        f.KeyDown += new KeyEventHandler(f_KeyDown);
        f.Paint += new PaintEventHandler(MyHandler);
        Application.Run(f);
    }
    static void MyHandler(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        Font font = new Font("MS ゴシック", 48);
        g.DrawString(c + "aaa", font, Brushes.Blue, new PointF(10F, 10F));
    }

    static void f_KeyDown(object sender, KeyEventArgs e)
    {
        c = Convert.ToChar(e.KeyValue);
        if (c < 'A' || c > 'Z')
            return;

        Form f = (Form)sender;
        
        f.Invalidate();
    }
}