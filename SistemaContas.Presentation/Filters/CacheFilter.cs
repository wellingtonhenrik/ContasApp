namespace ContasApp.Presentation.Filters
{

    public class CacheFilter
    {
        private readonly RequestDelegate m_next;

        public CacheFilter(RequestDelegate next)
        {
            m_next = next;
        }

        //Comando para o navegador não guardar cache na pagina
        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.OnStarting((state) =>
            {
                httpContext.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
                httpContext.Response.Headers.Append("Pragma", "no-cache");
                httpContext.Response.Headers.Append("Expires", "0");

                return Task.FromResult(0);
            }, null);

            await m_next.Invoke(httpContext);
        }
    }
}
