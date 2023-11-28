﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EastonPartners.Domain.Entities.Settings;
using EastonPartners.Infrastructure.Persistence.DbContexts;
using EastonPartners.Web.Controllers;
using RoverCore.BreadCrumbs.Services;
using System.Threading.Tasks;
using RoverCore.Abstractions.Settings;
using EastonPartners.Infrastructure.Common.Settings.Services;

namespace EastonPartners.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ConfigurationController : BaseController<ConfigurationController>
{
    private readonly ApplicationDbContext _context;
    private readonly ISettingsService<ApplicationSettings> _settingsService;

    public ConfigurationController(ApplicationDbContext context,
        ISettingsService<ApplicationSettings> settingsService)
    {
        _context = context;
        _settingsService = settingsService;
    }

    public IActionResult Index()
    {
        _breadcrumbs.StartAtAction("Dashboard", "Index", "Home", new { Area = "Dashboard" })
            .Then("Admin")
            .ThenAction("Site Configuration", "Index", "Configuration", new { Area = "Admin" });


        var settings = _settingsService.GetSettings();

        return View(settings);
    }

    // POST: Configuration/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        [Bind("SiteName,BaseUrl,Company,ApplyMigrationsOnStartup,SeedDataOnStartup,Email,LogoImageUrlSmall")] ApplicationSettings newSettings)
    {
        var settings = _settingsService.GetSettings();

        if (ModelState.IsValid)
        {
            settings.SiteName = newSettings.SiteName;
            settings.BaseUrl = newSettings.BaseUrl;
            settings.Company = newSettings.Company;
            settings.LogoImageUrlSmall = newSettings.LogoImageUrlSmall;

            settings.ApplyMigrationsOnStartup = newSettings.ApplyMigrationsOnStartup;
            settings.SeedDataOnStartup = newSettings.SeedDataOnStartup;

            // Email settings
            settings.Email.DefaultSenderAddress = newSettings.Email.DefaultSenderAddress;
            settings.Email.DefaultSenderName = newSettings.Email.DefaultSenderName;

            /*
            // Not yet implemented - Can't change smtp settings for Mailkit without restarting
            settings.Email.Server = newSettings.Email.Server;
			settings.Email.Port = newSettings.Email.Port;
			settings.Email.User = newSettings.Email.User;
			settings.Email.Password = newSettings.Email.Password;
			settings.Email.UseSsl = newSettings.Email.UseSsl;
			settings.Email.RequiresAuthentication = newSettings.Email.RequiresAuthentication;
			settings.Email.PreferredEncoding = newSettings.Email.PreferredEncoding;
			settings.Email.UsePickupDirectory = newSettings.Email.UsePickupDirectory;
			settings.Email.MailPickupDirectory = newSettings.Email.MailPickupDirectory;
            */

		    await _settingsService.SaveSettings();

            return RedirectToAction(nameof(Index));
        }

        return View("Index", settings);
    }

}