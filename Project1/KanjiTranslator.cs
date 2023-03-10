using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;


public static class KanjiTranslator
{
    public static string[] kanji_array = new string[5];
    public static string GetResponseFrom(this string url)
    {
        WebRequest req = WebRequest.Create(url);

        using (WebResponse res = req.GetResponse())
        using (Stream stm = res.GetResponseStream())
        using (StreamReader sr = new StreamReader(stm, Encoding.GetEncoding("utf-8")))
        {
            return sr.ReadToEnd();
        }
    }

    public static string GetForConvWithGoogleTransliterate(this string hiragana)
    {
        StringBuilder url = new StringBuilder("http://www.google.com/transliterate?langpair=ja-Hira|ja&text=");
        url.Append(HttpUtility.UrlEncode(hiragana));

        return url.ToString().GetResponseFrom();
    }


    public static string[] ToKanji(this string hiragana)
    {
        StringBuilder kanji = new StringBuilder();
        JArray jar = JArray.Parse(hiragana.GetForConvWithGoogleTransliterate());
        kanji_array = new string[5];

        foreach (JToken jt in jar)
        {
            var convArray = jt[1].ToArray();
            int i = 0;
            foreach (JValue jv in convArray)
            {
                kanji_array[i] = jv.ToString();
                i++;
            }
        }

        return kanji_array;
    }
}