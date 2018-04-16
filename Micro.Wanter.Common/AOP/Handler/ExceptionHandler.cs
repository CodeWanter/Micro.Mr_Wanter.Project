using System;
using Unity.Interception.PolicyInjection.Pipeline;

namespace Micro.Wanter.Common.AOP.Handler
{
    public class ExceptionHandler : ICallHandler
    {
        public int Order { get; set; }
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn methodReturn = getNext()(input, getNext);
            if (methodReturn.Exception == null)
            {
                Console.WriteLine("无异常");
            }
            else
            {
                Console.WriteLine("异常:{0}", methodReturn.Exception.Message);
            }
            return methodReturn;
        }
    }
}
