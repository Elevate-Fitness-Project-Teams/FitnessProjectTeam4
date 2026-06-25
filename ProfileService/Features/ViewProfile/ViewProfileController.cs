using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Common.BaseEndPoint;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Common.Services.Interfaces;
using ProfileService.Features.ViewProfile.Orchestrators;
using ProfileService.Features.ViewProfile.ViewModels;

namespace ProfileService.Features.ViewProfile
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class ProfileController : BaseController
    {
        /// <summary>
        /// Retrieves the profile data and workout statistics for the current user (View Profile).
        /// </summary>
        /// <remarks>
        /// You do not need to pass an identifier (UserId) in the URL. 
        /// The system automatically extracts the user's ID from the token (or the current Mock service), 
        /// and then merges the basic profile data with the statistics (Total Workouts, Current Streak) 
        /// to return them in a single, unified object.
        /// </remarks>
        /// <returns>
        /// A standard response object (ResponseViewModel) containing the request status, message, and the final data (ProfileViewModel).
        /// </returns>
        /// <response code="200">
        /// Profile data retrieved successfully.
        /// </response>
        /// <response code="400">
        /// A logical or validation error occurred while processing the request (BadRequest).
        /// </response>
        /// <response code="401">
        /// Unauthorized user (the token is missing or invalid).
        /// </response>
        /// <response code="404">
        /// No registered profile was found for this user.
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseViewModel<ProfileViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel<ProfileViewModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseViewModel<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseViewModel<ProfileViewModel>>> GetProfile()
        {
            var userId = CurrentUser.UserId;

            if (userId == null)
            {
                return Unauthorized(ResponseViewModel<ProfileViewModel>.Failure(
                    "AUTH_TOKEN_INVALID",
                    ErrorCode.AuthTokenInvalid));
            }

            var result = await Mediator.Send(new ViewProfileOrchestrator(userId.Value));

            if (!result.isSuccess)
            {
                if (result.errorCode == ErrorCode.ProfileNotFound)
                {
                    return NotFound(ResponseViewModel<ProfileViewModel>.Failure(
                        result.message,
                        result.errorCode));
                }

                return BadRequest(ResponseViewModel<ProfileViewModel>.Failure(
                    result.message,
                    result.errorCode));
            }

            return Ok(ResponseViewModel<ProfileViewModel>.Success(result.data));
        }
    }
}
