// using Api.Database;
// using Api.Model;
// using Bogus;
// using Microsoft.EntityFrameworkCore;
// using Xunit;

// namespace Api.Tests;

// public class ModelTest()
// {
//     private static DbContextOptions<AppDbContext> GetDbContextOptions()
//     {
//         return new DbContextOptionsBuilder<AppDbContext>()
//             .UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=root;Database=mydatabase")
//             .Options;
//     }

//     private static async Task TruncateTablesAsync(AppDbContext context)
//     {
//         // You can add more tables as needed
//         await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE public.\"FamilyMembers\" CASCADE");
//         await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE public.\"Families\" CASCADE");
//         await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE public.\"AthleteInfo\" CASCADE");
//         await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE public.\"Admins\" CASCADE");
//         await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE public.\"Courses\" CASCADE");
//         await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE public.\"Facilities\" CASCADE");
//         await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE public.\"Coaches\" CASCADE");
//         await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE public.\"Customers\" CASCADE");
//         await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE public.\"Accounts\" CASCADE");
//         // Add any other tables that need truncation here
//     }

//     private static List<Customer> GenerateCustomers(int count)
//     {
//         return new Faker<Customer>()
//             .CustomInstantiator(f =>
//                 new Customer(
//                     f.Person.FullName,
//                     f.Internet.Email(),
//                     f.Internet.Password(),
//                     f.Phone.PhoneNumber("##########")
//                 )).Generate(count);
//     }

//     private static async Task<List<Family>> CreateFamilies(AppDbContext context, int amt)
//     {
//         List<Family> families = [];

//         for (var i = 0; i < amt; i++) {
//             families.Add(new Family());
//         }

//         context.Families.AddRange(families);
//         await context.SaveChangesAsync();

//         return families;
//     }

//     [Fact]

//     public async Task AddAdmin_ShouldCreateAdminInDatabase()
//     {
//         var options = GetDbContextOptions();

//         var admins = new Faker<Admin>()
//             .CustomInstantiator(f =>
//                 new Admin(
//                     f.Person.FullName,
//                     f.Internet.Email(),
//                     f.Internet.Password(),
//                     f.Phone.PhoneNumber("##########")
//                 ))
//             .Generate(5);

//         await using (var context = new AppDbContext(options))
//         {
//             await TruncateTablesAsync(context);

//             await context.Admins.AddRangeAsync(admins); // Add the generated Admins to the context
//             await context.SaveChangesAsync(); // Save changes to the database

//             var adminCount = await context.Accounts.CountAsync(); // Count the number of Admins
//             Assert.Equal(5, adminCount); // Ensure there are exactly 5 Admins
//         }
//     }

//     [Fact]
//     public async Task AddCustomer_ShouldCreateCustomerInDatabase()
//     {
//         var options = GetDbContextOptions();

//         var customers = GenerateCustomers(5);

//         await using (var context = new AppDbContext(options))
//         {
//             await TruncateTablesAsync(context);

//             await context.Customers.AddRangeAsync(customers);
//             await context.SaveChangesAsync(); // Save changes to the database

//             var customerCount = await context.Customers.CountAsync(); // Count the number of customers
//             var accountsCount = await context.Accounts.CountAsync(); // Count the number of customers

//             Assert.Equal(5, customerCount); // Ensure there are exactly 5 Customers
//             Assert.Equal(5, accountsCount); // Ensure there are exactly 5 Customers
//         }
//     }

//     [Fact]
//     public async Task AddFamily_ShouldCreateFamilyInDatabase()
//     {
//         var options = GetDbContextOptions();

//         await using (var context = new AppDbContext(options))
//         {
//             await TruncateTablesAsync(context);

//             await CreateFamilies(context, 5);

//             var familiesCount = await context.Families.CountAsync(); // Count the number of families

//             Assert.Equal(5, familiesCount); // Ensure there are exactly 5 families
//         }
//     }

//     [Fact]
//     public async Task AddFamilyMember_ShouldCreateFamilyMemberInDatabase()
//     {
//         var options = GetDbContextOptions();

//         await using (var context = new AppDbContext(options))
//         {
//             await TruncateTablesAsync(context);

//             var customers = GenerateCustomers(20);

//             await context.Customers.AddRangeAsync(customers);

//             List<Family> families = await CreateFamilies(context, 5);

//             List<FamilyMember> members = [];

//             for (int i = 0; i < 7; i++)
//             {
//                 var customer = customers[i];
//                 var randomFamily = families[new Random().Next(families.Count)];
//                 List<FamilyMember.RolesEnum> roles = [FamilyMember.RolesEnum.Child, FamilyMember.RolesEnum.Parent];

//                 members.Add(new FamilyMember(randomFamily.Id, customer.Id, roles[new Random().Next(roles.Count)]));
//             }

//             await context.FamilyMembers.AddRangeAsync(members);

//             await context.SaveChangesAsync();

//             var accountsCount = await context.Accounts.CountAsync(); // Count the number of customers

//             Assert.Equal(20, customers.Count); // Ensure there are exactly 5 Customers
//             Assert.Equal(20, accountsCount); // Ensure there are exactly 5 Customers
//             Assert.Equal(5, families.Count);
//         }
//     }


// }