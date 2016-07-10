using System.Collections.Generic;

namespace TripServiceKata.Trip
{
	public interface ITripDao
	{
		List<Trip> TripsBy(User.User user);
	}
}