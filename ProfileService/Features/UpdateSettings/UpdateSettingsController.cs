using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Common.BaseEndPoint;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Features.UpdateSettings.Orchestrator;
using ProfileService.Features.UpdateSettings.ViewModels;

namespace ProfileService.Features.UpdateSettings
{
    [ApiController]
    [Route("api/v1/settings")]
    [AllowAnonymous]
    public class UpdateSettingsController : BaseController
    {
        /// <summary>
        /// Partially updates the user's settings. Only the supplied fields will be modified.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(ResponseViewModel<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseViewModel<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel<bool>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseViewModel<bool>>> UpdateSettings(
            [FromBody] UpdateSettingsRequestViewModel request,
            CancellationToken cancellationToken)
        {
            var userId = CurrentUser.UserId;

            if (userId == null)
            {
                return Unauthorized(ResponseViewModel<bool>.Failure(
                    "AUTH_TOKEN_INVALID",
                    ErrorCode.AuthTokenInvalid));
            }

            var result = await Mediator.Send(new UpdateSettingsOrchestrator(userId.Value, request), cancellationToken);

            if (!result.isSuccess)
            {
                if (result.errorCode == ErrorCode.ProfileNotFound)
                {
                    return NotFound(ResponseViewModel<bool>.Failure(result.message, result.errorCode));
                }

                return BadRequest(ResponseViewModel<bool>.Failure(result.message, result.errorCode));
            }

            return Ok(ResponseViewModel<bool>.Success(true, "Settings updated successfully."));
        }
    }
}
