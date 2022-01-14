using IotReworkDataAccess.UnitOfWork;
using ShopApi.Business.DTO;
using ShopApi.Business.DTO.OperationalResult;
using ShopApi.Db.Models;
using SqliteDatabase.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopApi.Core.UseCase.Bussines
{
    public class CustomerBo : IDisposable
    {
        private IUnitOfWork _uow;
        private IRepository<Customer> _repoCustomer;
        public CustomerBo()
        {
            _uow = new UnitOfWork();
            _repoCustomer = _uow.GetRepository<Customer>();
        }

        /// <summary>
        /// Tüm Müşterilerin döndürüldüğü Liste
        /// </summary>
        /// <returns></returns>
        public List<CustomerDto> GetAllCustomer()
        {
            var allCustomerList = _repoCustomer.GetAll().
                ToList().
                Select(x => new CustomerDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    IsConnectShop = x.IsConnectShop == 1 ? true : false,
                    IsShopEmployee = x.IsShopEmployee == 1 ? true : false,
                    InsertDate = Convert.ToDateTime(x.InsertDate)
                }).ToList();
            // sqlite bool değişkenlerle problem çıkardığı için casting işlemi yaptım

            return allCustomerList;
        }

        /// <summary>
        /// Id customerDto alma
        /// </summary>
        /// <param name="IdCode"></param>
        /// <returns></returns>
        public CustomerDto GetCustomer(int Id)
        {
            var customer = _repoCustomer.Filter(x=> x.Id == Id).FirstOrDefault();
            if (customer == null)
                return null;

            return new CustomerDto()
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                IsConnectShop = customer.IsConnectShop == 1 ? true : false,
                IsShopEmployee = customer.IsShopEmployee == 1 ? true : false,
                InsertDate = Convert.ToDateTime(customer.InsertDate)
            };
        }

        public BusinessResult AddCustomer(CustomerDto customer)
        {
            try
            {
                _repoCustomer.Add(new Customer()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Surname = customer.Surname,
                    IsConnectShop = customer.IsConnectShop ? 1 : 0,
                    IsShopEmployee = customer.IsShopEmployee ? 1 : 0,
                    InsertDate = customer.InsertDate.ToString()
                });

                int result = _uow.SaveChanges();

                if (result > 0)
                    return new BusinessResult();
                else
                    return new BusinessResult() { Result = false, Message = "Kayıt işlemi tamamlanamadı." };
            }
            catch (Exception ex)
            {
                return new BusinessResult() { Result = false, Message = ex.Message };
            }
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
