using System;
using System.Drawing;
using System.Windows.Forms;


class basicform
{
    public static void Main() {
        string title = "タイトル";
        Form  f = new Form();

        f.Text = title;
        f.Size = new Size(600, 200);

        Application.Run(f);
    }
}