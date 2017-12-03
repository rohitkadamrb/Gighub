using Gighub.Core;
using Gighub.Core.Dtos;
using Gighub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace Gighub.Controllers.api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;


        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            var following = (_unitOfWork.Followings.GetAnyFollowings(dto.FolloweeId, userId));
            if (following != null)
                return BadRequest("Following already exists.");
            following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };
            _unitOfWork.Followings.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var userId = User.Identity.GetUserId();
            var following = (_unitOfWork.Followings.GetAnyFollowings(id, userId));
            if (following == null)
                return NotFound();
            _unitOfWork.Followings.Cancel(following);
            _unitOfWork.Complete();
            return Ok(id);
        }
    }
}
