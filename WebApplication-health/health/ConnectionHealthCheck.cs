using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication_health.health
{
    public class ConnectionHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var (IsHealthy, time) = await CheckConnection();
            if (IsHealthy)
            {
                if (time > 10)
                {
                    return HealthCheckResult.Degraded("Connection Is Poor");
                }
                return HealthCheckResult.Healthy("healthed");
            }
            else
            {
                return HealthCheckResult.Unhealthy("Unhealthy");
            }
        }
        private async Task<Tuple<bool, int>> CheckConnection()
        {
            var time1 = DateTime.Now;
            await Task.Delay(12000);
            var client = new HttpClient();
            var response = await client.GetAsync("http://uporoje.ir/");
            var time2 = DateTime.Now;
            if (response.IsSuccessStatusCode)
            {
                return new Tuple<bool, int>(true, (int)(time2 - time1).TotalSeconds);
            }
            else
            {
                return new Tuple<bool, int>(false, -1);
            }

        }
    }
}
