using Shiftgram.Core.ViewModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Shiftgram.Core.Views
{
	internal static class View
	{
		private const string CONNECTION = "data source=aarsp4er1pxxe9.coqmx0efxhue.eu-central-1.rds.amazonaws.com,1433;initial catalog=Shiftgram;user id=shiftgram;password=7757739n;multipleactiveresultsets=True;application name=EntityFramework";

		public static async Task CreateFriendView(int accountAId)
		{
			using (SqlConnection connection = new SqlConnection(CONNECTION))
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

		public static async Task<IEnumerable<AccountFriendViewModel>> GetFriends(int accountAId)
		{
			List<AccountFriendViewModel> models = new List<AccountFriendViewModel>();

			using (SqlConnection connection = new SqlConnection(CONNECTION))
			{
				await connection.OpenAsync();
				string sqlCommand = $"SELECT * FROM Account{accountAId}";
				using (SqlCommand command = new SqlCommand(sqlCommand, connection))
				{
					using (var reader = await command.ExecuteReaderAsync())
					{
						while(await reader.ReadAsync())
						{
							models.Add(new AccountFriendViewModel
							{
								Username = await reader.IsDBNullAsync(0) ? string.Empty : reader.GetString(0),
								PhotoUrl = await reader.IsDBNullAsync(1) ? string.Empty : reader.GetString(1),
								Id = reader.GetInt32(2)
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
