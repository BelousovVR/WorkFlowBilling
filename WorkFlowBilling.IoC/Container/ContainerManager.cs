using Autofac;
using System.Reflection;
using WorkFlowBilling.IoC.Extensions;

namespace WorkFlowBilling.IoC.Container
{
    public class ContainerManager : IContainerManager
    {
        public ContainerBuilder Builder { get; private set; }

        //public 

        public ContainerManager()
        {
            Builder = new ContainerBuilder();
        }

        public void AddAssembliesTypes(Assembly assembly)
        {
            Builder.AddAssembliesTypes(assembly);
        }

        public IContainer GetContainerInstance()
        {
            return Builder.Build();
        }
    }
}
