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
using System.Net.Mail;
using System.Net;
using System.Security;

namespace Test_MailSender
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

        private void OnSendButtonClick(object sender, RoutedEventArgs e)
        {
            var message_subject = $"Тестовое сообщение от {DateTime.Now}";

            var message_body = $"Тело сообщения от {DateTime.Now}";

            const string from = "vs.kozh@list.ru";
            const string to = "stroyuni@gmail.com";


            try
            {
                using(var message = new MailMessage(from, to))
                {
                    message.Subject = message_subject;
                    message.Body = message_body;

                    const string server_adress = "smtp.mail.ru";
                    const int server_port = 587; //25

                    using(var client = new SmtpClient(server_adress, server_port))
                    {
                        client.EnableSsl = true;
                        var user_name = UserNameEdit.Text;

                        SecureString user_password = PasswordEdit.SecurePassword;

                        client.Credentials = new NetworkCredential(user_name, user_password);

                        client.Send(message);

                        MessageBox.Show("Почта благополучно отправлена!","Хвала богам!", MessageBoxButton.OK, MessageBoxImage.Information);

                    }




                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        
    }
}
