using Discount.Grpc;
using Grpc.Core;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class DiscountService(DiscountDbContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
  public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
  {
    var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
    if (coupon is null)
      coupon = new Coupon
      {
        ProductName = "No Discount",
        Description = "No Discount des",
        Amount = 0
      };

    logger.LogInformation("Discount is retrived for productName : {productName}, Amount :{amount} ", coupon.ProductName, coupon.Amount);

    var couponModel = coupon.Adapt<CouponModel>();
    return couponModel;

  }

  public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
  {
    var coupon = request.Coupon.Adapt<Coupon>();
    if (coupon is null)
    {
      throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
    }

    dbContext.Coupons.Add(coupon);
    await dbContext.SaveChangesAsync();
    logger.LogInformation("Discount is successfully created for productName : {productName}", coupon.ProductName);
    return coupon.Adapt<CouponModel>(); 

  }

  public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
  {
    var coupon = request.Coupon.Adapt<Coupon>();
    if (coupon is null)
    {
      throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
    }

    dbContext.Coupons.Update(coupon);
    await dbContext.SaveChangesAsync();
    logger.LogInformation("Discount is successfully updated for productName : {productName}", coupon.ProductName);
    return coupon.Adapt<CouponModel>();
  }

  public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
  {
    var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
    if (coupon is null)
      throw new RpcException(new Status(StatusCode.NotFound, $"Discount with {request.ProductName} not found"));

    dbContext.Coupons.Remove(coupon);
    await dbContext.SaveChangesAsync();
    logger.LogInformation("Discount is successfully deleted for productName : {productName}", coupon.ProductName);
    return new DeleteDiscountResponse { Success = true };
  }

}