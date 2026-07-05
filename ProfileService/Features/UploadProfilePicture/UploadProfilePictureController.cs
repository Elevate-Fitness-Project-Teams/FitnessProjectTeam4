using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Common.BaseEndPoint;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Features.UploadProfilePicture.Orchestrators;
using ProfileService.Features.UploadProfilePicture.ViewModels;

namespace ProfileService.Features.UploadProfilePicture
{
    [ApiController]
    [Route("api/v1/profile/picture")]
    [AllowAnonymous]
    public class UploadProfilePictureController : BaseController
    {
        /// <summary>
        /// Uploads and updates the profile picture for the currently authenticated user.
        /// </summary>
        /// <remarks>
        /// This endpoint expects a multipart/form-data request containing the file.
        /// Allowed extensions: .jpg, .jpeg, .png. Maximum size: 5 MB.
        /// </remarks>
        /// <param name="request">The form data containing the image file.</param>
        /// <returns>
        /// A response object containing the new URL of the uploaded profile picture.
        /// </returns>
        [HttpPut] // استخدام Put لأنه يحل محل الصورة القديمة
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ResponseViewModel<UploadProfilePictureResponseViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseViewModel<UploadProfilePictureResponseViewModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseViewModel<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseViewModel<UploadProfilePictureResponseViewModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseViewModel<UploadProfilePictureResponseViewModel>>> UploadPicture([FromForm] UploadProfilePictureRequestViewModel request, CancellationToken cancellationToken)
        {
            var userId = CurrentUser.UserId;

            if (userId == null)
            {
                return Unauthorized(ResponseViewModel<UploadProfilePictureResponseViewModel>.Failure(
                    "AUTH_TOKEN_INVALID",
                    ErrorCode.AuthTokenInvalid));
            }

            var result = await Mediator.Send(new UploadProfilePictureOrchestrator(request), cancellationToken);

            if (!result.isSuccess)
            {
                if (result.errorCode == ErrorCode.ProfileNotFound)
                {
                    return NotFound(ResponseViewModel<UploadProfilePictureResponseViewModel>.Failure(
                        result.message,
                        result.errorCode));
                }

                return BadRequest(ResponseViewModel<UploadProfilePictureResponseViewModel>.Failure(
                    result.message,
                    result.errorCode));
            }

            return Ok(ResponseViewModel<UploadProfilePictureResponseViewModel>.Success(result.data, result.message));
        }
    }
}
