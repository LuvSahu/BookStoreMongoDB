using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        public OrderModel AddOrder(OrderModel order,string userid);

        public bool CancleOrder(string id);

        public IEnumerable<OrderModel> GetOrder();



    }
}
