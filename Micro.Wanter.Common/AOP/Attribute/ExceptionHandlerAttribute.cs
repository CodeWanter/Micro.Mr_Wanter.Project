using Micro.Wanter.Common.AOP.Handler;
using Unity;
using Unity.Interception.PolicyInjection.Pipeline;
using Unity.Interception.PolicyInjection.Policies;

namespace Micro.Wanter.Common.AOP.Attribute
{
    public class ExceptionHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new ExceptionHandler() { Order = this.Order };
        }
    }

}
