using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Still.Models;
using Still.Repositories;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
namespace Still.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly IPageRepository _pageRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public PageController(
            IPageRepository pageRepository, 
            IUserProfileRepository userProfileRepository)
        {
            _pageRepository = pageRepository;
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var currentUserProfile = GetCurrentUserProfile();
            var userPages = _pageRepository.GetAllPagesByUser(currentUserProfile.Id);
            if(userPages == null)
            {
                NotFound();
            }
            return Ok(userPages);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var currentUserProfile = GetCurrentUserProfile();
            var page = _pageRepository.GetPageById(id);
            if(page.UserProfileId != currentUserProfile.Id)
            {
               return NotFound();
            }
            return Ok(page);
        }
        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}
