namespace ApiPetShop.Infra
{
    public class UrlService(IHttpContextAccessor contextAccessor)
    {
        public string GetApiUrl() => $"{contextAccessor.HttpContext?.Request.Scheme}://{contextAccessor.HttpContext?.Request.Host}";
    }
}