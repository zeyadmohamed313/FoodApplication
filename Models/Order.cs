using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace FoodApplication.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? RecipeId {  get; set; }
		[Required]
		public string? RecipeName { get; set; }
		[Required]
		public string? UserID {  get; set; }
		[Required]
		public string? Address {  get; set; }
		[Required]
		public int Price {  get; set; }
		[Required]
		public int Quantity {  get; set; }
		public int TotalAmount {  get; set; }
		public DateTime OrderDate { get; set; }
	}
}
