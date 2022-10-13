class Test {
    public static void Main() {
        string[] kanji = Ext.ToKanji("でんだい");
        for (int i = 0; i < kanji.Length; i++) { 
            System.Console.WriteLine(kanji[i]);
        }
    }
}