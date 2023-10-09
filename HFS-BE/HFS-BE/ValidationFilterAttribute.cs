using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using HFS_BE.Base;
using HFS_BE.Ultis;

namespace HFS_BE
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // valid 2
                //var validationErrors = new Dictionary<string, List<string>>();

                //foreach (var key in context.ModelState.Keys)
                //{
                //    var errors2 = context.ModelState[key].Errors.Select(error => error.ErrorMessage).ToList();
                //    validationErrors[key] = errors2;
                //}
                var a = new UnprocessableEntityObjectResult(context.ModelState);
                // valid 1:
                var errors = context.ModelState
                .Select(x => new ValidationErrorDto
                {
                    Field = x.Key,
                    Messages = x.Value.Errors.Select(e => e.ErrorMessage).ToList()
                })
                .ToList();
                 
                // return
                var result = new BaseOutputDto
                {
                    StatusCode = (int)System.Net.HttpStatusCode.UnprocessableEntity,
                    Success = false,
                    Message = "One or more validation errors occurred.",
                    Errors = new ErrorsMessage()
                    {
                        ValidationErrors = errors,
                    },
                    //ValdationErros2 = validationErrors
                };

                context.Result = new UnprocessableEntityObjectResult(result);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
