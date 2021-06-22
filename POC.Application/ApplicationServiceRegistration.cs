﻿using AutoMapper.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace POC.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
           {
               cfg.AddDataReaderMapping();
           }, Assembly.GetExecutingAssembly());

            services.AddAutoMapper(cfg =>
            {
                cfg.AddDataReaderMapping();
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}
