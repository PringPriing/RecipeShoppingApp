using AutoMapper;
using RecipeShoppingApp.Data.Infrastructure;
using RecipeShoppingApp.Data.Repositories;
using RecipeShoppingApp.Entities;
using RecipeShoppingApp.Web.Infrastructure.Core;
using RecipeShoppingApp.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using RecipeShoppingApp.Web.Infrastructure.Extensions;

namespace RecipeShoppingApp.Web.Controllers
{
    [Authorize(Roles="Admin")]
    [RoutePrefix("api/Measurement")]
    public class MeasurementController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Measurement> _measurementRepository;
        private readonly IEntityBaseRepository<Ingredient> _ingredientRepository;
        public MeasurementController(IEntityBaseRepository<Measurement> measurementRepository,IEntityBaseRepository<Ingredient> ingredientRepository,
            IEntityBaseRepository<Error> _errorsRepository,
            IUnitOfWork _unitOfWork)
            :base(_errorsRepository,_unitOfWork)
        {
            _measurementRepository = measurementRepository;
            _ingredientRepository = ingredientRepository;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("measurements")]
        public HttpResponseMessage GetMeasurements(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;
            var measurements = _measurementRepository.GetAll();

            IEnumerable<MeasurementViewModel> measurementVM = Mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>(measurements);

            response = request.CreateResponse<IEnumerable<MeasurementViewModel>>(HttpStatusCode.OK, measurementVM);
            
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, MeasurementViewModel measurement)
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
                        Measurement newMeasurement = new Measurement();
                        newMeasurement.UpdateMeasurement(measurement);
                        _measurementRepository.Add(newMeasurement);

                        _unitOfWork.Commit();

                        measurement = Mapper.Map<Measurement, MeasurementViewModel>(newMeasurement);
                        response = request.CreateResponse<MeasurementViewModel>(HttpStatusCode.Created, measurement);
                    }
                    return response;
                });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request,MeasurementViewModel measurement)
        {
            return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    
                    if(!ModelState.IsValid)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        var measurementDB = _measurementRepository.GetSingle(measurement.ID);
                        measurementDB.UpdateMeasurement(measurement);
                        _measurementRepository.Edit(measurementDB);

                        _unitOfWork.Commit();

                        measurement = Mapper.Map<Measurement, MeasurementViewModel>(measurementDB);
                        response = request.CreateResponse<MeasurementViewModel>(HttpStatusCode.OK, measurement);
                    }
                    
                    return response;
                });
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("delete/{id:int}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id )
        {
            return CreateHttpResponse(request, () =>
                {

                    HttpResponseMessage response = null;

                    var measurementToDelete = _measurementRepository.GetSingle(id);
                    var measurementUsed = _ingredientRepository.GetAll().Where(x => x.MeasurementID == id).Count();
                    if(measurementUsed != 0)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request : Some recipes are using this measurement.");
                    }
                    else
                    {
                        _measurementRepository.Delete(measurementToDelete);
                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK);

                    }

                    return response;
                });
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("measurementsAdmin")]
        public HttpResponseMessage GetMeasurementsAdmin(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;
            var measurements = _measurementRepository.GetAll();

            IEnumerable<MeasurementViewModel> measurementVM = Mapper.Map<IEnumerable<Measurement>, IEnumerable<MeasurementViewModel>>(measurements);

            response = request.CreateResponse<IEnumerable<MeasurementViewModel>>(HttpStatusCode.OK, measurementVM);

            return response;
        }


        
    
    }
}