using xeno_canto.Components;
using XenoCanto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//recordings?query=loc:%22Surrey,%20England%22+grp:birds

//builder.Services.AddHttpClient("XenoCantoAPI", client => client.BaseAddress = new Uri(builder.Configuration["XenoCantoUrl"] ?? "\\"));


// Register the app's typed HttpClient for the typed client component example
builder.Services.AddHttpClient<XenoCantoHttpClient>(client => client.BaseAddress = new Uri(builder.Configuration["XenoCantoUrl"] ?? "\\"));


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
