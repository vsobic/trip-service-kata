using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TripServiceKata.Tests
{
	[TestClass]
	public class UserShould
	{
		private readonly User.User _bob = new User.User();
		private readonly User.User _paul = new User.User();

		[TestMethod]
		public void InfromWhenUsersAreNotFriends()
		{
			var user = new UserBuilder()
				.User()
				.FriendsWith(_bob)
				.Build();

			Assert.IsFalse(user.IsFriendsWith(_paul));
		}

		[TestMethod]
		public void InfromWhenUsersAreFriends()
		{
			var user = new UserBuilder()
				.User()
				.FriendsWith(_bob, _paul)
				.Build();

			Assert.IsTrue(user.IsFriendsWith(_paul));
		}
	}
}