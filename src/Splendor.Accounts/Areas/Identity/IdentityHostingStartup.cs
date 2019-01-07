using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Splendor.Accounts.Data;
using Splendor.Accounts.Models;

[assembly: HostingStartup(typeof(Splendor.Accounts.Areas.Identity.IdentityHostingStartup))]
namespace Splendor.Accounts.Areas.Identity {
    public class IdentityHostingStartup : IHostingStartup {
        public void Configure(IWebHostBuilder builder) {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}