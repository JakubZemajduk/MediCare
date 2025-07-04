﻿using MediCare.Data;
using MediCare.Data.Models;
using MediCare.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace MediCare
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddDbContext<DB_MediCareContext>(options =>
               options.UseSqlServer(AppConfig.ConnectionString, sqlOptions =>
               {
                   sqlOptions.EnableRetryOnFailure();
               }));


            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<RegisterService>();
            services.AddScoped<LoginService>();


            ServiceProvider = services.BuildServiceProvider();

            base.OnStartup(e);

            using var context = new DB_MediCareContext();
            var hasher = new PasswordHasher<User>();
         

           
        }
    }
}
