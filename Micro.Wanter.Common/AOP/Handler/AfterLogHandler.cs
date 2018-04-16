using System;
using System.Text;
using Unity.Interception.PolicyInjection.Pipeline;

namespace Micro.Wanter.Common.AOP.Handler
{
    public class AfterLogHandler : ICallHandler
    {
        public int Order { get; set; }
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn methodReturn = getNext()(input, getNext);
            StringBuilder sb = new StringBuilder();
            foreach (var para in input.Inputs)
            {
                sb.AppendFormat("Para={0} ", para == null ? "null" : para.ToString());
            }
            Console.WriteLine("日志已记录，结束请求{0}", sb.ToString());
            return methodReturn;
        }
    }
}
