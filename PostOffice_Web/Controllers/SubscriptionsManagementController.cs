using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database;
using PostOffice.Models.Entities;

namespace PostOffice.Controllers;
public class SubscriptionsManagementController(PostOfficeContext postOfficeContext) : Controller
{
	[Authorize(Roles = "PostalOperator,Director")]
	public IActionResult List() => View("List", postOfficeContext.Subscriptions.ToList());

	[Authorize(Roles = "PostalOperator,Director")]
	public async Task<IActionResult> Approve(int id)
	{
		var subscription = await postOfficeContext.Subscriptions.FirstOrDefaultAsync(item => item.Id == id);
		if (subscription == null)
			return NotFound();

		subscription.SubscriptionCompleted = true;
		await postOfficeContext.SaveChangesAsync();

		return View("List", postOfficeContext.Subscriptions.ToList());
	}

	[Authorize(Roles = "PostalOperator,Director")]
	public async Task<IActionResult> Decline(int id)
	{
		var subscription = await postOfficeContext.Subscriptions.FirstOrDefaultAsync(item => item.Id == id);
		if (subscription == null)
			return NotFound();

		postOfficeContext.Subscriptions.Remove(subscription);
		await postOfficeContext.SaveChangesAsync();

		return View("List", postOfficeContext.Subscriptions.ToList());
	}
}
