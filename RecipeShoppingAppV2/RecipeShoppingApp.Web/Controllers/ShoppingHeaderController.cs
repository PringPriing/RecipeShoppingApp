using RecipeShoppingApp.Data.Infrastructure;
using RecipeShoppingApp.Data.Repositories;
using RecipeShoppingApp.Entities;
using RecipeShoppingApp.Web.Infrastructure.Core;
using RecipeShoppingApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using RecipeShoppingApp.Web.Infrastructure.Extensions;
using AutoMapper;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using ExportImplementation;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace RecipeShoppingApp.Web.Controllers
{
    [Authorize(Roles="Admin,Basic")]
    [RoutePrefix("api/shoppingHeader")]
    public class ShoppingHeaderController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<ShoppingHeader> _shoppingHeaderRepository;
        public ShoppingHeaderController(IEntityBaseRepository<ShoppingHeader> shoppingHeaderRepository,
                                        IEntityBaseRepository<Error> errorRepository,
                                        IUnitOfWork _unitOfWork)
            :base(errorRepository,_unitOfWork)
        {
            _shoppingHeaderRepository = shoppingHeaderRepository;
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request,ShoppingHeaderViewModel shoppingHeaderVM)
        {
            return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    if(!ModelState.IsValid)
                    {
                        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        ShoppingHeader newHeader = new ShoppingHeader();
                        newHeader.UpdateShoppingHeader(shoppingHeaderVM);
                        _shoppingHeaderRepository.Add(newHeader);

                        _unitOfWork.Commit();
                        
                        shoppingHeaderVM = Mapper.Map<ShoppingHeader,ShoppingHeaderViewModel>(newHeader);
                        response = request.CreateResponse<ShoppingHeaderViewModel>(HttpStatusCode.Created, shoppingHeaderVM);
                    }
                    return response;
                });
        }


        [HttpGet]
        [Route("shoppingHeaders")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;
            var shoppingHeaders = _shoppingHeaderRepository.GetAll();
            IEnumerable<ShoppingHeaderViewModel> shoppingVM = Mapper.Map<IEnumerable<ShoppingHeader>, IEnumerable<ShoppingHeaderViewModel>>(shoppingHeaders);
            response = request.CreateResponse<IEnumerable<ShoppingHeaderViewModel>>(HttpStatusCode.OK, shoppingVM);
            return response;
        }

        [Route("shoppingList/{weekDate:datetime}")]
        public HttpResponseMessage Get(HttpRequestMessage request, DateTime weekDate)
        {
     
            HttpResponseMessage response = null;
            var shoppingHeader = _shoppingHeaderRepository.GetAll()
                .Where(
                s => DbFunctions.TruncateTime(s.Date) == DbFunctions.TruncateTime(weekDate)).AsEnumerable()
                .LastOrDefault();
            ShoppingHeaderViewModel shoppingHeaderVM = Mapper.Map<ShoppingHeader, ShoppingHeaderViewModel>(shoppingHeader);
            response = request.CreateResponse<ShoppingHeaderViewModel>(HttpStatusCode.OK, shoppingHeaderVM);
            return response;
        }
        [HttpPost]
        [Route("delete/{weekDate:datetime}")]
        public HttpResponseMessage xGet(HttpRequestMessage request, DateTime weekDate)
        {

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var shoppingHeader = _shoppingHeaderRepository.GetAll().Where(
                     s => DbFunctions.TruncateTime(s.Date) == DbFunctions.TruncateTime(weekDate));

                foreach (var item in shoppingHeader)
                {
                    _shoppingHeaderRepository.Delete(item);
                   
                }

                _unitOfWork.Commit();
                response = request.CreateResponse(HttpStatusCode.OK);
                return response;

            });
        }

        [Route("export")]
        public HttpResponseMessage Export(HttpRequestMessage request,[FromBody] List<ShoppingListViewModel> shoppingList )
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var data = ExportFactory.ExportDataJson(JArray.FromObject(shoppingList).ToString(), ExportToFormat.PDFiTextSharp4);
                File.WriteAllBytes("shoppingList.pdf", data);
                Process.Start("shoppingList.pdf");
                response = request.CreateResponse(HttpStatusCode.OK);
                return response;
            });
        }
    }
}