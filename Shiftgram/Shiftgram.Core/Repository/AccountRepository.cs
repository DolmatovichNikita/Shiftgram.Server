﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Shiftgram.Core.Enums;
using Shiftgram.Core.Exceptions;
using Shiftgram.Core.Models;
using Shiftgram.Core.Strategy;
using Shiftgram.Core.Views;
using System.Linq;

namespace Shiftgram.Core.Repository
{
	public class AccountRepository : IAccountRepository
	{
		private ShiftgramContext _context;
		private ViewCreator creator;
		private View _view;

		public AccountRepository()
		{
			this._context = new ShiftgramContext();
			this.creator = new FriendViewCreator();
			this._view = creator.CreateView();
		}

		public ShiftgramContext Context => this._context;

		public IUpdatableAccount Updatable { get; set; }

		public async Task<int> Add(Account item)
		{
			this._context.Accounts.Add(item);
			int rows = await this._context.SaveChangesAsync();

			if(rows > 0)
			{
				await this._view.CreateView(item.Id);
				return item.Id;
			}

			throw new AccountException();
		}

		public async Task<DbAnswerCode> Delete(int id)
		{
			var dbEntry = await this._context.Accounts.FirstOrDefaultAsync<Account>(x => x.Id == id);

			if(dbEntry != null)
			{
				string viewName = $"Account{dbEntry.Id}";
				await this._view.DropView(viewName);
				this._context.Friends.ToList().RemoveAll(x => x.AccountAId == dbEntry.Id);
				this._context.Accounts.Remove(dbEntry);
				int rows = await this._context.SaveChangesAsync();

				if(rows > 0)
				{
					return DbAnswerCode.Ok;
				}
			}

			return DbAnswerCode.Bad;
		}

		public async Task<IEnumerable<Account>> GetAll()
		{
			return await this._context.Accounts.ToListAsync<Account>();
		}

		public async Task<Account> GetById(int id)
		{
			var dbEntry = await this._context.Accounts.FirstOrDefaultAsync<Account>(x => x.Id == id);

			if(dbEntry != null)
			{
				return dbEntry;
			}

			throw new AccountException();
		}

		public async Task<Account> GetByPhone(string phone)
		{
			var dbEntry = await this._context.Accounts.FirstOrDefaultAsync<Account>(x => x.Phone == phone);

			if(dbEntry != null)
			{
				return dbEntry;
			}

			throw new AccountException();
		}

		public async Task<DbAnswerCode> Update(AccountUpdateViewModel model)
		{
			return await this.Updatable.Update(model);
		}
	}
}
