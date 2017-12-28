using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;

namespace xave.web.code.svc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 구성 및 서비스
            // 전달자 토큰 인증만 사용하도록 Web API를 구성합니다.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // JSON 데이터에 카멜 케이스를 사용합니다.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API 경로
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // 응용 프로그램에서 추적을 사용하지 않도록 설정하려면 설명을 추가하거나 다음 코드 행을 제거합니다.
            // 자세한 내용은 http://www.asp.net/web-api를 참조하십시오.
            config.EnableSystemDiagnosticsTracing();
            // Install-Package Microsoft.AspNet.WebApi.Tracing

            // 디폴트 리턴 타입 변경
            config.Formatters.JsonFormatter.SupportedMediaTypes
            .Add(new MediaTypeHeaderValue("text/html"));

            // xml 입력 받을 수 있는 설정
            config.Formatters.XmlFormatter.UseXmlSerializer = true;
        }
    }
}
