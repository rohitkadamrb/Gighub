using AutoMapper;
using Gighub.Core;
using Gighub.Core.Dtos;
using Gighub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace Gighub.Controllers.api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private IUnitOfWork _unitOfWork;


        public NotificationsController(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }
        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _unitOfWork.Notifications.GetAllNotifications(userId);


            return notifications.Select(Mapper.Map<Notification, NotificationDto>);


        }
        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.Notifications.GetUserNotifications(userId);

            notifications.ForEach(n => n.Read());
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
