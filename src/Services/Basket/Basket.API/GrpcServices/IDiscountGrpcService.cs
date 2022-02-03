using Discount.GRPC.Protos;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public interface IDiscountGrpcService
    {
        public Task<CouponModel> GetDiscount(string productName);
    }
}