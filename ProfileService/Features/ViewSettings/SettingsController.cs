using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Common.BaseEndPoint;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Features.ViewSettings.Orchestrators;
using ProfileService.Features.ViewSettings.ViewModels;

namespace ProfileService.Features.ViewSettings
{
    
    public class SettingsController : BaseController
    {
        /// <summary>
        /// Retrieves a grouped JSON view of User Preferences, Notification Settings, and Privacy Settings.
        /// </summary>
        /// <returns>A successful response containing all grouped settings.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseViewModel<ViewSettingsResponseViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseViewModel<ViewSettingsResponseViewModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseViewModel<ViewSettingsResponseViewModel>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseViewModel<ViewSettingsResponseViewModel>>> GetSettings(CancellationToken cancellationToken)
        {
            var userId = CurrentUser.UserId;

            if (userId == null)
            {
                return Unauthorized(ResponseViewModel<ViewSettingsResponseViewModel>.Failure(
                    "AUTH_TOKEN_INVALID",
                    ErrorCode.AuthTokenInvalid));
            }

            var result = await Mediator.Send(new ViewSettingsOrchestrator(userId.Value), cancellationToken);

            if (!result.isSuccess)
            {
                if (result.errorCode == ErrorCode.ProfileNotFound)
                {
                    return NotFound(ResponseViewModel<ViewSettingsResponseViewModel>.Failure(
                        result.message,
                        result.errorCode));
                }

                return BadRequest(ResponseViewModel<ViewSettingsResponseViewModel>.Failure(
                    result.message,
                    result.errorCode));
            }

            return Ok(ResponseViewModel<ViewSettingsResponseViewModel>.Success(result.data, result.message));
        }
    }
}