
using Contract;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebApiMassTransit.Models;

namespace WebApiMassTransit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IService, Service>();
            services.AddScoped<ConsumerSaveMessageCommand>();

            services.AddMassTransit(x =>
            {
                // add the consumer to the container
                x.AddConsumer<ConsumerSaveMessageCommand>();

            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://rabbitmq3-management/"), h =>
                {
                    h.Username("NORUSDEV");
                    h.Password("teste1234");
                });

                //cfg.UseExtensionsLogging(provider.GetService<ILoggerFactory>());
                //cfg.UseJsonSerializer();

                cfg.ReceiveEndpoint(host, "ISaveMessageCommand", e =>
                {
                    //e.Bind("SalveFileCommand");
                    //e.Bind<IMessageText>();
                    e.PrefetchCount = 1;
                    //e.UseMessageRetry(x => x.Interval(1, 1));
                    e.Consumer<ConsumerSaveMessageCommand>(provider);
                    EndpointConvention.Map<ISaveMessageCommand>(e.InputAddress);
                });
            }));

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<ISaveMessageCommand>());

            services.AddSingleton<IHostedService, BusService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
