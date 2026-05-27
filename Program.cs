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
    var transactionTypeRepository = new TransactionTypeRepository(dataSource);
    while (true)
    {
        Console.ReadLine();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to connect to the database: {ex.Message}");
    return;
}
