using IssueTrackerApi;
using IssueTrackerApi.Adapters;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dataConnectionString = builder.Configuration.GetConnectionString("data") ?? throw new ArgumentException("Need a data connection string");

builder.Services.AddMarten(options =>
{
    options.Connection(dataConnectionString);
    
    if(builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
    }

});

var businessApiUri = builder.Configuration.GetValue<string>("business-api") ?? throw new ArgumentException("Need a URI for the Business API");

builder.Services.AddHttpClient<BusinessApiAdapter>(client =>
{
    client.BaseAddress = new Uri(businessApiUri);
}).AddPolicyHandler(SrePolicies.GetDefaultRetryPolicy(1))
.AddPolicyHandler(SrePolicies.GetDefaultCircuitBreaker());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
