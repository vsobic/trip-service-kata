using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
	[TestClass]
	public class TripServiceShould
	{
		private static User.User _loggedInUser;
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
			_loggedInUser = _registeredUser;
		}

		[TestMethod]
		[ExpectedException(typeof (UserNotLoggedInException))]
		public void ThrowAnExceptionWhenUserIsNotLoggedIn()
		{
			_loggedInUser = _guest;

			_tripService.GetTripsByUser(_unusedUser);
		}

		[TestMethod]
		public void NotReturnAnyTripsWhenUsersAreNotFriends()
		{
			var friend = new User.User();
			friend.AddFriend(_anotherUser);
			friend.AddTrip(_toBrasil);

			var friendTrips = _tripService.GetTripsByUser(friend);

			Assert.AreEqual(friendTrips.Count, 0);
		}

		[TestMethod]
		public void ReturnFriendTripsWhenUsersAreFriends()
		{
			var friend = new User.User();
			friend.AddFriend(_anotherUser);
			friend.AddFriend(_loggedInUser);
			friend.AddTrip(_toBrasil);
			friend.AddTrip(_toNoviSad);

			var friendTrips = _tripService.GetTripsByUser(friend);

			Assert.AreEqual(friendTrips.Count, 2);
		}

		private class TestableTripService : TripService
		{
			protected override User.User GetLoggedInUser()
			{
				return _loggedInUser;
			}

			protected override List<Trip.Trip> TripsBy(User.User user)
			{
				return user.Trips();
			}
		}
	}
}