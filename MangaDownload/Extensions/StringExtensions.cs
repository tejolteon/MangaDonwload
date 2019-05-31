using System.Linq;

namespace System
{
    public static class IStringExtensions
    {
        public static string NormalizeText(this string source)
        {
            string[] text = source.Split(new char[] { '\\', '/', ':', '*', '?', '"', '<', '>', '|', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string firstElem = text.First();
            string restOfArray = string.Join(" ", text.Skip(1));
            return firstElem + restOfArray;
        }

        public static string NormalizeFirstText(this string source)
        {
            string[] text = source.Split(new char[] { '.', '?', '!', ';', ':', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string firstElem = text.First();
            return firstElem;
        }

        public static string RemoveSpecialChar(this string source)
        {
            string[] text = source.Split(new char[] { '\\', '/', ':', '*', '?', '"', '<', '>', '|', '.', '?', '!', ';', ':', '~', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string firstElem = text.First();
            string restOfArray = string.Join(" ", text.Skip(1));
            return firstElem + restOfArray;
        }
    }
}
