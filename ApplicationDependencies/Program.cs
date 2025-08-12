// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using VacationDomain.Services;

using VacationPersistancy.Vacation;

var api = new ApplicationAdapter(args, options =>
{
    options.AddScoped<IVacationService, VacationService>();
    options.AddScoped<IVacationDbService, VacationDbService>();
});

await api.StartAsync();
