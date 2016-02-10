// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace AspNet5Resources
{
  public class Startup
  {
    private string applicationBasePath;

    public Startup(IApplicationEnvironment applicationEnvironment, IHostingEnvironment hostingEnvironment)
    {
      this.applicationBasePath = applicationEnvironment.ApplicationBasePath;

      hostingEnvironment.WebRootFileProvider = this.GetFileProvider(this.applicationBasePath);
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
      applicationBuilder.UseStaticFiles();
      applicationBuilder.UseMvcWithDefaultRoute();
    }

    public IFileProvider GetFileProvider(string path)
    {
      IEnumerable<IFileProvider> fileProviders = new IFileProvider[] { new PhysicalFileProvider(path) };

      return new CompositeFileProvider(
        fileProviders.Concat(
          new Assembly[] { Assembly.Load(new AssemblyName("AspNet5Resources.Resources")) }.Select(a => new EmbeddedFileProvider(a, a.GetName().Name))
        )
      );
    }
  }
}