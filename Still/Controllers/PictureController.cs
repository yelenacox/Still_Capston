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
    public class PictureController : ControllerBase
    {

        private readonly IPictureRepository _pictureRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        public PictureController(
            IPictureRepository pictureRepository,
            IUserProfileRepository userProfileRepository)
        {
            _pictureRepository = pictureRepository;
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pictureRepository.GetAllPictures());
        }

        [HttpGet("UserPictures")]
        public IActionResult GetPicturesByUser() 
        {
            var currentUserProfile = GetCurrentUserProfile();
            var userPictures = _pictureRepository.GetUserPictures(currentUserProfile.Id);
            if(userPictures == null)
            {
                NotFound();
            }
            return Ok(userPictures);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var currentUserProfile = GetCurrentUserProfile();
            var picture = _pictureRepository.GetPictureById(id);
            if (picture.UserProfileId == currentUserProfile.Id)
            {
                return Ok(picture);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(Picture picture)
        {
            var currentUserProfile = GetCurrentUserProfile();
            picture.UserProfileId = currentUserProfile.Id;

            _pictureRepository.Add(picture);
            return CreatedAtAction(nameof(Get), picture);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, Picture picture)
        {
            if (id != picture.Id)
            {
                return BadRequest();
            }
            _pictureRepository.Update(picture);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        { 
           _pictureRepository.Delete(id);
           return NoContent();
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}

