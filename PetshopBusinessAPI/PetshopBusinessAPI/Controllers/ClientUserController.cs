using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetshopBusinessAPI.Models;
using PetshopBusinessAPI.Services;
using PetshopBusinessAPI.ViewModels;
using PetshopDB.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PetshopBusinessAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName ="v1")]//Se for v2, exibiria somente os metodos da versao 2
    //[ApiVersion("2.0")] => um controlador da pra suportar mais de uma versão
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientUserController : ControllerBase
    {
        //private readonly SignInManager<ClientUser> _signInManager;
        private readonly PetshopDbContext _context;

        public ClientUserController(PetshopDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        //[ProducesResponseType(statusCode:200, Type = typeof(retorno do endpoint))]
        [ProducesResponseType(statusCode:500, Type = typeof(ErrorResponse))]
        [ProducesResponseType(statusCode: 404)]//Esses caras implementam UI de request no swagger
        [SwaggerOperation(Summary = "Gera um jwt token para autenticação")]
        //[AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] VMLogin model)
        {
            //var user = _context.ClientUser.Find(model.ClientUserId);
            var user = _context.ClientUser.Where(t => t.Login == model.Login && t.Password == model.Password).FirstOrDefault();

            if (user == null)
            {
                return NotFound( new { message = "Usuario ou senha está incorreto." });
            }

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
            //return BadRequest(ErrorResponse.FromModelState(ModelState)); => modelo para bad request
        }

        [HttpPost]//tirar a tag qq coisa
        [SwaggerOperation(Summary = "Cria um novo usuário", Tags = new[] { "ClientUser" })]
        [ProducesResponseType(statusCode: 200, Type = typeof(ClientUser))]
        [ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponse))]
        [ProducesResponseType(404)]
        //public async Task<ActionResult<ClientUser>> CreateUser([FromBody] VMLogin clientUser)
        public async Task<ActionResult<dynamic>> CreateUser([FromBody] [SwaggerParameter("ClientUser a ser criado")]  VMLogin clientUser)
        {
            var user = _context.ClientUser.Where(t => t.Login == clientUser.Login && t.Password == clientUser.Password).FirstOrDefault();

            if(user == null)
            {
                user = new ClientUser()
                {
                    InsertDate = DateTime.Now,
                    Login = clientUser.Login,
                    Password = clientUser.Password,
                    Name = clientUser.Name,
                };

                //clientUser.InsertDate = DateTime.Now;
                _context.ClientUser.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {

            }

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };

            //return CreatedAtAction("GetUserApp", new { id = user.ClientUserId }, user);
            //return Created();
        }

        [HttpGet]
        //[ProducesResponseType(statusCode: 200, Type = typeof(ClientUser))]
        //[ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponse))]
        //[ProducesResponseType(statusCode: 404)]
        public ActionResult<IEnumerable<ClientUser>> GetUserApps()
        {
            return _context.ClientUser.ToList();
        }

        [HttpGet("{id}")]
        //[ProducesResponseType(statusCode: 200, Type = typeof(UserApp))]
        //[ProducesResponseType(statusCode: 500, Type = typeof(ErrorResponse))]
        //[ProducesResponseType(statusCode: 404)]
        public async Task<ActionResult<ClientUser>> GetUserApp(int id)
        {
            var userApp = await _context.ClientUser.FindAsync(id);

            if (userApp == null)
            {
                return NotFound();
            }

            return userApp;
        }

        //[HttpPost]
        //public async Task<IActionResult> Token(ClientUser clientUser)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(clientUser.Login, clientUser.Password, true, true);
        //        if(result.Succeeded)
        //        {
        //            var direitos = new[]
        //            {
        //                new Claim(JwtRegisteredClaimNames.Sub, clientUser.Login),
        //                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //            };

        //            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("petshop-apiweb-authentication-valid"));
        //            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //            var token = new JwtSecurityToken(
        //                issuer: "PetshopBusiness",
        //                audience: "Postman",
        //                claims: direitos,
        //                signingCredentials: credentials,
        //                expires: DateTime.Now.AddMinutes(30)

        //            );

        //            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


        //            return Ok(token);
        //        }
        //        return Unauthorized();//401
        //    }

        //    return BadRequest();//400
        //}
    }
}