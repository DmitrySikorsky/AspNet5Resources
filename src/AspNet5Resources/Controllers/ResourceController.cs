// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace AspNet5Resources.Controllers
{
  public class ResourceController : Controller
  {
    public ActionResult Index(string name)
    {
      Assembly assembly = Assembly.Load(new AssemblyName("AspNet5Resources.Resources"));
      string fullName = assembly.GetName().Name + "." + name;

      if (assembly.GetManifestResourceNames().Contains(fullName))
      {
        Stream stream = assembly.GetManifestResourceStream(fullName);

        return this.Stream(stream);
      }

      return this.HttpNotFound();
    }
  }
}