using IotReworkDataAccess.UnitOfWork;
using ShopApi.Business.DTO;
using ShopApi.Core.UseCase.Bussines;
using ShopApi.Db.Models;
using SqliteDatabase.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApi.Business.UseCase
{
    public class DiscountBo : IDisposable
    {
        private IUnitOfWork _uow;
        private IRepository<Discount> _repoDiscount;
        public DiscountBo()
        {
            _uow = new UnitOfWork();
            _repoDiscount = _uow.GetRepository<Discount>();
        }

        public List<DiscountDto> GetAllDiscount()
        {
            return _repoDiscount.GetAll().ToList().Select(x => new DiscountDto()
            {
                DiscountType = x.DiscountType,
                DiscountRate = x.DiscountRate
            }).ToList();
        }

        public DiscountDto GetDiscountModel(int customerId)
        {
            DiscountDto discountDto = null;
            using (CustomerBo bo = new CustomerBo())
            {
                var customer = bo.GetCustomer(customerId);

                if (customer == null)
                    return null;

                if (customer.IsShopEmployee)
                {
                    discountDto = DiscountType("Employee");
                }
                else if (customer.IsConnectShop)
                {
                    discountDto = DiscountType("Connected");
                }
                else if (customer.InsertDate < DateTime.Now.AddYears(-2))
                {
                    discountDto = DiscountType("TwoYear");
                }
                else
                {
                    discountDto = DiscountType("NoDiscount");
                }

                return discountDto;
            }
        }

        private DiscountDto DiscountType(string DiscountType)
        {
            return _repoDiscount.Filter(x => x.DiscountType == DiscountType).
                        Select(x => new DiscountDto()
                        {
                            DiscountType = x.DiscountType,
                            DiscountRate = x.DiscountRate
                        }).FirstOrDefault();
        }



        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
