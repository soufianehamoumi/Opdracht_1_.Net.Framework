using Microsoft.EntityFrameworkCore;

namespace Studentenbeheer.Data
{
    public static class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new StudentenbeheerContext(
                serviceProvider.GetRequiredService<DbContextOptions<StudentenbeheerContext>>());
            {
                if (!context.Gender.Any())
                {
                    context.Gender.AddRange(
                    new Models.Gender
                    {
                        Id = '?',
                        Name = "?"
                    },
                   new Models.Gender
                   {
                       Id = 'M',
                       Name = "Male"
                   },
                   new Models.Gender
                   {
                       Id = 'F',
                       Name = "Female",
                       

                   });
                    context.SaveChanges();
                }
                if (!context.Student.Any())
                {
           
                context.Student.AddRange(

                   new Models.Student
                   {
                       Voornaam = "?",
                       Achternaam = "?",
                       Geboortedatum = DateTime.MinValue,
                       GenderID = '?',
                       Deleted = DateTime.MaxValue
                   },
                   new Models.Student
                   {
                       Voornaam = "soufiane",
                       Achternaam = "Hamoumi",
                       Geboortedatum = DateTime.MinValue,
                       GenderID = 'M',
                       Deleted = DateTime.MaxValue
                   }) ;
                context.SaveChanges();
                }
                if (!context.Module.Any())
                {
                    context.Module.AddRange(
                        new Models.Module
                    {
                      Name = "?",
                      Omschrijving = "?",
                      Deleted = DateTime.MaxValue
                    
                    },
                        new Models.Module
                    {
                      Name = "Module1",
                      Omschrijving = "Errste Module",
                      Deleted = DateTime.MaxValue
                    
                    });
                    context.SaveChanges();
                }
                if (!context.Inschrijvingen.Any())
                {
                    context.Inschrijvingen.AddRange(
                        new Models.Inschrijvingen
                        {
                            ModuleId = 1,
                            StudentId = 1,
                            InschrijvingsDatum = DateTime.MinValue,
                            AfgelegdOp  = DateTime.MaxValue,
                            Resultaat = "?"
                        },
                        new Models.Inschrijvingen
                        {
                            ModuleId = 2,
                            StudentId = 2,
                            InschrijvingsDatum = DateTime.MinValue,
                            AfgelegdOp = DateTime.MaxValue,
                            Resultaat = "Resultaat"
                        });
                    context.SaveChanges();
                }
            }
        }
    }
}
