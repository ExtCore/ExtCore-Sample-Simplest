// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using ExtCore.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApplication
{
  public class Startup : ExtCore.WebApplication.Startup
  {
    public Startup(IServiceProvider serviceProvider)
      : base (serviceProvider)
    {
      this.serviceProvider.GetService<ILoggerFactory>().AddConsole();

      IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(this.serviceProvider.GetService<IHostingEnvironment>().ContentRootPath)
        .AddJsonFile("config.json", optional: true, reloadOnChange: true);

      this.configurationRoot = configurationBuilder.Build();
    }

    public override void ConfigureServices(IServiceCollection services)
    {
      base.ConfigureServices(services);
    }

    public override void Configure(IApplicationBuilder applicationBuilder)
    {
      base.Configure(applicationBuilder);
      applicationBuilder.Run(async (context) =>
      {
        await context.Response.WriteAsync(ExtensionManager.Extensions.First().Name);
      });
    }
  }
}