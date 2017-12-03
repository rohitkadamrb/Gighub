
var FollowingsController = function (followingService) {
    var button;
    var fail = function () { alert("Something Failed !"); };

    var done = function () {
          
       
       var text = (button.text() === "Following") ? "Follow" : "Following";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var toggleFollowing = function (e) {

        button = $(e.target);
        var followeeId = button.attr("data-user-id");
        if (button.hasClass("btn-default"))
            followingService.createFollowing(followeeId, done, fail);

        else
            followingService.deletefollowing(followeeId, done, fail);

    };

    var init = function () {
        $(".js-toggle-follow").click(toggleFollowing);
    };
   
       
        return {
            init: init
        };

}(FollowingService);