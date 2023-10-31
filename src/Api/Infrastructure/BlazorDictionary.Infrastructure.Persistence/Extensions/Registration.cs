using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Infrastructure.Persistence.Context;
using BlazorDictionary.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<BlazorDictionaryDbContext>(conf => 
            {
                var connStr = configuration.GetConnectionString("SqlConnection");
                conf.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });

            // var seedData = new SeedData();
            // seedData.SeedAsync(configuration).GetAwaiter().GetResult();
            // // Eğer veri eklemek istersek yukarıdaki iki satırı çallıştırmak yeterli olacaktır. Uygulama her çalıştığında veri oluşturmamak için yorum satırı haline getirdik.

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();

            return services;
        }
    }
}
