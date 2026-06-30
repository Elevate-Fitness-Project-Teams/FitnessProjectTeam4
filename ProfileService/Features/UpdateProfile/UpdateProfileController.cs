using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Common.BaseEndPoint;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Features.UpdateProfile.Orchestrator;
using ProfileService.Features.UpdateProfile.ViewModels;

namespace ProfileService.Features.UpdateProfile
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class UpdateProfileController : BaseController
    {
        /// <summary>
        /// Updates the profile data for the currently authenticated user (or Mocked user).
        /// </summary>
        /// <remarks>
        /// The payload is dynamic; you only need to send the fields you wish to update 
        /// (FirstName, LastName, PhoneNumber). The Email field is not modifiable through this endpoint.
        /// At least one valid field must be provided.
        /// </remarks>
        /// <param name="request">The payload containing the updated profile information.</param>
        /// <returns>
        /// A standard response object (ResponseViewModel) indicating the success or failure of the update operation.
        /// </returns>
        /// <response code="200">
        /// Profile data updated successfully.
        /// </response>
        /// <response code="400">
        /// A validation error occurred (e.g., no fields were provided for update).
        /// </response>
        /// <response code="401">
        /// Unauthorized user (the user context is missing).
        /// </response>
        /// <response code="404">
        /// No registered profile was found for this user to update.
        /// </response>
        [HttpPut]
        [ProducesResponseType(typeof(ResponseViewModel<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseViewModel<bool>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseViewModel<bool>>> UpdateProfile([FromBody] UpdateProfileRequestViewModel request, CancellationToken cancellationToken)
        {
            var userId = CurrentUser.UserId;

            if (userId == null)
            {
                return Unauthorized(ResponseViewModel<bool>.Failure(
                    "AUTH_TOKEN_INVALID",
                    ErrorCode.AuthTokenInvalid));
            }

            var result = await Mediator.Send(new UpdateProfileOrchestrator(request), cancellationToken);

            if (!result.isSuccess)
            {
                if (result.errorCode == ErrorCode.ProfileNotFound)
                {
                    return NotFound(ResponseViewModel<bool>.Failure(
                        result.message,
                        result.errorCode));
                }

                return BadRequest(ResponseViewModel<bool>.Failure(
                    result.message,
                    result.errorCode));
            }

            return Ok(ResponseViewModel<bool>.Success(result.data, "Profile updated successfully."));
        }
    }
}
