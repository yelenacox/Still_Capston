﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Still.Models;
using Still.Repositories;
using System;

namespace Still.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {

        private readonly IPictureRepository _pictureRepository;
        public PictureController(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pictureRepository.GetAllPictures());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var picture = _pictureRepository.GetPictureById(id);
            if (picture != null) 
            {
                NotFound();
            }
            return Ok(picture);
        }
    }
}
