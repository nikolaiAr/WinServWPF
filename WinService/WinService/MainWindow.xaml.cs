using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceProcess;
using System.Collections;

namespace WinService
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ArrayList array = new ArrayList();

        string temp;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            ServiceController[] services;
            services = ServiceController.GetServices();

            foreach (ServiceController serv in services)
            {
                list.Items.Add(serv.DisplayName);
                string str = serv.ServiceName;
                array.Add(str);
            }
        }

        private void list_SelectionChanged(object sender, EventArgs e)
        {
            ServiceController[] services;
            services = ServiceController.GetServices();

            foreach (ServiceController serv in services)
            {
                string str = array[list.SelectedIndex].ToString();
                if (serv.ServiceName == str)
                {
                    temp = str;
                    text.Text = "Имя сервиса: " + serv.ServiceName + Environment.NewLine + "Состояние: " + serv.Status 
                        + Environment.NewLine + "Возможно остановить: " + serv.CanStop;

                    
                }
            }
        }

        private void butStop_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceController[] services;
                services = ServiceController.GetServices();

                foreach (ServiceController serv in services)
                {
                    if (serv.ServiceName == temp)
                    if (serv.Status.ToString()=="Running")
                    {
                        serv.Stop();
                        break;
                    }
                    list_SelectionChanged( sender, e);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void butStart_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceController[] services;
                services = ServiceController.GetServices();

                foreach (ServiceController serv in services)
                {
                    if (serv.ServiceName == temp)
                        if (serv.Status.ToString() == "Stopped")
                    {
                        serv.Start();
                        break;
                    }
                    list_SelectionChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
