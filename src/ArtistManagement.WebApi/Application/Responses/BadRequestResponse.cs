using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ArtistManagement.WebApi.Infrastructure;

namespace ArtistManagement.WebApi.Application.Responses
{
    public class BadRequestResponse : BaseModel
    {
        public string Message { get; }

        public List<ValidationErrorStateModel> Errors { get; }

        public BadRequestResponse(ModelStateDictionary modelState)
        {
            Message = "Validation Failed";

            Errors = modelState.Keys
                    .SelectMany(
                        key => modelState[key]
                            .Errors
                            .Select(x => new ValidationErrorStateModel(ToCamelCase(key), x.ErrorMessage))
                    )
                    .ToList();
        }

        private string ToCamelCase(string str)
        {
            if (str == null || str.Length < 2)
                return str;

            string[] words = str.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].First().ToString().ToLower() + words[i].Substring(1);
            }

            return string.Join(".", words);
        }
    }
}
