using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
	[TestClass]
	public class TripServiceShould
	{
		private readonly User.User _anotherUser = new User.User();
		private readonly User.User _guest = null;
		private readonly User.User _registeredUser = new User.User();
		private readonly Trip.Trip _toBrasil = new Trip.Trip();
		private readonly Trip.Trip _toNoviSad = new Trip.Trip();

		private readonly Mock<ITripDao> _tripDao = new Mock<ITripDao>();
		private readonly User.User _unusedUser = null;

		private TripService _tripService;

		[TestInitialize]
		public void TestInitialize()
		{
			_tripService = new TripService(_tripDao.Object);
		}

		[TestMethod]
		[ExpectedException(typeof (UserNotLoggedInException))]
		public void ThrowAnExceptionWhenUserIsNotLoggedIn()
		{
			_tripService.GetFriendTrips(_unusedUser, _guest);
		}

		[TestMethod]
		public void NotReturnAnyTripsWhenUsersAreNotFriends()
		{
			var friend = new UserBuilder()
				.User()
				.WithTrips(_toBrasil)
				.FriendsWith(_anotherUser)
				.Build();

			var friendTrips = _tripService.GetFriendTrips(friend, _registeredUser);

			Assert.AreEqual(friendTrips.Count, 0);
		}

		[TestMethod]
		public void ReturnFriendTripsWhenUsersAreFriends()
		{
			var friend = new UserBuilder()
				.User()
				.FriendsWith(_anotherUser, _registeredUser)
				.WithTrips(_toBrasil, _toNoviSad)
				.Build();

			_tripDao.Setup(t => t.TripsBy(friend)).Returns(friend.Trips);

			var friendTrips = _tripService.GetFriendTrips(friend, _registeredUser);

			Assert.AreEqual(friendTrips.Count, 2);
		}
	}
}