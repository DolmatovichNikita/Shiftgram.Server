using Shiftgram.Core.ViewModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Shiftgram.Core.Views
{
	internal class FriendView: View
	{
		public async Task CreateFriendView(int accountAId)
		{
			using (SqlConnection connection = new SqlConnection(this._connection))
			{
				await connection.OpenAsync();
				using (SqlCommand command = new SqlCommand("CreateFriendView", connection))
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.Add(new SqlParameter { ParameterName = "@NameView", Value = "Account" + accountAId });
					command.Parameters.Add(new SqlParameter { ParameterName = "@AccountId", Value = accountAId });

					await command.ExecuteNonQueryAsync();
				}

				connection.Close();
			}
		}

		public async Task<IEnumerable<AccountFriendViewModel>> GetFriends(int accountAId)
		{
			List<AccountFriendViewModel> models = new List<AccountFriendViewModel>();

			using (SqlConnection connection = new SqlConnection(this._connection))
			{
				await connection.OpenAsync();
				string sqlCommand = $"SELECT * FROM Account{accountAId}";
				using (SqlCommand command = new SqlCommand(sqlCommand, connection))
				{
					using (var reader = await command.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							models.Add(new AccountFriendViewModel
							{
								Username = await reader.IsDBNullAsync(0) ? string.Empty : reader.GetString(0),
								PhotoUrl = await reader.IsDBNullAsync(1) ? string.Empty : reader.GetString(1),
								Id = reader.GetInt32(2),
								Phone = reader.GetString(3)
							});
						}
					}
				}

				connection.Close();
			}

			return models;
		}
	}
}
