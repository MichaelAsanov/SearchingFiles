using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.Diagnostics;
using ThreadState = System.Threading.ThreadState;

namespace FileSearching
{
    public partial class FileSearchingForm : Form
    {
        /// <summary>
        /// Поток для добавления файлов в treeView
        /// </summary>
        private Thread _thread;

      
        /// <summary>
        /// Таймер
        /// </summary>
        private Stopwatch _stopwatch;

        /// <summary>
        /// Делегат для добавления элементов в treeView в потоке
        /// </summary>
        /// <param name="str">Добавляемая строка</param>
        private delegate void TreeViewDelegate(string str);


        /// <summary>
        /// Делегат для изменения текста в label-е, показывающем прошедшее время добавления, в потоке
        /// </summary>       
        private delegate void TimeLabelDelegate();

        /// <summary>
        /// Делегат для изменения текста в textBox-е, показывающем обрабатываемый файл, в потоке
        /// </summary>   
        private delegate void CurrentHandlingFileTextBoxDelegate();

        /// <summary>
        /// Делегат для изменения текста в label-е, показывающем число обработанных файлов, в потоке
        /// </summary>   
        private delegate void NumberOfHandledFilesLabelDelegate();


        /// <summary>
        /// Делегат для запуска/остановки таймера в потоке
        /// </summary>
        private delegate void TimerDelegate();

        /// <summary>
        /// Стартовая директория
        /// </summary>
        private string StartDirectory //{ get; set; }
        {
            get { return startDirectoryTextBox.Text; }
            set
            {
                startDirectoryTextBox.Text = value;
                folderBrowserDialog1.SelectedPath = value;
            }
        }

        /// <summary>
        /// Шаблон имени файла
        /// </summary>
        private string FileNamePattern //{ get; set; }
        {
            get { return fileNameTemplateTextBox.Text; }
            set { fileNameTemplateTextBox.Text = value; }
        }

        /// <summary>
        /// Число обработанных файлов
        /// </summary>
        private int _numberOfHandledFiles;

        /// <summary>
        /// Текст в файле
        /// </summary>
        private string TextInFile //{ get; set; }
        {
            get { return textInFileTextBox.Text; }
            set { textInFileTextBox.Text = value; }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public FileSearchingForm()
        {
            InitializeComponent();                    
        }

      
        /// <summary>
        /// Получить критерии из XML
        /// </summary>
        private void LoadCriteriaFromXml()
        {
            var xmlCriteriaDocument = new XmlDocument();
            xmlCriteriaDocument.Load(Consts.SearchingCriteriaFileName);

            var criteriaConfigurationXmlNode = xmlCriteriaDocument.GetElementsByTagName(Consts.FileSearchConfigurationXmlNodeName)[0];

            StartDirectory = criteriaConfigurationXmlNode.Attributes[Consts.StartDirectoryXmlAttributeName].Value;
            FileNamePattern = criteriaConfigurationXmlNode.Attributes[Consts.FileNamePatternXmlAttributeName].Value;
            TextInFile = criteriaConfigurationXmlNode.Attributes[Consts.TextInFileXmlAttributeName].Value;
        }
      

        /// <summary>
        /// Записать критерии в XML для того, чтобы не потерять их при перезапуске программы
        /// </summary>
        private void WriteCriteriaToXml()
        {
            var xmlCriteriaDocument = new XmlDocument();
            xmlCriteriaDocument.Load(Consts.SearchingCriteriaFileName);

            var criteriaConfigurationXmlNode = xmlCriteriaDocument.GetElementsByTagName(Consts.FileSearchConfigurationXmlNodeName)[0];
            
            criteriaConfigurationXmlNode.Attributes[Consts.StartDirectoryXmlAttributeName].Value = StartDirectory;
            criteriaConfigurationXmlNode.Attributes[Consts.FileNamePatternXmlAttributeName].Value = FileNamePattern;
            criteriaConfigurationXmlNode.Attributes[Consts.TextInFileXmlAttributeName].Value = TextInFile;

            var textWriter = new XmlTextWriter(Consts.SearchingCriteriaFileName, null);
            textWriter.Formatting = Formatting.Indented;
            xmlCriteriaDocument.WriteContentTo(textWriter);
            textWriter.Close();
        }

       
        /// <summary>
        /// При закрытии формы и завершении работы приложения происходит запись критериев в XML-файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSearchingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                WriteCriteriaToXml();
            }
            catch{}
        }

