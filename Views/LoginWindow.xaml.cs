using Diary.Data;
using Diary.Helpers;
using Diary.Models;
using System.Linq;
using System.Windows;

namespace Diary.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.",
                                "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var db = new DiaryContext();
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                MessageBox.Show("No account found. Please register first.",
                                "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user.PasswordHash != PasswordHelper.HashPassword(password))
            {
                MessageBox.Show("Incorrect password. Try again.",
                                "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Success → open main diary
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            // 1. Basic validation
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username cannot be empty.", "Validation Error",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password cannot be empty.", "Validation Error",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Validation Error",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var db = new DiaryContext();

            // 2. Enforce only one user
            if (db.Users.Any())
            {
                MessageBox.Show("User already exists. Only one account is allowed.",
                                "Registration Blocked", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 3. Create the single user
            var user = new User
            {
                Username = username,
                PasswordHash = PasswordHelper.HashPassword(password)
            };

            db.Users.Add(user);
            db.SaveChanges();

            MessageBox.Show("User registered successfully! Please log in.",
                            "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}