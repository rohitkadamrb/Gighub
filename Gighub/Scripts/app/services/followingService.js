var FollowingService = function () {
    var createFollowing = function (followeeId, done, fail) {
        $.post("/api/followings", { followeeId: followeeId })
               .done(done)
               .fail(fail);

    };
    var deletefollowing = function (followeeId, done, fail) {
        $.ajax({
            url: "/api/followings/" + followeeId,
            method: "DELETE"
        })
     .done(done)
      .fail(fail);
    };

    return {
        createFollowing: createFollowing,
        deletefollowing: deletefollowing
    };
}();