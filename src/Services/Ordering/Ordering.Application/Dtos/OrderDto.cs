public record OrderDto
(
  Guid Id,
  Guid CustomerId,
  string OrderName,
  AddressDto ShippingAddress,
  AddressDto BillingAddress,
  PaymentDto PaymentDto,
  OrderStatus OrderStatus,
  List<OrderItemDto> OrderItems

);