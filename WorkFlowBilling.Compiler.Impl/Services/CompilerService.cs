using System;
using WorkFlowBilling.Compiler.Services;
using WorkFlowBilling.IoC.Attributes;

namespace WorkFlowBilling.Compiler.Impl.Services
{
    [AutoInjectableInstance]
    class CompilerService : ICompilerService
    {
        public void Compile()
        {
            return;
        }
    }
}
