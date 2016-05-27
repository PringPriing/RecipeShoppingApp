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
using System.Collections;
using System.IO;

namespace RecipeShoppingApp.Web.Controllers
{
    [Authorize(Roles = "Admin,Basic")]
    [RoutePrefix("api/Recipe")]
    public class RecipeController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Recipe> _recipeRepository;
        public RecipeController(IEntityBaseRepository<Recipe> recipeRepository,
                                IEntityBaseRepository<Error> _errorsRepository,
                                IUnitOfWork _unitOfWork)
            :base(_errorsRepository,_unitOfWork)
        {
            _recipeRepository = recipeRepository;
        }
        [HttpGet]
        [Route("recipes")]
        public HttpResponseMessage GetRecipes(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;
            var recipes = _recipeRepository.GetAll();

            IEnumerable<RecipeViewModel> recipeVM = Mapper.Map<IEnumerable<Recipe>, IEnumerable<RecipeViewModel>>(recipes);

            response = request.CreateResponse<IEnumerable<RecipeViewModel>>(HttpStatusCode.OK, recipeVM);

            return response;

        }
        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request,int id)
        {
            return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    var recipe = _recipeRepository.GetSingle(id);

                    RecipeViewModel recipeVm = Mapper.Map<Recipe, RecipeViewModel>(recipe);
                    response = request.CreateResponse<RecipeViewModel>(HttpStatusCode.OK, recipeVm);

                    return response;
                });
        }

        [AllowAnonymous]
        [Route("{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    List<Recipe> recipes = null;

                    int totalRecipes = new int();

                    if (!string.IsNullOrEmpty(filter))
                    {
                        recipes = _recipeRepository.FindBy(r => r.RecipeName.ToLower()
                                   .Contains(filter.ToLower().Trim()))
                                   .OrderBy(r => r.ID)
                                   .Skip(currentPage * currentPageSize)
                                   .Take(currentPageSize)
                                   .ToList();

                        totalRecipes = _recipeRepository.FindBy(r => r.RecipeName.ToLower()
                            .Contains(filter.ToLower().Trim()))
                            .Count();

                    }
                    else
                    {
                        recipes = _recipeRepository.GetAll()
                            .OrderBy(r => r.ID)
                            .Skip(currentPage * currentPageSize)
                            .Take(currentPageSize)
                            .ToList();

                        totalRecipes = _recipeRepository.GetAll().Count();
                    }

                    IEnumerable<RecipeViewModel> recipeVM = Mapper.Map<IEnumerable<Recipe>, IEnumerable<RecipeViewModel>>(recipes);
                    PaginationSet<RecipeViewModel> pagedSet = new PaginationSet<RecipeViewModel>()
                    {
                        Page = currentPage,
                        TotalCount = totalRecipes,
                        TotalPages = (int)Math.Ceiling((decimal)totalRecipes / currentPageSize),
                        Items = recipeVM
                    };

                    response = request.CreateResponse<PaginationSet<RecipeViewModel>>(HttpStatusCode.OK, pagedSet);

                    return response;
                });
         }


        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request,RecipeViewModel recipe)
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
                        Recipe newRecipe = new Recipe();
                        newRecipe.UpdateRicepe(recipe);
                        _recipeRepository.Add(newRecipe);

                        _unitOfWork.Commit();

                        //Update View Model
                        recipe = Mapper.Map<Recipe, RecipeViewModel>(newRecipe);
                        response = request.CreateResponse<RecipeViewModel>(HttpStatusCode.Created, recipe);
                    }

                    return response;
                });
        }

        [HttpPost]
        [Route("delete/{id=int}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    var recipeToDelete = _recipeRepository.GetSingle(id);
                    _recipeRepository.Delete(recipeToDelete);
                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);
                    return response;
                });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, RecipeViewModel recipe)
        {
            return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;
                    if (!ModelState.IsValid)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        var recipeDb = _recipeRepository.GetSingle(recipe.ID);
                        recipeDb.UpdateRicepe(recipe);
                        _recipeRepository.Edit(recipeDb);

                        _unitOfWork.Commit();

                        recipe = Mapper.Map<Recipe, RecipeViewModel>(recipeDb);
                        response = request.CreateResponse<RecipeViewModel>(HttpStatusCode.OK, recipe);
                    }
                    return response;
                });
        }

        [MimeMultipart]
        [Route("images/upload")]
        public HttpResponseMessage Post(HttpRequestMessage request, int recipeID)
        {
            return CreateHttpResponse(request, () =>
                {
                    HttpResponseMessage response = null;

                    var recipeDB = _recipeRepository.GetSingle(recipeID);
                    if (recipeDB == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid recipe.");
                    else
                    {
                        var uploadPath = HttpContext.Current.Server.MapPath("~/Content/images/recipe");

                        var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);

                        // Read the MIME multipart asynchronously 
                        Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

                        string _localFileName = multipartFormDataStreamProvider
                            .FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

                        // Create response
                        FileUploadResult fileUploadResult = new FileUploadResult
                        {
                            LocalFilePath = _localFileName,

                            FileName = Path.GetFileName(_localFileName),

                            FileLength = new FileInfo(_localFileName).Length
                        };

                        // update database
                        recipeDB.Image = fileUploadResult.FileName;
                        _recipeRepository.Edit(recipeDB);
                        _unitOfWork.Commit();

                        response = request.CreateResponse(HttpStatusCode.OK, fileUploadResult);
                    }

                    return response;
                });
        }


    }
}