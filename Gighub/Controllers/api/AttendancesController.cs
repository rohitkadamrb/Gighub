using Gighub.Core;
using Gighub.Core.Dtos;
using Gighub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace Gighub.Controllers.api
{


    [Authorize]
    public class AttendancesController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public AttendancesController(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }


        [HttpPost]

        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_unitOfWork.Attendances.AnyAttendance(dto.GigId, userId))
            {
                return BadRequest("The attendance already exists");
            }
            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId

            };
            _unitOfWork.Attendances.Add(attendance);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendances(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _unitOfWork.Attendances.GetIfAnyAttendance(id, userId);
            if (attendance == null)
            {
                return NotFound();
            }
            _unitOfWork.Attendances.Cancel(attendance);
            _unitOfWork.Complete();
            return Ok(id);
        }
    }
}
