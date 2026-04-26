using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.Managers;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        // 🔹 DATABASE CONFIGURATION
        services.AddDbContext<StudentDbContext>(options =>
            options.UseSqlServer(
                "Server=localhost\\SQLEXPRESS;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;"
            ));

        // 🔹 DEPENDENCY INJECTION
        services.AddScoped<IStudentManager, StudentManager>();

        // 🔹 (OPTIONAL BUT GOOD PRACTICE)
        services.AddLogging();
    })
    .Build();

host.Run();