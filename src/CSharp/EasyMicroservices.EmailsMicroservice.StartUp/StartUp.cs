using EasyMicroservices.EmailsMicroservice.Helpers;
using EasyMicroservices.EmailsMicroservice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.EmailsMicroservice
{
    public class StartUp
    {
        public Task Run(IDependencyManager dependencyManager)
        {
            ApplicationManager.Instance.DependencyManager = dependencyManager;
            return Task.CompletedTask;
        }
    }
}
