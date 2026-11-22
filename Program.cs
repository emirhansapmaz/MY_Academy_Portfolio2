using Microsoft.EntityFrameworkCore;
using MY_Academy_Portfolio2.Context;
using MY_Academy_Portfolio2.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PortfolioContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PortfolioContext>();
    try
    {
        context.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Messages' and xtype='U')
            BEGIN
                CREATE TABLE [Messages] (
                    [MessageId] int NOT NULL IDENTITY(1, 1),
                    [NameSurname] nvarchar(max) NOT NULL,
                    [Email] nvarchar(max) NOT NULL,
                    [Subject] nvarchar(max) NOT NULL,
                    [MessageContent] nvarchar(max) NOT NULL,
                    [SendDate] datetime2 NOT NULL,
                    [IsRead] bit NOT NULL,
                    CONSTRAINT [PK_Messages] PRIMARY KEY ([MessageId])
                );
            END
        ");

        context.Database.ExecuteSqlRaw(@"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Skills' and xtype='U')
            BEGIN
                CREATE TABLE [Skills] (
                    [SkillId] int NOT NULL IDENTITY(1, 1),
                    [Title] nvarchar(max) NOT NULL,
                    [Value] int NOT NULL,
                    [Category] nvarchar(max) NULL,
                    CONSTRAINT [PK_Skills] PRIMARY KEY ([SkillId])
                );
            END
        ");

        if (!context.Skills.Any())
        {
            context.Skills.AddRange(
                new Skill { Title = "C#", Value = 92, Category = "Backend Development" },
                new Skill { Title = "ASP.NET Core", Value = 90, Category = "Backend Development" },
                new Skill { Title = "Web API", Value = 88, Category = "Backend Development" },
                new Skill { Title = "EF Core", Value = 85, Category = "Backend Development" },
                new Skill { Title = "LINQ", Value = 85, Category = "Backend Development" },
                new Skill { Title = "HTML5 & CSS3", Value = 90, Category = "Frontend Development" },
                new Skill { Title = "JavaScript (ES6+)", Value = 80, Category = "Frontend Development" },
                new Skill { Title = "Bootstrap 5", Value = 90, Category = "Frontend Development" },
                new Skill { Title = "Responsive Design", Value = 88, Category = "Frontend Development" },
                new Skill { Title = "MS SQL Server", Value = 88, Category = "Database Technologies" },
                new Skill { Title = "PostgreSQL", Value = 75, Category = "Database Technologies" },
                new Skill { Title = "Git & GitHub", Value = 88, Category = "Tools & Practices" },
                new Skill { Title = "Docker", Value = 70, Category = "Tools & Practices" },
                new Skill { Title = "Postman & Swagger", Value = 85, Category = "Tools & Practices" },
                new Skill { Title = "OOP (Nesne Yönelimli)", Value = 90, Category = "Software Architecture" },
                new Skill { Title = "SOLID Principles", Value = 85, Category = "Software Architecture" },
                new Skill { Title = "Clean Architecture", Value = 82, Category = "Software Architecture" }
            );
            context.SaveChanges();
        }

        var existingAbout = context.Abouts.FirstOrDefault();
        if (existingAbout != null && existingAbout.NameSurname != null && existingAbout.NameSurname.Contains("Poyraz"))
        {
            existingAbout.NameSurname = "Emirhan Sapmaz";
            existingAbout.Email = existingAbout.Email.Replace("poyraz.alsever", "emirhan.sapmaz").Replace("poyraz", "emirhan");
            existingAbout.Description = existingAbout.Description.Replace("Poyraz Alsever", "Emirhan Sapmaz").Replace("Poyraz", "Emirhan");
            context.SaveChanges();
        }

        var existingHero = context.HeroSections.FirstOrDefault();
        if (existingHero != null && existingHero.Subtitle != null && existingHero.Subtitle.Contains("Poyraz"))
        {
            existingHero.Subtitle = "Emirhan Sapmaz";
            context.SaveChanges();
        }
    }
    catch
    {
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
    


app.Run();
