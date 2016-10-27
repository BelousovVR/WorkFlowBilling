using WorkFlowBilling.IoC.Attributes;

namespace WorkFlowBilling.Compiler.Services
{
    [AutoInjectable]
    public interface ICompilerService
    {
        void Compile();
    }
}
