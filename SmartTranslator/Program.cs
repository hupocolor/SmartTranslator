using Hjc.ChatGPT;
using Hjc.TranslatorDomain;
using Hjc.TranslatorDomain.TranslatorServices;
using Hjc.UserDomain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartTranslator.Filter;
using SmartTranslator.JWT;
using System.Text;
using WebDemo.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//swagger配置
builder.Services.AddSwaggerGen(c =>
{
    var scheme = new OpenApiSecurityScheme()
    {
        Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Authorization"
        },
        Scheme = "oauth2",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    };
    c.AddSecurityDefinition("Authorization", scheme);
    var requirement = new OpenApiSecurityRequirement();
    requirement[scheme] = new List<string>();
    c.AddSecurityRequirement(requirement);
});

//过滤器注册
builder.Services.Configure<MvcOptions>(opt =>
{
    opt.Filters.Add<MyExceptionFilter>();
});


//服务注册
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITanslatorService,ChatGPTTranslate>();





//Redis连接
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetSection("Redis").GetSection("host").Value;
    opt.InstanceName = builder.Configuration.GetSection("Redis").GetSection("name").Value;
});
builder.Services.AddSingleton<RedisUtil>();

//数据库连接
builder.Services.AddDbContext<TranslatorDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetSection("SqlServer").GetSection("TranslatorDb").Value);
});

builder.Services.AddDbContext<UserDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetSection("SqlServer").GetSection("UserDb").Value);
});
builder.Services.AddDataProtection();
//校验控制
builder.Services.AddIdentityCore<MyUser>(options =>
{
    //是否必须数字
    options.Password.RequireDigit = false;
    //是否必须小写字母
    options.Password.RequireLowercase = false;
    //是否必须特殊符号
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    //最小长度
    options.Password.RequiredLength = 6;
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
});
var idBuilder = new IdentityBuilder(typeof(MyUser), typeof(MyRole), builder.Services);
idBuilder.AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders()
    .AddRoleManager<RoleManager<MyRole>>()
    .AddUserManager<UserManager<MyUser>>();


//配置跨域
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(b =>
    {
        b.WithOrigins(new string[] { "http://localhost:5173/" })
        .AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(x =>
{
    var jwtOpt = builder.Configuration.GetSection("JWT").Get<JWTOptions>();
    byte[] keyBytes = Encoding.UTF8.GetBytes(jwtOpt.SigningKey);
    var secKey = new SymmetricSecurityKey(keyBytes);
    x.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = secKey
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//打开校验
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
