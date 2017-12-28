using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using xave.com.helper;
using xave.web.structureset.biz;
using xave.web.structureset.dto;

namespace xave.web.structureset.svc.Controllers
{
    public class StructureSetController : ApiController
    {
        // GET api/structureset
        // url: http://localhost:50002/api/StructureSet/Get
        public Containers Get()
        {
            try
            {
                Logger.Save(System.Reflection.MethodBase.GetCurrentMethod(), Request.RequestUri.AbsoluteUri);

                BusinessLayer businessLayer = new BusinessLayer();
                Containers container = businessLayer.ReadContainer();
                return container;
            }
            catch (ArgumentException e) // 입력 값이 잘못되었을 때 발생되는 Exception
            {
                Logger.Save(System.Reflection.MethodBase.GetCurrentMethod(), Request.RequestUri.AbsoluteUri, e);
                throw xave.com.helper.ExceptionHandler.WebException(e, "StructureSetController", "Get", null, HttpStatusCode.BadRequest);
            }
            catch (Exception e) // 서버 내부 오류로 인하여 발생되는 Exception
            {
                Logger.Save(System.Reflection.MethodBase.GetCurrentMethod(), Request.RequestUri.AbsoluteUri, e);
                throw xave.com.helper.ExceptionHandler.WebException(e, "StructureSetController", "Get", null, HttpStatusCode.InternalServerError);
            }
        }
    }
}
