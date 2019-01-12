﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace ListaLekow
{
    /// <summary>
    /// Interaction logic for LekWindow.xaml
    /// </summary>
    public partial class LekWindow : Window
    {

        MainWindow mwInstance = (MainWindow)Application.Current.MainWindow;
        Lek tempLek = new Lek("", "", 0, false);

        private ObservableCollection<Lek> lekiLista;

        public ObservableCollection<Lek> LekiLista
        {
            get
            {
                if (this.lekiLista == null)
                {
                    this.lekiLista = new ObservableCollection<Lek>();
                }
                return this.lekiLista;
            }
            set { this.lekiLista = value; }
        }

        public LekWindow()
        {
            InitializeComponent();
            this.DataContext = tempLek;
        }

        private void CancelBTN_Click(object sender, RoutedEventArgs e)
        {
            tempLek = null;
            this.Close();
        }

        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckForDuplicate(tempLek.NazwaLeku))
            {
                tempLek.ID = generateID(tempLek.NazwaLeku);
                this.lekiLista.Add(tempLek);
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxResult result = MessageBox.Show("Nowy lek : " + tempLek.NazwaLeku + " został dodany.", "Nowy Lek", buttons);
                mwInstance.LekiLista = this.LekiLista;
            }
            else
            {
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxResult result = MessageBox.Show("Lek : " + tempLek.NazwaLeku + " już znajduje się na liście.", "Nowy Lek", buttons);
            }
            this.Close();
        }

        private string generateID(string nazwa_leku)
        {
            return Guid.NewGuid().ToString("N");
        }

        private bool CheckForDuplicate(string nazwa_leku)
        {
            try
            {
                Lek lek = this.LekiLista.Single(x => x.NazwaLeku == nazwa_leku);
                return true;
            }
            catch (System.InvalidOperationException ex)
            {
                return false;
            }
        }
    }
}
