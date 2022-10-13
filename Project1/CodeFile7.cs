using System;
using System.Drawing;
using System.Windows.Forms;

class Event : Form
{
    public static void Main() { 
        Application.Run(new Event());
    }
    public Event() {
        Text = "aa";
        BackColor = SystemColors.Window;
        Size = new Size(400, 100);
        Paint += (object sender, PaintEventArgs e) => { 
            Graphics g = e.Graphics;
            Form f = (Form)sender;
            g.DrawString(f.Text, f.Font,SystemBrushes.WindowText, new PointF(0F, 0F));
        };
    }

}
