﻿using Gighub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace Gighub.Controllers.api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private IUnitOfWork _unitOfWork;


        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithUserId(id);
            if (gig == null || gig.IsCancelled)
            {
                return NotFound();
            }
            if (gig.ArtistId != userId)
            {
                return Unauthorized();

            }
            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }

    }
}
