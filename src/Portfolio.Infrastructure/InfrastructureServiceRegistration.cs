using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Contracts.FileManagement;
using Portfolio.Application.Contracts.Infrastructure.ImageHandler;
using Portfolio.Application.Contracts.Infrastructure.Mail;
using Portfolio.Application.Models.Files;
using Portfolio.Application.Models.Mail;
using Portfolio.Infrastructure.FileManagement;
using Portfolio.Infrastructure.ImageHandler;
using Portfolio.Infrastructure.Mail;

namespace Portfolio.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailService, EmailService>();

            services.AddSingleton(x => new BlobServiceClient(configuration.GetSection("BlobStorageSettings:ConnectionString").Value));
            services.AddScoped<IFileStorageService, BlobStorageService>();


            services.AddScoped<IImageHandlerService, ImageHandlerService>();

            return services;
        }
    }
}
