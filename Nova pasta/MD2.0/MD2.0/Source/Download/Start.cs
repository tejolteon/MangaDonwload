namespace MD2._0.Source.Download
{
    public static class Start
    {
        public static string file;

        public static void Main(string url, string manga, bool navegador, int capitulo, int volume, string path, int volNumber)
        {
            if (navegador)
                Selenium.Union(url, manga, capitulo, volume, path, volNumber);
            //else
            //    ViaCrawler.StartProcess(url, manga, capitulo, volume, path, volNumber);
        }
    }
}
