using Gighub.Core;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Gighub.Controllers
{
    public class FolloweesController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public FolloweesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var artists = _unitOfWork.Users.GetAllFollowersAsPerId(userId);

            return View(artists);
        }
    }
}