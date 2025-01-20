using System;
using Microsoft.EntityFrameworkCore;
using PersonalBudgetGushin.DatabaseContext;


namespace PersonalBudgetGushin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            RefreshCollectionView();
            DisplayTotalAmount(); 
        }

        private void GoToAddTransactionPage(object sender, EventArgs e)
        {
            AppShell.Current.GoToAsync(nameof(AddTransactionPage), true);
        }

        private void RefreshData(object sender, EventArgs e)
        {
            RefreshCollectionView();
            DisplayTotalAmount(); 
            RefreshV.IsRefreshing = false;
        }

        private void RefreshCollectionView()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                TransactionCL.ItemsSource = dbContext.Transactions.ToList();
            }
        }

        private void DisplayTotalAmount()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                
                var transactions = dbContext.Transactions.ToList();

                
                decimal totalAmount = transactions.Sum(t => t.Amount);

                int transactionCount = transactions.Count();

                TransactionCountLabel.Text = $"Количество затрат: {transactionCount}";
                TotalAmountLabel.Text = $"Сумма всех затрат: {totalAmount}";
            }
        }
    }

   
}
