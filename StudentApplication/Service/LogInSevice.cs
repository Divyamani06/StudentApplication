using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentApplication.Authentication;
using StudentApplication.Context;
using StudentApplication.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StudentApplication.Service
{
    public class LogInSevice : ILogInSevice
    {
        private readonly JwtToken _jwtToken;
        private readonly ModelContext _context;

        public LogInSevice(IOptions<JwtToken> options, ModelContext context)
        {
            _jwtToken = options.Value;
            _context = context;

        }

        public async Task<string> SignUp(UserModel model)
        {
            var encryped = EncryptPassword(model.Password);
            var token = new UserModel
            {
                UserName = model.UserName,
                Password = encryped,
            };
            var data = await _context.LoginUser.Where(x => x.UserName == model.UserName).ToListAsync();
           if (data.Count != 0)
            {
                return "User Already Exit";
            }
            _context.LoginUser.Add(token);
          
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey =Encoding.UTF8.GetBytes(_jwtToken.Key);

            var claims = new ClaimsIdentity(new Claim[]
               {
               
               new Claim(ClaimTypes.Name,model.UserName),

               });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,

                Expires = DateTime.UtcNow.AddYears(5),

                SigningCredentials = new SigningCredentials
                                   (new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };

            var tokens = tokenHandler.CreateToken(tokenDescriptor);
            var fianleToken = tokenHandler.WriteToken(tokens);
            await _context.SaveChangesAsync();
            return fianleToken;
        }

        public async Task<string> LogInAsync(UserModel model)
        {
            //Encryption Password
            var encrrypted = EncryptPassword(model.Password);

            //Checking Email and Password
            var userlogin = await _context.LoginUser.FirstOrDefaultAsync(x => x.UserName == model.UserName);

            if (userlogin == null)
            {
                return "user not found";

            }


            if (userlogin.Password != encrrypted)
            {
                return "password missmatch";
            }

            //JWT Creation
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_jwtToken.Key);
            var claims = new ClaimsIdentity(new Claim[]
               {
               new Claim(ClaimTypes.NameIdentifier,model.Id.ToString()),
               new Claim(ClaimTypes.Name,model.UserName)
               });
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.UserName));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,

                Expires = DateTime.UtcNow.AddYears(5),

                SigningCredentials = new SigningCredentials
                                   (new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };
            var tokens = tokenHandler.CreateToken(tokenDescriptor);
            var fianleToken = tokenHandler.WriteToken(tokens);
            return fianleToken;



        }


        public static string EncryptPassword(string password)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }





    }
}
