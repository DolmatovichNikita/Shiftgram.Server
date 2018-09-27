﻿using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Shiftgram.Core.Views
{
	internal static class View
	{
		private const string CONNECTION = "data source=aarsp4er1pxxe9.coqmx0efxhue.eu-central-1.rds.amazonaws.com,1433;initial catalog=Shiftgram;user id=shiftgram;password=7757739n;multipleactiveresultsets=True;application name=EntityFramework";

		public static async Task CreateFriendView(int accountId)
		{
			using (SqlConnection connection = new SqlConnection(CONNECTION))
			{
				await connection.OpenAsync();
				using (SqlCommand command = new SqlCommand("CreateFriendView", connection))
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.Add(new SqlParameter { ParameterName = "@NameView", Value = "Account" + accountId });
					command.Parameters.Add(new SqlParameter { ParameterName = "@AccountId", Value = accountId });

					await command.ExecuteNonQueryAsync();
				}

				connection.Close();
			}
		}
	}
}