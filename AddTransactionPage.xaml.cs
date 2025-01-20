using PersonalBudgetGushin.DatabaseContext;
using PersonalBudgetGushin.Entities;

namespace PersonalBudgetGushin;

public partial class AddTransactionPage : ContentPage
{

    public AddTransactionPage()
    {
        InitializeComponent();
    }

    private void AddTransaction(object sender, EventArgs e)
    {
        string title = TitleEntry.Text;
        string description = DescriptionEntry.Text;
        string amount = AmountEntry.Text;

        bool isTitleEmpty = string.IsNullOrWhiteSpace(title);
        if (isTitleEmpty)
        {
            AppShell.Current.DisplayAlert("Ошибка", "Заголовок не может быть пустым", "ОК");
            return;
        }

        bool isDescriptionEmpty = string.IsNullOrWhiteSpace(description);
        if (isDescriptionEmpty)
        {
            AppShell.Current.DisplayAlert("Ошибка", "Описание траты не может быть пустым", "ОК");
            return;
        }

        bool isAmountEmpty = string.IsNullOrWhiteSpace(amount);
        if (isAmountEmpty)
        {
            AppShell.Current.DisplayAlert("Ошибка", "Количество денег не может быть пустым", "ОК");
            return;
        }

        decimal amountConvertedToDecimal = Convert.ToDecimal(amount);

        ApplicationDbContext dbContext = new ApplicationDbContext();
        dbContext.Transactions.Add(new TransactionEntity(title, description, amountConvertedToDecimal));
        dbContext.SaveChanges();

        AppShell.Current.DisplayAlert("Готово!", "Траты добавлены.", "ОК");
        AppShell.Current.GoToAsync("..", true);
    }
}