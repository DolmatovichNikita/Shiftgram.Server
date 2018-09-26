namespace Shiftgram.AccountServer.Models
{
	public class AccountViewModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Bio { get; set; }
		public string PhotoUrl { get; set; }
		public string GenderName { get; set; }
		public string Phone { get; set; }
		public string Username { get; set; }
		public bool IsAuth { get; set; }
	}
}