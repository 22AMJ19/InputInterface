using System;
using System.Drawing;
using System.Windows.Forms;


class OnPaint01 : Form {
    public static void Main() { 
        OnPaint01 f = new OnPaint01();
        Application.Run(f);
    }
    protected override void OnPaint(PaintEventArgs e) { 
        base.OnPaint(e);
        Graphics g = e.Graphics;
        Font font = new Font("MS ゴシック",48);
        g.DrawString("aaa",font, Brushes.Blue, new PointF(10.0F, 10.0F));
    }
    public OnPaint01() {
        Text = "タイトル";
        Width = 440;
        Height = 120;
        BackColor = SystemColors.Window;
    }
}