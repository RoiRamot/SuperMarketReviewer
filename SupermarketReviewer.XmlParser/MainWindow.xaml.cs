using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using SupermarketReviewer.Core.Models;
using SupermarketReviewer.XmlParser.ViewModels;

namespace SupermarketReviewer.XmlParser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            list=new List<Brand>();
            parser=new XParser();
        }

        public XParser parser ;

        private List<Brand> list { get; set; }
        private async void ParseButton_OnClick(object sender, RoutedEventArgs e)
        {
            BusyIndicator.IsBusy = true;
            await Task.Run(() =>
            {
                list = parser.XmlScanner();
            });
                ProductListBox.ItemsSource = list;
                BusyIndicator.IsBusy = false;
        }
        private async void UnzipButton_OnClick(object sender, RoutedEventArgs e)
        {
            var paths = Directory.GetFiles(@"C:\XmlFolder","*", SearchOption.AllDirectories);
         await Task.Run(() =>
            {
                foreach (var path in paths)
                {
                    var fileInfo = new FileInfo(path);
                    if (fileInfo.Name.Contains("PriceFull") || fileInfo.Name.Contains("Stores"))
                    {
                        Decompress(fileInfo);
                    }
                }

            });
            MessageBox.Show("Done Unziping");
        }
        public static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                if (fileToDecompress.Name.EndsWith(".gz"))
                {
                    string currentFileName = fileToDecompress.Name;
                    string newFileName = @"C:\XmlFolder\xml" + currentFileName + ".xml";

                    using (FileStream decompressedFileStream = File.Create(newFileName))
                    {
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                        }
                    }
                }
            }
        }
        private void DownloadButton_OnClick(object sender, RoutedEventArgs e)
        {
            var path = Environment.CurrentDirectory;
            var start = new ProcessStartInfo();
            start.Arguments = @"C:\XmlFolder\";
            start.FileName = @"C:\projects\קורס .net\SupremarketReviewer\SupermarketReviewer\SupermarketReviewer.XmlParser\3rdParty\pricesDownloader.exe";
            start.WindowStyle = ProcessWindowStyle.Normal;
            start.CreateNoWindow = true;


            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

            }
           // Process.Start(@"pack://application:,,,/3rdParty/pricesDownloader.exe", "pack://application:,,,/XmlFolder");
        }
        private void UploadToDB_Button_OnClick(object sender, RoutedEventArgs e)
        {
            
            var uploadToDb = parser.UploadToDb();
            if (uploadToDb.IsCompleted)
            {
                MessageBox.Show("finished uploading to database");
            }
        }

        private void SaveToFile_Button_OnClick(object sender, RoutedEventArgs e)
        {
            parser.SaveToFile(list);

        }

        private void ReadFromFile_Button_OnClick(object sender, RoutedEventArgs e)
        {
            var brands = parser.LoadFromFile();
            ProductListBox.ItemsSource = brands;
        }

        private void Normalize_Button_OnClick(object sender, RoutedEventArgs e)
        {
            var brands = parser.Normalize();
            ProductListBox.ItemsSource = brands;
        }
    }
}
