using System.Configuration;

namespace Lotca2ClassLib
{
    public static class About
    {
        public static string Doc = "Библиотека классов моделирования системы хищник - жертва";
        public static string Author = "Самаркин А.И.";
        public static string Affiliation = "Псковский государственный университет";
        public static DateTime Date = new DateTime(2023,3,24);

        private static string endl = Environment.NewLine;
        
        /// tostring
        public static new string ToString()
        { return Author+endl+Affiliation+endl+Doc+endl+Date.ToString(); }

    }
}