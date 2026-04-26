using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace StudentManagementAzureFunctions.ServiceBusTrigger;

public class ServiceBusTrigger
{
    private readonly ILogger<ServiceBusTrigger> _logger;

    public ServiceBusTrigger(ILogger<ServiceBusTrigger> logger)
    {
        _logger = logger;
    }

    [Function(nameof(ServiceBusTrigger))]
    public async Task Run(
        [ServiceBusTrigger("studentqueue", Connection = "ServiceBusConnection")]
        string message)
    {
        _logger.LogInformation($"Received: {message}");
        
    }
}