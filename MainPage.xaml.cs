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
            DisplayTotalAmount(); // Вызываем метод для отображения суммы
        }

        private void GoToAddTransactionPage(object sender, EventArgs e)
        {
            AppShell.Current.GoToAsync(nameof(AddTransactionPage), true);
        }

        private void RefreshData(object sender, EventArgs e)
        {
            RefreshCollectionView();
            DisplayTotalAmount(); // Обновляем сумму при обновлении данных
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
                // Получаем все транзакции из базы данных
                var transactions = dbContext.Transactions.ToList();

                // Вычисляем сумму на стороне клиента
                decimal totalAmount = transactions.Sum(t => t.Amount);

                int transactionCount = transactions.Count();

                TransactionCountLabel.Text = $"Количество затрат: {transactionCount}";
                TotalAmountLabel.Text = $"Сумма всех затрат: {totalAmount}";
            }
        }
    }

   
}
