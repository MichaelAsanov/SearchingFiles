using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearching
{
    /// <summary>
    /// Содержит вспомогательные методы по работе с файлами
    /// </summary>
    public static class FileHelper
    {   
        /// <summary>
        /// Возвращает для заданного файла/каталога родительскую директорию
        /// </summary>
        /// <param name="path">Путь к файлу/каталогу</param>
        /// <returns>Путь к родительскому каталогу</returns>
        public static string GetParentDirectory(string path)
        {
            var directory = "";
            var components = path.Split('\\');
            for(int i = 0; i<components.Length-1; ++i)
            {
                directory += components[i] + "\\";  
            }
            return directory;
        }

        /// <summary>
        /// Проверяет, есть ли в файле с заданным путем заданная последовательность символов
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="textPattern">Последовательность символов</param>
        /// <returns>Признак, содержит ли файл заданную последовательность символов</returns>
        public static bool ThereIsTextInFile(this string path, string textPattern)
        {
            try
            {
                using (var reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        var str = reader.ReadLine().ToLower();

                        //регистр при поиске не учитываем
                        if (str.Contains(textPattern.ToLower()))
                            return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
