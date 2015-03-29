using System.Threading.Tasks;
using System.Web.Cors;
using Microsoft.Owin;
using Microsoft.Owin.Cors;

namespace FagdagCqrs.Backend.Bootstrapping
{
    public class FagdagCqrsCorsProvider : ICorsPolicyProvider
    {
        public Task<CorsPolicy> GetCorsPolicyAsync(IOwinRequest request)
        {
            return Task.Run(() => new CorsPolicy
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true,
                AllowAnyOrigin = true,
                SupportsCredentials = true
            });
        }
    }
}