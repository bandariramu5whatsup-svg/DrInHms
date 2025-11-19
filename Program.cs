using HanuMediSoftCore.Helpers;
using HanuMediSoftCore.Services.Hms.Op.Masters;
using static HanuMediSoftCore.Helpers.Helpers;

var builder = WebApplication.CreateBuilder(args);

// INIT DB CONNECTION
ConnectionHelper.Initialize(builder.Configuration);

// API CONTROLLERS
builder.Services.AddControllers();

builder.Services.AddScoped<DatabaseHelper>();
builder.Services.AddAllServices();

// SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HTTP CLIENT
builder.Services.AddHttpClient("apiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:44306/");
}).ConfigurePrimaryHttpMessageHandler(() =>
    new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDev", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("https://localhost:44306");
    });
});

// RAZOR PAGES + AUTH RULES
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");          // Protect ALL pages
    options.Conventions.AllowAnonymousToPage("/Login");
    options.Conventions.AllowAnonymousToPage("/Unauthorized");

    // Example: Only OP department: /Op folder
    options.Conventions.AuthorizeFolder("/Op");
});

// AUTHENTICATION + COOKIE LOGIN
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Unauthorized";
        options.ExpireTimeSpan = TimeSpan.FromHours(6);
    });

// AUTHORIZATION
builder.Services.AddAuthorization();

// SESSION
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

// SWAGGER
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("LocalDev");

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllers();
app.MapRazorPages();

app.Run();



////var builder = WebApplication.CreateBuilder(args);



////// Add services to the container.
////builder.Services.AddRazorPages();



////builder.Services.AddHttpClient("apiClient", client =>
////{
////    client.BaseAddress = new Uri("https://localhost:7285/");
////})
////.ConfigurePrimaryHttpMessageHandler(() =>
////    new HttpClientHandler
////    {
////        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
////    }
////);

////var app = builder.Build();

////// Configure the HTTP request pipeline.
////if (!app.Environment.IsDevelopment())
////{
////    app.UseExceptionHandler("/Error");
////    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
////    app.UseHsts();
////}

////app.UseHttpsRedirection();
////app.UseStaticFiles();

////app.UseRouting();

////app.UseAuthorization();

////app.MapRazorPages();

////app.Run();


//using static HanuMediSoftCore.Helpers.Helpers;

//var builder = WebApplication.CreateBuilder(args);
//ConnectionHelper.Initialize(builder.Configuration);

//// ENABLE API CONTROLLERS
//builder.Services.AddControllers();

//// ENABLE SWAGGER
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// HTTP CLIENT FOR CALLING API
//builder.Services.AddHttpClient("apiClient", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7285/");
//}).ConfigurePrimaryHttpMessageHandler(() =>
//    new HttpClientHandler
//    {
//        ServerCertificateCustomValidationCallback =
//            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
//    });

//// OPTIONAL CORS (only if calling from another app)
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("LocalDev", policy =>
//    {
//        policy.AllowAnyHeader()
//              .AllowAnyMethod()
//              .WithOrigins("https://localhost:7285");
//    });
//});

//// RAZOR PAGES
////builder.Services.AddRazorPages();
//builder.Services.AddRazorPages(options =>
//{
//    options.Conventions.AuthorizeFolder("/"); // Protect all pages
//    options.Conventions.AllowAnonymousToPage("/Login"); // Login page allowed
//    options.Conventions.AllowAnonymousToPage("/Unauthorized"); // Unauthorized page allowed
//    options.Conventions.AuthorizeFolder("/Op");

//});


//builder.Services.AddAuthentication("CookieAuth")
//    .AddCookie("CookieAuth", options =>
//    {
//        options.LoginPath = "/Login";
//        options.AccessDeniedPath = "/Unauthorized";
//    });

//builder.Services.AddAuthorization();

//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession();




//var app = builder.Build();

//// SWAGGER in Development
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();


//app.UseSession();

//app.MapControllers();

//// MAP RAZOR PAGES
//app.MapRazorPages();

//app.Run();
