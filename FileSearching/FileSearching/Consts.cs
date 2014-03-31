using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearching
{
    /// <summary>
    /// Глобальные константы
    /// </summary>
    public static class Consts
    {
        /// <summary>
        /// XML-файл критериев поиска
        /// </summary>
        public const string SearchingCriteriaFileName = "SearchingCriteria.xml";


        /// <summary>
        /// Имя узла в Xml-файле критериев
        /// </summary>
        public const string FileSearchConfigurationXmlNodeName = "FileSearchConfiguration";


        /// <summary>
        /// Имя xml-атрибута стартовой директории
        /// </summary>
        public const string StartDirectoryXmlAttributeName = "StartDirectory";


        /// <summary>
        /// Имя xml-атрибута шаблона имени файла
        /// </summary>
        public const string FileNamePatternXmlAttributeName = "FileNamePattern";


        /// <summary>
        /// Имя xml-атрибута текста в файле
        /// </summary>
        public const string TextInFileXmlAttributeName = "TextInFile";


        /// <summary>
        /// "Возобновить"
        /// </summary>
        public const string SuspendButtonTextToResume = @"Возобновить";


        /// <summary>
        /// "Приостановить"
        /// </summary>
        public const string SuspendButtonTextToSuspend = @"Приостановить";
    }
}
