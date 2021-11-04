using Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using Models.Migrations;
using System.Windows.Controls;

namespace DataAccessEF_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MarketSqlContext sqlDb;
        private MarketOleDbContext oleDb;

        public MainWindow()
        {
            InitializeComponent();
            sqlDb = new MarketSqlContext();
            oleDb = new MarketOleDbContext();
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MarketOleDbContext, OleDbMigrationConfiguration>(true));
        }

        private void buttonUsersFormFill_Click(object sender, RoutedEventArgs e)
        {
            sqlDb.Customers.Load();
            dataGridUsers.ItemsSource = sqlDb.Customers.Local;
        }

        private void buttonUsersFormInsert_Click(object sender, RoutedEventArgs e)
        {
            var customer = new Customer()
            {
                FirstName = textBoxUserFirstName.Text,
                LastName = textBoxUserLastName.Text,
                PhoneNumber = textBoxUserPhone.Text,
                Email = textBoxUserEmail.Text
            };

            sqlDb.Customers.Add(customer);
            sqlDb.SaveChanges();
        }

        private void buttonUsersFormDelete_Click(object sender, RoutedEventArgs e)
        {
            var customer = dataGridUsers.SelectedItem as Customer;
            sqlDb.Customers.Remove(customer);
            sqlDb.SaveChanges();
        }

        private void buttonProductsFormFill_Click(object sender, RoutedEventArgs e)
        {
            oleDb.Products.Load();
            dataGridProducts.ItemsSource = oleDb.Products.Local;
        }

        private void buttonProductsFormInsert_Click(object sender, RoutedEventArgs e)
        {
            var product = new Product()
            {
                EmailCustomer = textBoxCustomerEmail.Text,
                ProductName = textBoxProductName.Text,
                ProductCode = textBoxProductCode.Text
            };

            oleDb.Products.Add(product);
            oleDb.SaveChanges();
        }

        private void buttonProductsRowDelete_Click(object sender, RoutedEventArgs e)
        {
            var product = dataGridProducts.SelectedItem as Product;
            oleDb.Products.Remove(product);
            oleDb.SaveChanges();
        }

        private void buttonRelation_Click(object sender, EventArgs e)
        {
            var customers = sqlDb.Customers.ToList();

            var req = customers.
                Join(oleDb.Products,
                customer => customer.Email,
                product => product.EmailCustomer,
                (customer, product) => new
                {
                    product.ID,
                    product.ProductName,
                    product.ProductCode,
                    product.EmailCustomer,
                    CustomerFullName = $"{customer.FirstName} {customer.LastName}"
                });

            var relationWindow = new RelationWindow();
            relationWindow.dataGrid.ItemsSource = req.ToList();
            relationWindow.Owner = this;
            relationWindow.Show();
        }

        private void dataGridUsers_CurrentCellChanged(object sender, EventArgs e)
        {
            sqlDb.SaveChanges();
        }

        private void dataGridProducts_CurrentCellChanged(object sender, EventArgs e)
        {
            oleDb.SaveChanges();
        }
    }
}
