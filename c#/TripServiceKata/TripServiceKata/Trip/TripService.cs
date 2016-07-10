using System.Collections.Generic;
using TripServiceKata.Exception;

namespace TripServiceKata.Trip
{
	public class TripService
	{
		private static List<Trip> NoTrips => new List<Trip>();

		public List<Trip> GetTripsByUser(User.User user, User.User loggedInUser)
		{
			if (loggedInUser == null)
			{
				throw new UserNotLoggedInException();
			}

			return user.IsFriendsWith(loggedInUser) ? TripsBy(user) : NoTrips;
		}

		protected virtual List<Trip> TripsBy(User.User user)
		{
			return TripDAO.FindTripsByUser(user);
		}
	}
}