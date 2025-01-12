using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace order_service.Order;

[Table("Order")]
public class Order(Guid id, string email, DateTime dateTime)
{
    [Key] public Guid Id { get; set; } = id;

    [Required] [EmailAddress] public string Email { get; set; } = email;

    [Required] public DateTime DateTime { get; set; } = dateTime;

}

[Table("ProductOrder")]
public class ProductOrder(Guid id, Guid orderId, Guid productId)
{
    [Key] public Guid Id { get; set; } = id;
    [Required] [ForeignKey("orderId")] public Guid OrderId { get; set; } = orderId;
    [Required] [ForeignKey("productId")] public Guid ProductId { get; set; } = productId;
}

public record OrderInput(string Email, DateTime DateTime, List<Guid> ProductIds);

internal static class OrderExtension
{
    internal static Order ToOrder(this OrderInput orderInput) =>
        new(Guid.NewGuid(), orderInput.Email, orderInput.DateTime);
}