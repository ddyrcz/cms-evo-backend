﻿using Autofac;
using CMS.Cars.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CMS.Cars.Application
{
    public class CarsModule : Autofac.Module
    {
        public IConfiguration Configuration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterDbContext(builder);

            builder.RegisterAssemblyTypes(typeof(ApplicationProjectMarker).Assembly)
                .Where(service =>
                    service.Name.EndsWith("CommandHandler") ||
                    service.Name.EndsWith("QueryHandler"))
                .AsImplementedInterfaces();

            var configuration = new Configuration()
            {
                NotificationDaysBefore = Configuration.GetValue<int>("NotificationDaysBefore")
            };

            builder.RegisterInstance(configuration).AsSelf();
        }

        private void RegisterDbContext(ContainerBuilder builder)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<CarsDbContext>();
            var connectionString = Configuration.GetValue<string>("ConnectionStrings:CarsDbConnectionString");
            dbContextOptionsBuilder.UseSqlServer(connectionString);

            var dbContextOptions = dbContextOptionsBuilder.Options;

            // DbContextOptions are required by EF Core tools
            builder.RegisterInstance(dbContextOptions)
                .As<DbContextOptions>();

            builder.Register(c => new CarsDbContext(dbContextOptions))
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
