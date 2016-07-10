using System.Collections.Generic;
using TripServiceKata.Exception;

namespace TripServiceKata.Trip
{
	public class TripDao : ITripDao
	{
		public List<Trip> TripsBy(User.User user)
		{
			return FindTripsByUser(user);
		}

		public static List<Trip> FindTripsByUser(User.User user)
		{
			throw new DependendClassCallDuringUnitTestException(
				"TripDao should not be invoked on an unit test.");
		}
	}
}