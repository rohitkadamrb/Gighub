using FluentAssertions;
using Gighub.Controllers.api;
using Gighub.Core;
using Gighub.Core.Models;
using Gighub.Core.Repositories;
using Gighub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace Gighub.Tests.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {
        private GigsController _gigsController;
        private Mock<IGigRepository> _mocRepostiory;
        private string _userId;

        public GigsControllerTests()
        {
            _mocRepostiory = new Mock<IGigRepository>();

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.SetupGet(u => u.Gigs).Returns(_mocRepostiory.Object);
            _gigsController = new GigsController(mockUow.Object);
            _userId = "1";
            _gigsController.MockCurrentUser(_userId, "user@domain.com");

        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _gigsController.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }
        [TestMethod]
        public void Cancel_GigIsCAncelled_ShouldREturnotFound()
        {
            var gig = new Gig();

            gig.Cancel();
            _mocRepostiory.Setup(r => r.GetGigWithUserId(1)).Returns(gig);
            var result = _gigsController.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }
        [TestMethod]
        public void Cancel_CancellingAnotherUsersGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig { ArtistId = _userId + "_" };
            _mocRepostiory.Setup(r => r.GetGigWithUserId(1)).Returns(gig);
            var result = _gigsController.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();

        }
        [TestMethod]
        public void Cancel_ValidRequest_shouldREturnOk()
        {
            var gig = new Gig { ArtistId = _userId };
            _mocRepostiory.Setup(r => r.GetGigWithUserId(1)).Returns(gig);
            var result = _gigsController.Cancel(1);
            result.Should().BeOfType<OkResult>();
        }
    }
}
