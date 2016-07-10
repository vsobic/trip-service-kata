using System.Collections.Generic;
using TripServiceKata.Exception;

namespace TripServiceKata.Trip
{
	public class TripService
	{
		private readonly ITripDao _tripDao;
		private static List<Trip> NoTrips => new List<Trip>();

		public TripService(ITripDao tripDao)
		{
			_tripDao = tripDao;
		}

		public List<Trip> GetFriendTrips(User.User friend, User.User loggedInUser)
		{
			ValidateLoggedInUser(loggedInUser);

			return friend.IsFriendsWith(loggedInUser) ? TripsBy(friend) : NoTrips;
		}

		private static void ValidateLoggedInUser(User.User loggedInUser)
		{
			if (loggedInUser == null)
			{
				throw new UserNotLoggedInException();
			}
		}

		private List<Trip> TripsBy(User.User user)
		{
			return _tripDao.TripsBy(user);
		}
	}
}