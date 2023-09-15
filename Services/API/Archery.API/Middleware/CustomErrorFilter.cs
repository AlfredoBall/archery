namespace Archery.API.Middleware
{
    public class CustomErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            error = error.RemoveExtensions().RemoveLocations().RemoveCode().RemovePath().RemoveSyntaxNode();

            //return error.WithMessage(error.Message);

            return ErrorBuilder.FromError(error).Build();
        }
    }
}