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

namespace RecipeShoppingApp.Web.Controllers
{

    [Authorize(Roles = "Admin,Basic")]
    [RoutePrefix("api/ingredient")]
    public class IngredientController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Ingredient> _ingredientRepository;
        public IngredientController(IEntityBaseRepository<Ingredient> ingredientRepository,
                                    IEntityBaseRepository<Error> _errorRepository,
                                    IUnitOfWork _unitofWork)
            :base(_errorRepository,_unitofWork)
        {
            _ingredientRepository = ingredientRepository;
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, IngredientViewModel ingredientViewModel)
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
                        Ingredient newIngredient = new Ingredient();
                        newIngredient.UpdateIngredient(ingredientViewModel);
                        _ingredientRepository.Add(newIngredient);

                        _unitOfWork.Commit();

                        ingredientViewModel = Mapper.Map<Ingredient, IngredientViewModel>(newIngredient);
                        response = request.CreateResponse<IngredientViewModel>(HttpStatusCode.Created, ingredientViewModel); 
                    }
                    return response;
                });
        }

        [HttpGet]
        [Route("ingredients")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;
            var ingredients = _ingredientRepository.GetAll();
            IEnumerable<IngredientViewModel> ingredientsVM = Mapper.Map<IEnumerable<Ingredient>, IEnumerable<IngredientViewModel>>(ingredients);
            response = request.CreateResponse<IEnumerable<IngredientViewModel>>(HttpStatusCode.OK, ingredientsVM);
            return response;
        }

        [HttpGet]
        [Route("ingredients/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request,int id)
        {
            HttpResponseMessage response = null;
            var ingredients = _ingredientRepository.GetAll().Where(x => x.recipe.ID == id );
            IEnumerable<IngredientViewModel> ingredientsVM = Mapper.Map<IEnumerable<Ingredient>, IEnumerable<IngredientViewModel>>(ingredients);
            response = request.CreateResponse<IEnumerable<IngredientViewModel>>(HttpStatusCode.OK, ingredientsVM);
            return response;
        }
    }
}