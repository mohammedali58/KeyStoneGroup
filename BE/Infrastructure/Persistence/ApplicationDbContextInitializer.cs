using Domain.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextInitializer
    {
        private readonly ILogger<ApplicationDbContextInitializer> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_context.Database.IsSqlite())
                {
                   await _context.Database.MigrateAsync();
                }

            }catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }

        public async Task SeedDataAsync()
        {
            try
            {
                //List<ProductBrand>? brands = default;
                //List<ProductType>? types = default;
                List<Language>? languages = default;
                List<DeliveryMethod>? deliveryMethods = default;
                List<CourseTxtInput>? courseTxtInput = default;

                if(!_context.Languages.Any())
                {
                    var languagesText = File.ReadAllText("../Infrastructure/SeedingData/languages.json");
                    languages = JsonSerializer.Deserialize<List<Language>>(languagesText);
                    if(languages is null)
                    {
                        throw new Exception("languages data seeding is null");
                    }

                    _context.AddRange(languages);
                    await _context.SaveChangesAsync();
                }

                if (!_context.DeliveryMethods.Any())
                {
                    var deliveryMethodText = File.ReadAllText("../Infrastructure/SeedingData/delivery-methods.json");
                    deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodText);
                    if (deliveryMethods is null)
                    {
                        throw new Exception("delivery-methods data seeding is null");
                    }

                    _context.AddRange(deliveryMethods);
                    await _context.SaveChangesAsync();
                }


                if (!_context.Courses.Any())
                {
                    var coursesText = File.ReadAllText("../Infrastructure/SeedingData/courses.json");
                    courseTxtInput = JsonSerializer.Deserialize<List<CourseTxtInput>>(coursesText);
                    
                    if (courseTxtInput is null)
                    {
                        throw new Exception("courses data seeding is null");
                    }

                    List<Course> courses = new List<Course>();

                    foreach (var course in courseTxtInput)
                    {
                        Language? language = _context.Languages.FirstOrDefault(l => l.Id == course.LanguageId);
                        DeliveryMethod? deliveryMethod = _context.DeliveryMethods.FirstOrDefault(d => d.Id == course.DeliveryMethodId);
                        Course newCourse = new() 
                        {
                            Id = course.Id,
                            InstituteName=course.InstituteName,
                            CourseName=course.CourseName,
                           Category=course.Category,
                           Location=course.Location,
                           StartDate =course.StartDate,
                           Language=language,
                           DeliveryMethod=deliveryMethod,
                           
                        };
                        courses.Add(newCourse);
                    }

                    _context.AddRange(courses);
                    await _context.SaveChangesAsync();

                }

               
                    if(_context.ChangeTracker.HasChanges())
                    {
                        await _context.SaveChangesAsync();
                    }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding data.");
            }
        }
    }
}
