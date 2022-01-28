using FluentValidation.Results;

namespace Movies.RestApi.Response
{
    public class ValidatedResponse<TModel>  where TModel : class 
    {
        public ValidatedResponse(ValidationResult validationResult, TModel model)
        {
            ValidationResult = validationResult;
            Result = model;
        }

        public TModel Result { get; }
        public ValidationResult ValidationResult { get; }
    }
}
