// using Ardalis.Result;
// using Microsoft.AspNetCore.Http.HttpResults;
// using IResult = Ardalis.Result.IResult;
//
// namespace SSW.CleanArchitecture.WebApi.Extensions;
//
// public static class TypedResultsExt
// {
//     public static ValidationProblem ValidationProblem(IResult result)
//     {
//         var errors = new Dictionary<string, string[]>();
//         foreach (var error in result.ValidationErrors)
//             errors.Add(error.Identifier, [error.ErrorMessage]);
//
//         return TypedResults.ValidationProblem(errors, title: "One or more validation errors occurred.");
//     }
//
//     public static NotFound<string> NotFound(Result result)
//     {
//         return TypedResults.NotFound(result.Errors.FirstOrDefault());
//     }
// }