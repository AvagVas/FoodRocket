using System;
using System.Threading.Tasks;
using Convey.HTTP;
using Convey.Secrets.Vault;
using Convey.WebApi.Security;
using FoodRocket.Services.Inventory.Application.DTO;
using FoodRocket.Services.Inventory.Application.Services.Clients;

namespace FoodRocket.Services.Inventory.Infrastructure.Services.Clients
{
    internal sealed class OrdersServiceClient : IOrdersServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public OrdersServiceClient(IHttpClient httpClient, HttpClientOptions options,
            ICertificatesService certificatesService, VaultOptions vaultOptions, SecurityOptions securityOptions)
        {
            _httpClient = httpClient;
            _url = options.Services["customers"];
            if (!vaultOptions.Enabled || vaultOptions.Pki?.Enabled != true)
            {
                return;
            }

            var certificate = certificatesService.Get(vaultOptions.Pki.RoleName);
            if (certificate is null)
            {
                return;
            }

            var header = securityOptions.Certificate.GetHeaderName();
            var certificateData = certificate.GetRawCertDataString();
            _httpClient.SetHeaders(h => h.Add(header, certificateData));
        }
    }
}