        /// <summary>
        /// При загруке формы происходит считывание критериев поиска и XML-файла и вставка стартовой директории в TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSearchingForm_Load(object sender, EventArgs e)
        {            
            LoadCriteriaFromXml();            
        }

        /// <summary>
        /// Показать окно выбора директории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFolderBrowserDirectoryButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
        }

        public void SetFilesTreeView(string path)
        {
            //находим специальный шаблон для метода Directory.EnumerateFiles()
            var patternString = string.Format("*{0}*", FileNamePattern);

            //находим все поддиректории в заданной директории
            var directories = Directory.EnumerateDirectories(path, "*");

            //находим файлы с заданным шаблоном имени в данной директории и во всех поддиректориях
            var files = Directory.EnumerateFiles(path, patternString);

            foreach (var file in files)
            {
                try
                {
                    //если содержимое файла содержит заданный шаблон
                    if (file.ThereIsTextInFile(TextInFile))
                    {
                        //то добавляем его в treeView
                        AddPathNodeToTreeView(file);
                    }

                    //файл обработали
                    ++_numberOfHandledFiles;

                    //Выводим на форму прошедшее время обработки
                    TimeLabelDelegate timeLabelDelegate = () => { timeLabel.Text = _stopwatch.Elapsed.ToString(); };
                    this.Invoke(timeLabelDelegate);

                    //Выводим на форму текущий обрабатываемый файл
                    CurrentHandlingFileTextBoxDelegate currentHandlingFileTextBoxDelegate = () =>
                    {
                        currentHandlingFileTextBox.Text = file;

                        //если поток стоит, значит, ничего не обрабатываем
                        if (_thread.ThreadState == ThreadState.Stopped)
                            currentHandlingFileTextBox.Text = "";
                    };
                    this.Invoke(currentHandlingFileTextBoxDelegate);

                    //Выводим на форму общее число обработанных файлов
                    NumberOfHandledFilesLabelDelegate numberOfHandledFilesLabelDelegate = () =>
                    {
                        numberOfHandledFilesLabel.Text = _numberOfHandledFiles.ToString();
                    };
                    this.Invoke(numberOfHandledFilesLabelDelegate);

                    CurrentHandlingFileTextBoxDelegate currentHandlingFileTextBoxDelegate1 = () =>
                    {
                        currentHandlingFileTextBox.Text = "";
                    };
                    this.Invoke(currentHandlingFileTextBoxDelegate1);

                    //все файлы обработали, останавливаем таймер
                    TimerDelegate stopTimerDelegate = () => timer1.Enabled = false;
                    this.Invoke(stopTimerDelegate);
                }
                catch (UnauthorizedAccessException)
                {
                    continue;                    
                }
            }

            foreach (var directory in directories)
            {
                try
                {
                    SetFilesTreeView(directory);
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
            }
        }
      
        /// <summary>
        /// Приходит по всем файлам с заданным шаблоном имени, добавляет их в treeView и выводит данные о процессе 
        /// обработки файлов на форму
        /// </summary>
        public void SetFilesTreeView()
        {
            try
            {              
                SetFilesTreeView(StartDirectory);              
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Если произошло исключение, не забываем записать критерии в XML и сбросить информацию об обрабатываемом файле
                WriteCriteriaToXml();

                try
                {
                    CurrentHandlingFileTextBoxDelegate currentHandlingFileTextBoxDelegate1 = () =>
                    {
                        currentHandlingFileTextBox.Text = "";
                    };

                    this.Invoke(currentHandlingFileTextBoxDelegate1);
                }
                catch { }
            }
        }

        /// <summary>
        /// Поиск файлов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Устанавливаем стартовую директорию
                startDirectoryTextBox.Text = folderBrowserDialog1.SelectedPath;
                StartDirectory = startDirectoryTextBox.Text;
                FileNamePattern = fileNameTemplateTextBox.Text;
                TextInFile = textInFileTextBox.Text;
                
                //Обнуляем число обработанных файлов
                _numberOfHandledFiles = 0;

                //Очищаем treeView
                treeView1.Nodes.Clear();
                if (_thread != null && _thread.ThreadState != System.Threading.ThreadState.Aborted &&
                    _thread.ThreadState != System.Threading.ThreadState.Suspended)
                    _thread.Abort();

                //Инициализируем поток и таймер
                _thread = new Thread(new ThreadStart(SetFilesTreeView));
                _stopwatch = new Stopwatch();                

                //Начинаем поиск - запускаем поток и таймер
                _thread.Start();
                _stopwatch.Restart();
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                WriteCriteriaToXml();
            }
        }

        /// <summary>
        /// Данный метод отвечает за вставку элементов, соответствующим файлам и каталогам в нужное место TreeView
        /// </summary>
        /// <param name="path">Путь к файлу, подлежащий отображению в treeView</param>
        private void AddPathNodeToTreeView(string path)
        {
            //Находим все промежуточные каталоги на пути к файлу. Необходимо для того, чтобы при их тоже добавть в дерево,
            //если их там еще нет
            var allPathDirectories = path.Split('\\');

            string formPath = "";

            var parentDirectory = FileHelper.GetParentDirectory(path);

            //Узел в дереве, куда будем вставлять данный файл/каталог; нам его необходимо найти
            TreeNode node = null;

            TreeViewDelegate treeViewDelegate = delegate(string x)
            {
                if (node == null)
                    node = treeView1.TopNode;

                //Ищем в treeView элементы с заданным ключом; в качестве ключа назначаем полный путь к файлу/каталогу.
                //Сделано для того, чтобы удобно отыскивать нужный узел.
                if (treeView1.Nodes.Count > 0 && treeView1.Nodes.Find(parentDirectory, true).Any())
                    node = treeView1.Nodes.Find(parentDirectory, true).First();
            };

            this.Invoke(treeViewDelegate, "");

            foreach (var component in allPathDirectories)
            {  
                //Формируем полный путь к каждой промежуточной директории пути к заданному файлу
                formPath += component + "\\";

                //Ищем в treeView элементы с заданным ключом; в качестве ключа назначаем полный путь к файлу/каталогу.
                //Сделано для того, чтобы удобно отыскивать нужный узел.
                var nodes = treeView1.Nodes.Find(formPath, true);

                //если нашли 
                if (nodes.Any())
                    node = nodes.First();

                //если не нашли, то добавляем в дерево
                else
                {
                    if (this.treeView1.InvokeRequired)
                    {
                        TreeViewDelegate @delegate = AddPathNodeToTreeView;
                        this.Invoke(@delegate, path);
                    }
                    else
                    {
                        //если нашли узел, то добавляем путь к файлу/каталогу в колллекцию дочерних узлов найденного узла
                        if (node != null)
                        {
                            node.Nodes.Insert(node.Nodes.Count, formPath, component);
                            node = node.Nodes[node.Nodes.Count - 1];
                        }
                        //если узлы не нашли (их там нет), то добавляем в узел в коллекцию дочерних узлов самого treeView
                        else
                        {
                            treeView1.Nodes.Insert(treeView1.Nodes.Count, formPath, component);
                            node = treeView1.Nodes[treeView1.Nodes.Count - 1];
                        }
                    }
                }              
            }          
        }
          
        /// <summary>
        /// Пауза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void suspendButton_Click(object sender, EventArgs e)
        {
            //Поставить на паузу
            if (_thread.ThreadState ==  System.Threading.ThreadState.Running)
            {
                _thread.Suspend();
                _stopwatch.Stop();
                timer1.Enabled = false;
                suspendButton.Text = Consts.SuspendButtonTextToResume;
            }
           
            //Возобновить поиск
            else if(_thread.ThreadState == System.Threading.ThreadState.Suspended)
            {
                _thread.Resume();
                _stopwatch.Start();
                timer1.Enabled = true;
                suspendButton.Text = Consts.SuspendButtonTextToSuspend;
            }        
        }       

        private void startDirectoryTextBox_TextChanged(object sender, EventArgs e)
        {
            StartDirectory = startDirectoryTextBox.Text;
        }

        private void fileNameTemplateTextBox_TextChanged(object sender, EventArgs e)
        {
            FileNamePattern = fileNameTemplateTextBox.Text;
        }

        private void textInFileTextBox_TextChanged(object sender, EventArgs e)
        {
           TextInFile = textInFileTextBox.Text;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = _stopwatch.Elapsed.ToString();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        
    }
}
