using System;
using System.Drawing;
using System.Windows.Forms;

class MyClass : Form {
    public MyClass() {
        Text = "タイトル";
        BackColor = SystemColors.Window;
        Size = new Size(400,100);

        Paint += delegate (Object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("aa", ((Form)sender).Font,SystemBrushes.WindowText, new PointF(0F, 0F));
        };
    }
}
class Event1{
    public static void Main() { 
        MyClass f = new MyClass();
        f.Paint += delegate (Object sender, PaintEventArgs e) {
            e.Graphics.DrawString("bb", ((Form)sender).Font, Brushes.Black, new PointF(0F, 20F));
        };

        Application.Run(f);
    }
}