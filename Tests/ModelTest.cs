using Api.Database;
using Api.Model;
using Api.Model.People;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Api.Tests;

public class ModelTest()
{
    private static DbContextOptions<AppDbContext> GetDbContextOptions()
    {
        return new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=root;Database=mydatabase")
            .Options;
    }

    private static void TruncateTables(AppDbContext context)
    {
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"Families\" CASCADE");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"AthleteInfo\" CASCADE");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"CustomerFinancialInfo\" CASCADE");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"FinancialInfo\" CASCADE");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"CourseSchedules\" CASCADE");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"Admins\" CASCADE");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"Courses\" CASCADE");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"Facilities\" CASCADE");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"Coaches\" CASCADE");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"Customers\" CASCADE");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"IndividualAccounts\" CASCADE");
    }

    private static List<Customer> GenerateCustomers(int count, List<Family>? familiesForRandPick, List<Customer.RolesEnum>? rolesForRandPick)
    {

        if (familiesForRandPick != null && rolesForRandPick != null)
        {
            return new Faker<Customer>()
            .CustomInstantiator(f =>
                new Customer(
                    f.Person.FullName,
                    f.Internet.Email(),
                    f.Internet.Password(),
                    f.Phone.PhoneNumber("##########"),
                    familiesForRandPick[new Random().Next(familiesForRandPick.Count)].Id,
                    rolesForRandPick[new Random().Next(rolesForRandPick.Count)]
                )).Generate(count);
        }

        return new Faker<Customer>()
            .CustomInstantiator(f =>
                new Customer(
                    f.Person.FullName,
                    f.Internet.Email(),
                    f.Internet.Password(),
                    f.Phone.PhoneNumber("##########")
                )).Generate(count);
    }

    private static async Task<List<Family>> CreateFamilies(AppDbContext context, int amt)
    {
        List<Family> families = [];

        for (var i = 0; i < amt; i++)
        {
            families.Add(new Family());
        }

        context.Families.AddRange(families);
        await context.SaveChangesAsync();

        return families;
    }

    private static List<Coach> GenerateCoaches(int amt)
    {
        return new Faker<Coach>()
            .CustomInstantiator(f =>
                new Coach(
                    f.Person.FullName,
                    f.Internet.Email(),
                    f.Internet.Password(),
                    f.Phone.PhoneNumber("##########")
                )).Generate(amt);
    }

    [Fact]

    public async Task AddAdmin_ShouldCreateAdminInDatabase()
    {
        var options = GetDbContextOptions();

        var admins = new Faker<Admin>()
            .CustomInstantiator(f =>
                new Admin(
                    f.Person.FullName,
                    f.Internet.Email(),
                    f.Internet.Password(),
                    f.Phone.PhoneNumber("##########")
                ))
            .Generate(5);

        await using (var context = new AppDbContext(options))
        {
            TruncateTables(context);

            await context.Admins.AddRangeAsync(admins); // Add the generated Admins to the context
            await context.SaveChangesAsync(); // Save changes to the database

            var adminCount = await context.IndividualAccounts.CountAsync(); // Count the number of Admins
            Assert.Equal(5, adminCount); // Ensure there are exactly 5 Admins
        }
    }

    [Fact]
    public async Task AddCustomer_ShouldCreateCustomerInDatabase_No_Family()
    {
        var options = GetDbContextOptions();

        var customers = GenerateCustomers(5, null, null);

        await using (var context = new AppDbContext(options))
        {
            TruncateTables(context);

            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync(); // Save changes to the database

            var customerCount = await context.Customers.CountAsync(); // Count the number of customers
            var accountsCount = await context.IndividualAccounts.CountAsync(); // Count the number of customers

            Assert.Equal(5, customerCount); // Ensure there are exactly 5 Customers
            Assert.Equal(5, accountsCount); // Ensure there are exactly 5 Customers
        }
    }

    [Fact]
    public async Task AddFamily_ShouldCreateFamilyInDatabase()
    {
        var options = GetDbContextOptions();

        await using (var context = new AppDbContext(options))
        {
            TruncateTables(context);

            await CreateFamilies(context, 5);

            var familiesCount = await context.Families.CountAsync(); // Count the number of families

            Assert.Equal(5, familiesCount); // Ensure there are exactly 5 families
        }
    }

    [Fact]
    public async Task AddCustomers_With_Family_ShouldCreate_Customers_And_Family_And_Accounts_InDatabase()
    {
        var options = GetDbContextOptions();

        await using (var context = new AppDbContext(options))
        {
            TruncateTables(context);

            List<Family> families = await CreateFamilies(context, 5);

            List<Customer.RolesEnum> roles = [Customer.RolesEnum.Child, Customer.RolesEnum.Parent];

            var customers = GenerateCustomers(20, families, roles);

            await context.Customers.AddRangeAsync(customers);

            await context.SaveChangesAsync();

            var accountsCount = await context.Customers.CountAsync(); // Count the number of customers

            Assert.Equal(20, customers.Count); // Ensure there are exactly 5 Customers
            Assert.Equal(20, accountsCount); // Ensure there are exactly 5 Customers
            Assert.Equal(5, families.Count);
        }
    }

    [Fact]
    public async Task AddCoaches_ShouldCreateCoachesInDatabase()
    {
        var options = GetDbContextOptions();

        await using (var context = new AppDbContext(options))
        {
            TruncateTables(context);

            var coaches = GenerateCoaches(5);

            await context.Coaches.AddRangeAsync(coaches);

            await context.SaveChangesAsync();

            var coachesCount = await context.Coaches.CountAsync(); // Count the number of coaches
            var accountsCount = await context.IndividualAccounts.CountAsync(); // Count the number of coaches

            Assert.Equal(5, coachesCount); // Ensure there are exactly 5 coaches
            Assert.Equal(5, accountsCount); // Ensure there are exactly 5 accounts
        }
    }


}