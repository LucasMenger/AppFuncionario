using Api.Common.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDocumentation();
builder.AddCrossOrigin(); 

builder.AddSecurity();
builder.AddServices();


var app = builder.Build();

if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        options.RoutePrefix = "swagger"; 
    });
}

app.UseCors("CorsPolicy");

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1");
    c.RoutePrefix = string.Empty; 
    c.DocumentTitle = "Documentação da API - Empresa Navcode"; 
    c.InjectStylesheet("/swagger-ui/custom.css"); 
    c.InjectJavascript("/swagger-ui/custom.js"); 
});

app.UseSwagger();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();