using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Nancy;
using Owin;

namespace FagdagCqrsWeb
{
    [assembly: OwinStartup(typeof(FagdagCqrsWeb.Startup))]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileSystem = new PhysicalFileSystem(@"Scripts"),
                RequestPath = new PathString("/Scripts")
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileSystem = new PhysicalFileSystem(@"sass"),
                RequestPath = new PathString("/sass")
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileSystem = new PhysicalFileSystem(@"app"),
                RequestPath = new PathString("/app"),
                ServeUnknownFileTypes = true
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileSystem = new PhysicalFileSystem(@"Content"),
                RequestPath = new PathString("/content"),
                ServeUnknownFileTypes = true
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileSystem = new PhysicalFileSystem(@"bower_components"),
                RequestPath = new PathString("/bower_components")
            });

            app.UseStageMarker(PipelineStage.MapHandler);

            StaticConfiguration.DisableErrorTraces = false;

            app.UseNancy();
        }
    }
}