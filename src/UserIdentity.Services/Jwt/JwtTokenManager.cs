using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using UserIdentity.ViewModels.Authentication.Claims;

namespace UserIdentity.Services.Jwt
{
    public class JwtTokenService : IJwtTokenService
	{
		private const string MobilePhone = "MobilePhone";
		private const string UserId = "UserId";
		private const string Email = "Email";
        private const string MclUserId = "MclUserId";
		private const string FirstName = "FirstName";
		private const string LastName = "LastName";
		private const string Username = "Username";
		private const string PhoneNumber = "PhoneNumber";
		private const string PassportNumber = "PassportNumber";
		private const string NationalInsuranceNumber = "NationalInsuranceNumber";
		private const string DateOfBirth = "DateOfBirth";

		private readonly byte[] _jwtTokenSecret;
		private readonly string _issuer;
		private readonly string _audience;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly int _jwtExpiryInDays;

		public JwtTokenService(byte[] jwtTokenSecret, string audience, IHttpContextAccessor httpContextAccessor, int jwtExpiryInDays)
		{

            //var key = Encoding.ASCII.GetBytes(configuration["jwtTokenSecret"]);
            //var audience = configuration["jwtAudience"];
            //var jwtExpiryInDays = int.Parse(configuration["jwtExpiryInDays"]);


			_jwtTokenSecret = jwtTokenSecret;
			_audience = audience;
			_httpContextAccessor = httpContextAccessor;
			_jwtExpiryInDays = jwtExpiryInDays;
		}

		public string CreateToken(TokenBreachUser user, List<Claim> claims)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			user.Roles = claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray();
			var allClaims = claims.Concat(new[]
			{
				new Claim(UserId, user.ToString()),
				new Claim(Email, user.Email),
				new Claim(Username, user.Username),
				new Claim(FirstName, user.FirstName),
				new Claim(LastName, user.LastName),
                new Claim(MclUserId, user.MclUserId),
			}).ToList();

			if (!string.IsNullOrEmpty(user.PhoneNumber))
			{
				allClaims.Add(new Claim(PhoneNumber, user.PhoneNumber));
			}
			if (!string.IsNullOrEmpty(user.PassportNumber))
			{
				allClaims.Add(new Claim(PassportNumber, user.PassportNumber));
			}
			if (!string.IsNullOrEmpty(user.NationalInsuranceNumber))
			{
				allClaims.Add(new Claim(NationalInsuranceNumber, user.NationalInsuranceNumber));
			}
			if (user.DateOfBirth.HasValue)
			{
				allClaims.Add(new Claim(DateOfBirth, user.DateOfBirth.Value.ToString("dd/MM/yyyy")));
			}
			var identity = new BreachIdentity(user, allClaims);
			var request = _httpContextAccessor.HttpContext.Request;
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				IssuedAt = DateTime.UtcNow,
				Issuer = request.Scheme + "://" + request.Host.ToString() + "/",
				Audience = _audience,
				Subject = identity,
				Expires = DateTime.UtcNow.AddDays(_jwtExpiryInDays),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtTokenSecret), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public string CreateToken(TokenBreachUser user, IEnumerable<string> roles)
		{
			var claims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
			return CreateToken(user, claims);
		}

		public BreachPrincipal ReadToken(string token, string expectedIssuer, string expectedAudience)
		{
			token = ClearCommas(token);
			var tokenHandler = new JwtSecurityTokenHandler();
			var validationOptions = new TokenValidationParameters
			{
				IssuerSigningKey = new SymmetricSecurityKey(_jwtTokenSecret),
				ValidateLifetime = true,
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidIssuer = expectedIssuer,
				ValidAudiences = expectedAudience?.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries) ?? new string[0]
			};
			Console.WriteLine("Expected audiences: " + string.Join(", ", validationOptions.ValidAudiences));
			Debug.WriteLine("Expected audiences: " + string.Join(", ", validationOptions.ValidAudiences));
			try
			{
				tokenHandler.ValidateToken(token, validationOptions, out SecurityToken st);
				if (st == null)
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				return null;
			}
			var jwtToken = tokenHandler.ReadJwtToken(token);
			var claims = jwtToken.Payload.Claims.ToList();
			var dobClaim = PopClaimValue(claims, DateOfBirth);
			var user = new TokenBreachUser
			{
				Email = PopClaimValue(claims, Email),
				Id = PopClaimValue(claims, UserId),
                MclUserId = PopClaimValue(claims, MclUserId),
				Username = PopClaimValue(claims, Username),
				FirstName = PopClaimValue(claims, FirstName),
				LastName = PopClaimValue(claims, LastName),
				PhoneNumber = PopClaimValue(claims, PhoneNumber),
				PassportNumber = PopClaimValue(claims, PassportNumber),
				NationalInsuranceNumber = PopClaimValue(claims, NationalInsuranceNumber),
				DateOfBirth = string.IsNullOrEmpty(dobClaim) ? (DateTime?)null : DateTime.ParseExact(dobClaim, "dd/MM/yyyy", CultureInfo.InvariantCulture),
				Roles = claims.Where(IsRole).Select(x => x.Value).ToArray()
			};
			var gasIdentity = new BreachIdentity(user, claims);
			return new BreachPrincipal(gasIdentity);
		}

		private string PopClaimValue(List<Claim> claims, string key)
		{
			var claimValue = claims.SingleOrDefault(x => x.Type == key);

			return claimValue?.Value;
		}

		private bool IsRole(Claim claim)
		{
			var claimType = claim.Type;
			return claimType == ClaimTypes.Role || claimType.ToLower() == "role";
		}

		private string ClearCommas(string toClear)
		{
			while (toClear.StartsWith(","))
			{
				toClear = toClear.Substring(1);
			}
			return toClear.Trim();
		}
	}
}
