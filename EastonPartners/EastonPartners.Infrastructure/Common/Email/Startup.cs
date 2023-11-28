﻿using FluentEmail.MailKitSmtp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EastonPartners.Domain.Entities.Settings;
using EastonPartners.Infrastructure.Common.Email.Services;
using EastonPartners.Infrastructure.Common.Templates.Models;

namespace EastonPartners.Infrastructure.Common.Email;

/// <summary>
/// Configure all email services
/// </summary>
public static class Startup
{
	public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		var settings = configuration.GetSection("Settings").Get<ApplicationSettings>();

		var provider = new VirtualFileProvider();

		services.AddSingleton<VirtualFileProvider>(provider);
		services.AddFluentEmail(settings.Email.DefaultSenderAddress, settings.Email.DefaultSenderName)
			.AddLiquidRenderer(config =>
			{
				config.FileProvider = provider;
			})
			.AddMailKitSender(new SmtpClientOptions
			{
				Server = settings.Email.Server,
				Port = settings.Email.Port,
				User = settings.Email.User,
				Password = settings.Email.Password,
				UseSsl = settings.Email.UseSsl,
				RequiresAuthentication = settings.Email.RequiresAuthentication,
				PreferredEncoding = settings.Email.PreferredEncoding,
				UsePickupDirectory = settings.Email.UsePickupDirectory,
				MailPickupDirectory = settings.Email.MailPickupDirectory
			});

        services.AddTransient<IEmailSender, EmailSender>();
	}

	public static void Configure(IApplicationBuilder app, IConfiguration configuration)
	{
	}
}

