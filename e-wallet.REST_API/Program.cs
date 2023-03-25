using e_wallet.REST_API.DataContexts;
using Microsoft.EntityFrameworkCore;
using e_wallet.REST_API.Controllers;
using e_wallet.REST_API.Services;
using Microsoft.IdentityModel.Tokens;

const string jwtSchemeName = "JwtBearer";
const string SECURITY_KEY = "0d5b3235a8b403c3dab9c3f4f65c07fcalskd234n1k41230";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var signingSecurityKey = new SigningSecurityKey(SECURITY_KEY, SecurityAlgorithms.HmacSha256);
builder.Services.AddSingleton<ISigningSecurityKey>(signingSecurityKey);
builder.Services.AddDbContext<AuthenticationContext>(options => options.UseSqlite("Data Source = Identity.db"));
builder.Services.AddDbContext<EWalletContext>(options => options.UseSqlite("Data Source = EWallet.db"));

builder.Services.AddAuthentication(options => { options.DefaultAuthenticateScheme = jwtSchemeName; options.DefaultChallengeScheme = jwtSchemeName; })
    .AddJwtBearer(jwtSchemeName, options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = "e_wallet",

            ValidateAudience = true,
            ValidAudience = "e_wallet_Audience",

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingSecurityKey.GetKey(),

            ValidateLifetime = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
