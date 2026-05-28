using FinanceTracker.Data;
using FinanceTracker.Data.Repositories;
using Microsoft.Extensions.Configuration;
using FinanceTracker.Models;


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var databaseConnection = new DatabaseConnection(configuration
    .GetSection("Database")
    .GetSection("ConnectionString")
    .Value);
try
{
    var dataSource = databaseConnection.CreateDataSource();
    var NewCurrencyRepository = new CurrencyRepository(dataSource);
    await NewCurrencyRepository.DeleteAsync(1);
    var NewCurrency = new Currency
    {
        CurrencyCode = "USD",
        CurrencyName = "United States Dollar",
        Symbol = "$",
    };
    var addedCurrency = await NewCurrencyRepository.AddAsync(NewCurrency);
    Console.WriteLine($"Added Currency: {addedCurrency.Id} - {addedCurrency.CurrencyName} ({addedCurrency.CurrencyCode}) {addedCurrency.Symbol}) Created At: {addedCurrency.CreatedAt} Updated At: {addedCurrency.UpdatedAt}");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to connect to the database: {ex.Message}");
    return;
}
