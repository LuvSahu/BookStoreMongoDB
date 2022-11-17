using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Business
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }
        public OrderModel AddOrder(OrderModel order,string userid)
        {
            try
            {
                return this.orderRL.AddOrder(order,userid);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool CancleOrder(string id)
        {
            try
            {
                return this.orderRL.CancleOrder(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<OrderModel> GetOrder()
        {
            try
            {
                return this.orderRL.GetOrder();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
