using System.Text;
using System.IO;

class TextTest
{
    static void Main(string[] args)
    {
        //Pass the filepath and filename to the StreamWriter Constructor
        StreamWriter sw = new StreamWriter("C:\\Test.txt");
        //Write a line of text
        sw.WriteLine("Hello World!!");
        //Write a second line of text
        //Close the file
        sw.Close();
    }
}