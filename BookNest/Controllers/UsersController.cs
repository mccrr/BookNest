using BookNest.Dtos.Users;
using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace BookNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService userService;
        public UsersController(UserService userService) {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IBaseResponse> GetUsers(CancellationToken cancellationToken)
        {
            try
            {
                var result = await userService.GetUsers(cancellationToken);
                return BaseResponse<List<User>>.SuccessResponse(result);
            } catch (Exception E)
            {
                return BaseResponse<object>.ErrorResponse(HttpStatusCode.InternalServerError, E.Message);
            }
        }
        [HttpGet("id/{id}")]
        public async Task<IBaseResponse> GetUserById(int id)
        {
            try { 
            var result = await userService.GetById(id);
            if (result == null) return BaseResponse<object>.ErrorResponse(HttpStatusCode.NotFound,"User Not Found");
            return BaseResponse<object>.SuccessResponse(new { result.Username, result.Age,result.FirstName,result.LastName });
            } catch(Exception E)
            {
                return BaseResponse<object>.ErrorResponse(HttpStatusCode.InternalServerError, E.Message);
            }
        }
        [HttpGet("profile")]
        public async Task<IBaseResponse> GetProfile()
        {
            if (HttpContext.User?.Claims == null || !HttpContext.User.Claims.Any())
            { return BaseResponse<object>.ErrorResponse(HttpStatusCode.Unauthorized, "You are not logged in!"); }
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                return BaseResponse<object>.ErrorResponse(HttpStatusCode.Unauthorized, "Claims were not found");
            int.TryParse(claim.Value, out int userId);
            try
            {
                var result = await userService.GetById(userId);
                if (result == null) return BaseResponse<object>.ErrorResponse(HttpStatusCode.NotFound, "User Not Found");
                return BaseResponse<object>.SuccessResponse(new ProfileDto(result));
            }
            catch (Exception E) { return BaseResponse<object>.ErrorResponse(HttpStatusCode.NotFound, E.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IBaseResponse> DeleteUser(int id)
        {
            await userService.DeleteUser(id);
            return BaseResponse<object>.SuccessResponse(null);
        }

    }
}
