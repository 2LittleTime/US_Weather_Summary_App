using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using System.Configuration;
using TemplateFull.Models;
using TemplateFull.Models.Interfaces;
using TemplateFull.Models.ViewModels;

namespace TemplateFull.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernal;

        public NinjectDependencyResolver()
        {
            kernal = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernal.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernal.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernal.Bind<ITablesView>().To<TablesView>();
            /*   
            .WithConstructorArgument("stationName", Cont)
               .WithConstructorArgument("beginMonth", beginMonth)
               .WithConstructorArgument("beginDay", beginDay)
               .WithConstructorArgument("beginYear", beginYear)
               .WithConstructorArgument("endMonth", endMonth)
               .WithConstructorArgument("endDay", endDay)
               .WithConstructorArgument("endYear", endYear);
             */
        }


    }
}