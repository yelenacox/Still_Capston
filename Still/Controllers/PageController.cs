using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Still.Models;
using Still.Repositories;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

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

        [HttpPost]
        public IActionResult Post(RequestParams requestParams)
        {
            Page page = requestParams.page;
            List<int> pictureIds = requestParams.pictureIds;

            var currentUserProfile = GetCurrentUserProfile();
            page.UserProfileId = currentUserProfile.Id;

            int pageId = _pageRepository.Add(page);
            foreach (var pictureId in pictureIds)
            {
                PagePicture pp = new PagePicture();
                pp.PictureId = pictureId;
                pp.PageId= pageId;
                _pageRepository.AddPagePicture(pp);
            }
            return CreatedAtAction(nameof(Get), page);
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
    public class RequestParams
    {
        public Page page { get; set; }
        public List<int> pictureIds { get; set; }
    }
}
