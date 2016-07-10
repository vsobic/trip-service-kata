using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
		private readonly User.User _unusedUser = null;
		private TestableTripService _tripService;

		[TestInitialize]
		public void TestInitialize()
		{
			_tripService = new TestableTripService();
		}

		[TestMethod]
		[ExpectedException(typeof (UserNotLoggedInException))]
		public void ThrowAnExceptionWhenUserIsNotLoggedIn()
		{
			_tripService.GetTripsByUser(_unusedUser, _guest);
		}

		[TestMethod]
		public void NotReturnAnyTripsWhenUsersAreNotFriends()
		{
			var friend = new UserBuilder()
				.User()
				.WithTrips(_toBrasil)
				.FriendsWith(_anotherUser)
				.Build();

			var friendTrips = _tripService.GetTripsByUser(friend, _registeredUser);

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

			var friendTrips = _tripService.GetTripsByUser(friend, _registeredUser);

			Assert.AreEqual(friendTrips.Count, 2);
		}

		private class TestableTripService : TripService
		{
			protected override List<Trip.Trip> TripsBy(User.User user)
			{
				return user.Trips();
			}
		}
	}
}