using Financa.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<InserirPessoaRepository>();
builder.Services.AddScoped<ConsultaPessoaPorCpfRepository>();
builder.Services.AddTransient<PessoaRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


Financa.ConnectionSqls.ConnectionSql.Configure(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();