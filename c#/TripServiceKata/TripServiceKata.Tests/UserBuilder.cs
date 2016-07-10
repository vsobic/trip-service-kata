namespace TripServiceKata.Tests
{
	public class UserBuilder
	{
		private User.User[] _friends = {};
		private Trip.Trip[] _trips = {};

		public UserBuilder User()
		{
			return new UserBuilder();
		}

		public UserBuilder FriendsWith(params User.User[] friends)
		{
			_friends = friends;
			return this;
		}

		public UserBuilder WithTrips(params Trip.Trip[] trips)
		{
			_trips = trips;
			return this;
		}

		public User.User Build()
		{
			var user = new User.User();
			AddTripsTo(user);
			AddFriends(user);
			return user;
		}

		private void AddFriends(User.User user)
		{
			foreach (var friend in _friends)
			{
				user.AddFriend(friend);
			}
		}

		private void AddTripsTo(User.User user)
		{
			foreach (var trip in _trips)
			{
				user.AddTrip(trip);
			}
		}
	}
}