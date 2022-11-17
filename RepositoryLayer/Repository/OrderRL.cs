using ModelLayer;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Repository
{
    public class OrderRL : IOrderRL
    {
        private readonly IMongoCollection<OrderModel> Order;

        public OrderRL(IDBSetting db)
        {
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            Order = database.GetCollection<OrderModel>("Order");
        }
        public OrderModel AddOrder(OrderModel order,string userid)
        {
            try
            {
                var check = this.Order.Find(x => x.orderID == order.orderID && x.userID == userid).SingleOrDefault();
                if (check == null)
                {
                    this.Order.InsertOne(order);
                    return order;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool CancleOrder(string id)
        {
            try
            {
                this.Order.FindOneAndDelete(x => x.orderID == id);
                return true;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<OrderModel> GetOrder()
        {
            //Order.Aggregate({ "$project": { "totalPay": { "$multiply": ["$discountPrice", "$quantity"]  } } } );
            return Order.Find(FilterDefinition<OrderModel>.Empty).ToList();
        }
    }
}
