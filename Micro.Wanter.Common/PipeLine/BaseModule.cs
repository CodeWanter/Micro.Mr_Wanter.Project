using Micro.Wanter.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Micro.Wanter.Common.PipeLine
{
    public class BaseModule : IHttpModule
    {
        /// <summary>
        /// Init方法仅用于给期望的事件注册方法
        /// </summary>
        /// <param name="httpApplication"></param>
        public void Init(HttpApplication httpApplication)
        {
            httpApplication.BeginRequest += new EventHandler(context_BeginRequest);//Asp.net处理的第一个事件，表示处理的开始
            //context.AuthenticateRequest	//验证请求，一般用来取得请求用户的信息
            //context.PostAuthenticateRequest	已经获取请求用户的信息
            //context.AuthorizeRequest	授权，一般用来检查用户的请求是否获得权限
            //context.PostAuthorizeRequest	用户请求已经得到授权
            //context.ResolveRequestCache	获取以前处理缓存的处理结果，如果以前缓存过，那么，不必再进行请求的处理工作，直接返回缓存结果
            //context.PostResolveRequestCache	已经完成缓存的获取操作
            //context.PostMapRequestHandler	已经根据用户的请求，创建了处理请求的处理器对象
            //context.AcquireRequestState	取得请求的状态，一般用于Session
            //context.PostAcquireRequestState	已经取得了Session
            //context.PreRequestHandlerExecute	准备执行处理程序
            //context.PostRequestHandlerExecute	已经执行了处理程序
            //context.ReleaseRequestState	释放请求的状态
            //context.PostReleaseRequestState	已经释放了请求的状态
            //context.UpdateRequestCache	更新缓存
            //context.PostUpdateRequestCache	已经更新了缓存
            //context.LogRequest	请求的日志操作
            //context.PostLogRequest	已经完成了请求的日志操作

            httpApplication.EndRequest += new EventHandler(context_EndRequest);//本次请求处理完成
        }

        // 处理BeginRequest 事件的实际代码
        private void context_BeginRequest(object sender, EventArgs e)
        {
            ExcuteBat.Bat();
        }

        // 处理EndRequest 事件的实际代码
        private void context_EndRequest(object sender, EventArgs e)
        {
        }

        public void Dispose()
        {

        }
    }
}