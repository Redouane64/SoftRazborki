namespace SoftRazborki.WebApi.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using SoftRazborki.WebApi.Models;

    public class DefaultUsersService : IUsersService
    {

        private readonly IHashingService hashingService;
        private readonly IConfiguration configuration;
        private readonly JwtTokenOptions jwtOptions;

        public DefaultUsersService(IHashingService hashingService, IConfiguration configuration, IOptionsSnapshot<JwtTokenOptions> jwtOptions)
        {
            this.hashingService = hashingService;
            this.configuration = configuration;
            this.jwtOptions = jwtOptions.Value;
        }

        public async Task<string> Login(string username, string password)
        {
            if (username is null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                var user = await db.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE username = @username", new { username });

                if (user is null)
                {
                    return null;
                }

                string hash = this.hashingService.Hash(password);

                if(!hash.Equals(user.Password, StringComparison.Ordinal))
                {
                    return null;
                }

                return GenerateToken(new[] { new Claim(ClaimTypes.NameIdentifier, user.Guid.ToString()) });
            }
        }

        public async Task Register(string username, string password)
        {
            if (username is null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var user = new User
            {
                Guid = Guid.NewGuid(),
                Username = username,
                Password = this.hashingService.Hash(password)
            };

            using(IDbConnection db = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                _ = await db.ExecuteAsync("INSERT INTO Users(guid, username, password) VALUES (@Guid, @Username, @Password)", user);
            }
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtOptions.LifeTime),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
