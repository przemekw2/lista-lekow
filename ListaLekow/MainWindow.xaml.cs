﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.ComponentModel;

namespace ListaLekow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Lek tempLek;
        private DaneOsobowe daneOsobowe;
        private List<Lek> lekiLista;

        public Lek TempLek { get; set; }

        public DaneOsobowe DaneOsobowe
        {
            get
            {
                if (this.daneOsobowe == null)
                {
                    this.daneOsobowe = new DaneOsobowe("", "", "", "");
                }
                return this.daneOsobowe;
            }
            set { this.daneOsobowe = value; }
        }

        //public DaneOsobowe DaneOsobowe { get; set; }

        public List<Lek> LekiLista
        {
            get
            {
                if (this.lekiLista == null)
                {
                    this.lekiLista = new List<Lek>();
                }
                return this.lekiLista;
            }
            set { this.lekiLista = value; }
        }

        public MainWindow()
        {

            InitializeComponent();
            //if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;
            this.Closed += new EventHandler(MainWindow_Closed);
            SetGlobals();
            //ReadSettingsDATFile();
            ReadDataFile();
            this.DataContext = LekiLista;
            this.dataGrid1.AutoGeneratedColumns += new EventHandler(dataGrid1_AutoGeneratedColumns);
            this.dataGrid1.ItemsSource = LekiLista;
        }

        //ukrywanie kolumny ID
        private void dataGrid1_AutoGeneratedColumns(object sender, EventArgs e)
        {
            foreach (DataGridColumn item in this.dataGrid1.Columns)
            {
                if (item.Header.ToString() == "ID") this.dataGrid1.Columns.Remove(item);
                break;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //zamkniecie aplikacji
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //zamkniecie aplikacji toolbar button
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //otwarcie okna do dodawania nowego leku
            LekWindow lekWindow = new LekWindow();
            lekWindow.LekiLista = this.LekiLista;
            lekWindow.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //usuniecie leku
            Lek selectedItem = (Lek)dataGrid1.SelectedItem;

            if (null == selectedItem) return;
            try
            {
                
                MessageBoxResult result = MessageBox.Show("Czy chcesz usunąć z listy lek : " + selectedItem.NazwaLeku.ToString() + " ?",
                                          "Usunięcie leku",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    //kasowanie leku z listy
                    LekiLista.RemoveAll(x => x.NazwaLeku == selectedItem.NazwaLeku.ToString());
                    //datagrid refresh
                    this.dataGrid1.ItemsSource = null;
                    this.dataGrid1.ItemsSource = LekiLista;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //edycja leku
            Lek selectedItem = (Lek)dataGrid1.SelectedItem;
            if (null == selectedItem) return;

            LekEdycjaWindow lekEdycjaWindow = new LekEdycjaWindow(selectedItem, this.LekiLista);
            //lekEdycjaWindow.TempLek = selectedItem;
            lekEdycjaWindow.ShowDialog();

        }

        private void SetGlobals()
        {
            //definicja filepath do executable 
            Globals.ApplicationDirPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + System.IO.Path.DirectorySeparatorChar;
            //nazwa pliku z ustawieniami
            //Globals.SettingsFileName = "Settings.dat";
            //nazwa pliku z lista lekow
            Globals.DataFileName = "ListaLekow.dat";
            //nazwa pliku z dawkowaniem
            Globals.DawkowanieFilename = "Dawkowanie.pdf";
            //nazwa pliku z lista lekow
            Globals.ListaLekowFilename = "ListaLekow.pdf";
        }

        //private void ReadSettingsDATFile()
        //{
        //    if (File.Exists(Globals.ApplicationDirPath + Globals.SettingsFileName))
        //    {
        //        BinaryFormatter formatter = new BinaryFormatter();
        //        using (Stream input = File.OpenRead(Globals.ApplicationDirPath + Globals.SettingsFileName))
        //        {
        //            SettingVariables settingvariables = (SettingVariables)formatter.Deserialize(input);
        //            Globals.UseDefaultDataFileLocation = settingvariables.UseDefaultDataLocation;
        //            Globals.DataFileLocation = settingvariables.DataFileLocation;
        //        }
        //    }
        //    else
        //    {
        //        Globals.UseDefaultDataFileLocation = true;
        //        Globals.DataFileLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + System.IO.Path.DirectorySeparatorChar;
        //    }

        //}

        private void ReadDataFile()
        {

            if (File.Exists(Globals.ApplicationDirPath + Globals.DataFileName))
            {

                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (Stream input = File.OpenRead(Globals.ApplicationDirPath + Globals.DataFileName))
                    {
                        DataVariables dataVariables = (DataVariables)formatter.Deserialize(input);
                        LekiLista = dataVariables.LekiLista;
                        DaneOsobowe.Imie = dataVariables.Imie;
                        DaneOsobowe.Nazwisko = dataVariables.Nazwisko;
                        DaneOsobowe.Pesel = dataVariables.Pesel;
                        DaneOsobowe.Adres = dataVariables.Adres;
                    }
                }
                catch(Exception ex)
                {
                    LekiLista = null;
                }
                
            }

        }


        private void WriteDataFile()
        {
            DataVariables dataVariables = new DataVariables(LekiLista, DaneOsobowe.Imie, DaneOsobowe.Nazwisko, DaneOsobowe.Pesel, DaneOsobowe.Adres);
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream output = File.Create(Globals.ApplicationDirPath + Globals.DataFileName))
            {
                formatter.Serialize(output, dataVariables);
            }
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            //WriteSettingsDATFile();
            WriteDataFile();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //okno ustawien
            //SettingsWindow settingWindow = new SettingsWindow();
            //settingWindow.ShowDialog();
        }

        private void Dane_osoboweBTN_Click(object sender, RoutedEventArgs e)
        {
            DaneOsoboweWindow daneOsoboweWindow = new DaneOsoboweWindow(DaneOsobowe);
            daneOsoboweWindow.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //Dawkowanie PDF
            string filePath = Globals.ApplicationDirPath + Globals.DawkowanieFilename;
            //Lista Lekow PDF
            GenerujDawkowaniePDF(filePath, LekiLista);
            OpenPDF(filePath);
        }

        private void ListaLekowBTN_Click(object sender, RoutedEventArgs e)
        {
            string filePath = Globals.ApplicationDirPath + Globals.ListaLekowFilename;
            //Lista Lekow PDF
            GenerujListeLekowPDF(filePath, DaneOsobowe, LekiLista);
            OpenPDF(filePath);
        }

        static void GenerujDawkowaniePDF(string filePath, List<Lek> lekiLista)
        {
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                Document document = new Document(PageSize.A4, 30, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();
                BaseFont bf = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\ARIALUNI.TTF", BaseFont.IDENTITY_H, true);
                Font NormalFont = new iTextSharp.text.Font(bf, 12, Font.NORMAL);
                PdfPTable table = new PdfPTable(5);
                table.TotalWidth = 430f;
                table.LockedWidth = true;
                float[] widths = new float[] { 150f, 70f, 70f, 70f, 70f };
                table.SetWidths(widths);
                table.SpacingBefore = 100f;
                PdfPCell cell = new PdfPCell();
                cell.Border = 0;
                table.AddCell(cell);
                table.AddCell(new PdfPCell(new Phrase("Rano", NormalFont)));
                table.AddCell(new PdfPCell(new Phrase("Południe", NormalFont)));
                table.AddCell(new PdfPCell(new Phrase("Popołudnie", NormalFont)));
                table.AddCell(new PdfPCell(new Phrase("Wieczór", NormalFont)));

                foreach (Lek lekitem in lekiLista)
                {
                    string nazwaLeku = lekitem.NazwaLeku;
                    char separator = (char)'\u0020';
                    if (nazwaLeku.Contains(" "))
                    {
                        string[] words = nazwaLeku.Split(separator);
                        nazwaLeku = words[0];
                    }
                    table.AddCell(new PdfPCell(new Phrase(nazwaLeku, NormalFont)));
                    table.AddCell(lekitem.DawkowanieRano.ToString());
                    table.AddCell(lekitem.DawkowaniePoludnie.ToString());
                    table.AddCell(lekitem.DawkowaniePopoludnie.ToString());
                    table.AddCell(lekitem.DawkowanieWieczor.ToString());
                }
                Paragraph para = new Paragraph(" ");
                document.Add(para);
                document.Add(table);

                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                if (File.Exists(filePath)) File.Delete(filePath);
                File.WriteAllBytes(filePath, bytes);
            }
        }

        static void GenerujListeLekowPDF(string filePath, DaneOsobowe daneOsobowe, List<Lek> lekiLista)
        {
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                Document document = new Document(PageSize.A4, 30, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                Paragraph para = new Paragraph(" ");
                document.Add(para);
                document.Add(para);
                document.Add(para);

                BaseFont bf = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\ARIALUNI.TTF", BaseFont.IDENTITY_H, true);
                Font NormalFont = new iTextSharp.text.Font(bf, 12, Font.NORMAL);

                //Imie i Nazwisko
                string paraTxt = daneOsobowe.Imie + " " + daneOsobowe.Nazwisko;
                para = new Paragraph(paraTxt, NormalFont);
                document.Add(para);

                //Nr PESEL
                paraTxt = "PESEL : " + daneOsobowe.Pesel;
                para = new Paragraph(paraTxt, NormalFont);
                document.Add(para);

                //Adres
                paraTxt = daneOsobowe.Adres;
                para = new Paragraph(paraTxt, NormalFont);
                document.Add(para);

                PdfPTable table = new PdfPTable(2);
                float[] widths = new float[] { 3f, 1f };
                table.SetWidths(widths);
                table.TotalWidth = 350f;
                table.LockedWidth = true;
                table.HorizontalAlignment = 0;
                table.SpacingBefore = 50f;
                table.AddCell(new PdfPCell(new Phrase("Nazwa Leku", NormalFont)));
                table.AddCell(new PdfPCell(new Phrase("Ilość", NormalFont)));

                foreach (Lek lekitem in lekiLista)
                {
                    if (lekitem.Wydruk)
                    {
                        table.AddCell(new PdfPCell(new Phrase(lekitem.NazwaLeku, NormalFont)));
                        table.AddCell(new PdfPCell(new Phrase(lekitem.Ilosc.ToString() + " op.", NormalFont)));
                    }
                }

                document.Add(table);

                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                //string filePath = @"C:\Users\M609090\PDF\ListaLekow.pdf";
                if (File.Exists(filePath)) File.Delete(filePath);
                File.WriteAllBytes(filePath, bytes);
            }
        }

        static void OpenPDF(string filePath)
        {
            if (File.Exists(filePath))
            {
                //proba odpalenia w adobe readerze
                try
                {
                    Process adobeProcess = new Process();
                    adobeProcess.StartInfo.FileName = "acroRd32.exe"; //not the full application path
                    adobeProcess.StartInfo.Arguments = "/A \"zoom = 100\" " + "\"" + filePath + "\"";
                    adobeProcess.Start();
                }
                catch { Process.Start(filePath); }

            }
        }
    }
}
