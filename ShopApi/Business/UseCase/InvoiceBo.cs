using IotReworkDataAccess.UnitOfWork;
using ShopApi.Business.DTO;
using ShopApi.Business.DTO.OperationalResult;
using ShopApi.Core.UseCase.Bussines;
using ShopApi.Db.Models;
using SqliteDatabase.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApi.Business.UseCase
{
    public class InvoiceBo : IDisposable
    {
        private IUnitOfWork _uow;
        private IRepository<Invoice> _repoInvoice;
        private double _pricaeMultiplier = 5;

        public InvoiceBo()
        {
            _uow = new UnitOfWork();
            _repoInvoice = _uow.GetRepository<Invoice>();
        }

        /// <summary>
        /// Tüme invoice kayıtlarının alındığı func
        /// </summary>
        /// <returns></returns>
        public List<InvoiceDto> GetAllInvoice()
        {
            return _repoInvoice.GetAll().ToList().Select(x => new InvoiceDto()
            {
                Price = x.Price,
                DiscountPrice = x.DiscountPrice,
                DiscountRate = x.DiscountRate,
                NetPrice = x.NetPrice,
                IsShoppingAction = x.IsShoppingAction == 1 ? true : false,
                CustomerId = x.CustomerId,
                InsertDate = Convert.ToDateTime(x.InsertDate)
            }).ToList();
        }


        public InvoiceDto CalculationInvoice(InvoiceCalculationModel model)
        {
            double isShoppingDiscount = 0, otherDiscount = 0;

            InvoiceDto invoice = new InvoiceDto();

            if (!model.IsShopping)
            {
                using (DiscountBo bo = new DiscountBo())
                {
                    invoice.DiscountRate = bo.GetDiscountModel(model.CustomerId).DiscountRate;
                }

                if (invoice.DiscountRate != 0)
                    isShoppingDiscount = Math.Round(model.Price * (invoice.DiscountRate / 100), 2);
            }

            otherDiscount = Math.Floor((model.Price / 100)) * _pricaeMultiplier;

            invoice.Price = model.Price;
            invoice.NetPrice = model.Price - (isShoppingDiscount + otherDiscount);
            invoice.IsShoppingAction = model.IsShopping;
            invoice.CustomerId = model.CustomerId;
            invoice.DiscountPrice = isShoppingDiscount;
            invoice.InsertDate = DateTime.Now;

            //Add Invoice
            AddInvoice(invoice);

            return invoice;
        }

        public BusinessResult AddInvoice(InvoiceDto invoice)
        {
            _repoInvoice.Add(new Invoice()
            {
                CustomerId = invoice.CustomerId,
                DiscountPrice = invoice.DiscountPrice,
                DiscountRate = invoice.DiscountRate,
                InsertDate = invoice.InsertDate.ToString(),
                IsShoppingAction = invoice.IsShoppingAction ? 1 : 0,
                NetPrice = invoice.NetPrice,
                Price = invoice.Price,
            });

            int result = _uow.SaveChanges();


            if (result > 0)
                return new BusinessResult();
            else
                return new BusinessResult() { Result = false, Message = "Fatura Kayıt edilemedi !" };
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
