using LazyLoadingEagerLoading.Data;
using LazyLoadingEagerLoading.Exceptions;
using LazyLoadingEagerLoading.Mappers;
using LazyLoadingEagerLoading.Repositories;
using LazyLoadingEagerLoading.Services;
using Microsoft.EntityFrameworkCore;
//using System.Text.Json.Serialization;


//UseLazyLoadingProxies()-> it tells entity framework to create helper objects for your entity classes
//when you try to access related entity eg-> author from book that hasn not been loaded yet and helper automatically fetches data from database
//and these saves resources by loading related data only when it is needed
//without UseLazyLoadingProxies() lazy loading will not work even if you have virtual properties
namespace LazyLoadingEagerLoading
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddDbContext<LibraryContext>(options =>
            {
                options.UseLazyLoadingProxies(); //enable lazy loading proxies
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnString"));
            });

            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddTransient<IBookService, BookService>();
            builder.Services.AddTransient<IAuthorService, AuthorService>();

            builder.Services.AddControllers();

            //builder.Services.AddControllers().AddJsonOptions(x =>
            //{
            //    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            //});
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddExceptionHandler<AppExceptionHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseExceptionHandler(_ => { });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
