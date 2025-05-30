﻿using BookNest.Dtos.ChallengeDtos;
using BookNest.Models.Entities;
using BookNest.Services;
using BookNest.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("/api/challenges")]
    public class ChallengeController : ControllerBase
    {
        private readonly ChallengeService _challengeService;
        public ChallengeController(ChallengeService challengeService)
        {
            _challengeService = challengeService;
        }


        //TODO: Implement the dynamic progress of each challenge based on the bookprogress between challenge dates
        [HttpGet]
        public async Task<IBaseResponse> GetChallengesByUser()
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var challenges = await _challengeService.GetByUser(userId);
            return BaseResponse<List<Challenge>>.SuccessResponse(challenges);
        }

        [HttpGet("{id}")]
        public async Task<IBaseResponse> GetChallengeById(int id)
        {

            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var challenge = await _challengeService.GetById(id, userId);
            int progress;
            if (challenge.Type == "pages")
                progress = await _challengeService.GetPagesBetweenDates(userId, id, challenge.StartedAt, challenge.EndsAt);
            else
                progress = await _challengeService.GetBooksBetweenDates(userId, id, challenge.StartedAt, challenge.EndsAt);
            var response = new ChallengeResponseDto(challenge, progress);
            await _challengeService.Update(id, userId, response.IsCompleted);
            return BaseResponse<ChallengeResponseDto>.SuccessResponse(response);        
        }

        [HttpPost]
        public async Task<IBaseResponse> Create(ChallengeDto challengeDto)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var challenge = await _challengeService.Create(challengeDto, userId);
            return BaseResponse<Challenge>.SuccessResponse(challenge);
        }

        //TODO: Update the logic of progress
        //[HttpPut("{id}")]
        //public async Task<IBaseResponse> Update(int id) {
        //    var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    var challenge = await GetChallengeById(id);
        //    var updated = await _challengeService.Update(id,userId,);
        //    return BaseResponse<Challenge>.SuccessResponse(updated);
        //}

    }
}
