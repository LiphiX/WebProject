using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database;
using PostOffice.Models.Entities;

namespace PostOffice.Models;
public class UserService(PostOfficeContext postOfficeContext)
{
	private PostOfficeContext _context = postOfficeContext;

	public List<UserAccount> GetUserAccounts() => _context.UserAccounts.ToList();

	public async Task<List<UserAccount>> GetUserAccountsAsync() => await _context.UserAccounts.ToListAsync();

	public void Add(UserAccount account) =>
		_context.UserAccounts.Add(account);

	public async Task AddAsync(UserAccount account) =>
		await _context.UserAccounts.AddAsync(account);

	public void AddRange(List<UserAccount> accounts) =>
		_context.UserAccounts.AddRange(accounts);

	public async Task AddRangeAsync(List<UserAccount> accounts) =>
		await _context.UserAccounts.AddRangeAsync(accounts);

	public UserAccount? FindUserAccount(string login, string password) =>
		_context.UserAccounts.FirstOrDefault(item => item.Login == login && item.Password == item.Password);

	public async Task<UserAccount?> FindUserAccountAsync(string login, string password) =>
		await _context.UserAccounts.FirstOrDefaultAsync(item => item.Login == login && item.Password == password);
}
