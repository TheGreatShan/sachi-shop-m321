using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace order_service.Order;

[Table("Order")]
public class Order(Guid id, string email, DateTime dateTime, List<Guid> productIds)
{
    [Key] public Guid Id { get; set; } = id;

    [Required] [EmailAddress] public string Email { get; set; } = email;

    [Required] public DateTime DateTime { get; set; } = dateTime;

    [Required] public List<Guid> ProductIds { get; set; } = productIds;
}

public record OrderInput(string Email, DateTime DateTime, List<Guid> ProductIds);

internal static class OrderExtension
{
    internal static Order ToOrder(this OrderInput orderInput) =>
        new(Guid.NewGuid(), orderInput.Email, orderInput.DateTime, orderInput.ProductIds);
}