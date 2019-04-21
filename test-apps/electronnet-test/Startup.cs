using System;
using System.Linq;
using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace electronnet_test
{
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute( name: "default", template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa => {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment()) {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
            if (HybridSupport.IsElectronActive) {
                ElectronBootstrap();
            }
        }
         public async void ElectronBootstrap(){
            var opt = new BrowserWindowOptions{
                DarkTheme = true,
                Width = 1024,
                Height = 768,
                Maximizable = true,
                EnableLargerThanScreen = true,
                Show = false
            };
            var window = await Electron.WindowManager.CreateWindowAsync(opt);
            window.OnReadyToShow += () => {
                window.Show();
                window.Reload();
            };
            window.SetTitle("Electron.NET Test ddd");
            var menu = new MenuItem[] {
                new MenuItem { Label = "File", Submenu = new MenuItem[] {
                   new MenuItem {
                        Label = "Ricarica",
                        Accelerator = "CmdOrCtrl+R",
                        Click = () => {
                            Electron.WindowManager.BrowserWindows.ToList().ForEach(browserWindow => {
                                if(browserWindow.Id != 1) {
                                    browserWindow.Close();
                                } else {
                                    browserWindow.Reload();
                                }
                            });
                        }
                    },
                    new MenuItem { Type = MenuType.separator },
                    new MenuItem { Label = "Chiudi", Accelerator = "CmdOrCtrl+Q", Click = ()=>{
                        Electron.App.Exit(0);
                        Environment.Exit(0);
                    }
                    }
                }
                },
                new MenuItem { Label = "Develop Tools", Submenu = new MenuItem[] {
                   new MenuItem {
                        Label = "Open Developer Tools",
                        Accelerator = "CmdOrCtrl+I",
                        Click = () => Electron.WindowManager.BrowserWindows.First().WebContents.OpenDevTools()
                    } 
                }
                }
            };
            Electron.Menu.SetApplicationMenu(menu);
            window.OnMinimize += () => {
                if (Electron.Tray.MenuItems.Count == 0) {
                    window.Hide();
                    var mn = new MenuItem {
                        Label = "Remove",
                        Click = () => {
                            Electron.Tray.Destroy();
                            window.Show();
                        }
                    };
                    Electron.Tray.Show("/Assets/electron_32x32.png", mn);
                    Electron.Tray.SetToolTip("Electron Demo in the tray.");
                } else {
                    Electron.Tray.Destroy();
                }
            };
            Electron.WindowManager.IsQuitOnWindowAllClosed = false;

            // Emitted when all windows have been closed.
            Electron.App.WindowAllClosed += App_WindowAllClosed;
            /*window.WebContents.OnCrashed +=  async (killed) => {
                var options = new MessageBoxOptions("Questo processo è andato in errore.") {
                    Type = MessageBoxType.info,
                    Title = "L'applicazione è crashata",
                    Buttons = new string[] { "Ricarica", "Chiudi" }
                };
                var result = await Electron.Dialog.ShowMessageBoxAsync(options);
                if (result.Response == 0) {
                    window.Reload();
                } else {
                    window.Close();
                }
            };
            window.OnUnresponsive += async () => {
                var options = new MessageBoxOptions("L'applicazione è bloccata.") {
                    Type = MessageBoxType.info,
                    Title = "Blocco Applicazione",
                    Buttons = new string[] { "Ricarica", "Chiudi" }
                };
                var result = await Electron.Dialog.ShowMessageBoxAsync(options);
                if (result.Response == 0){
                    window.Reload();
                } else {
                    window.Close();
                }
            }; */
         }
        private async void App_WindowAllClosed() {
            //await Task.Run(()=>Environment.Exit(0));
            await Task.Run(()=> Electron.App.Exit(0));
        }
    }
}