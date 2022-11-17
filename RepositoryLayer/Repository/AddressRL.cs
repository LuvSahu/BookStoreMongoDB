using ModelLayer;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Repository
{
    public class AddressRL : IAddressRL
    {
        private readonly IMongoCollection<AddressModel> Address;

        public AddressRL(IDBSetting db)
        {
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            Address = database.GetCollection<AddressModel>("Address");
        }
        public AddressModel AddAddress(AddressModel add,string userid)
        {
            try
            {
                var check = this.Address.Find(x => x.addressID == add.addressID && x.userID == userid).SingleOrDefault();
                if (check == null)
                {
                    this.Address.InsertOne(add);
                    return add;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public AddressModel UpdateAddress(AddressModel edit, string id,string userid)
        {
            try
            {
                var check = this.Address.Find(x => x.addressID == id && x.userID == userid).FirstOrDefault();
                if (check != null)
                {
                    this.Address.UpdateOne(x => x.addressID == id,
                        Builders<AddressModel>.Update.Set(x => x.fullAddress, edit.fullAddress)
                        .Set(x => x.city, edit.city)
                        .Set(x => x.state, edit.state)
                        .Set(x => x.pinCode, edit.pinCode));
                    return check;

                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteAddress(string id)
        {
            try
            {
                this.Address.FindOneAndDelete(x => x.addressID == id);
                return true;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AddressModel> GetallAddress()
        {
            return this.Address.Find(FilterDefinition<AddressModel>.Empty).ToList();
        }

        public AddressModel GetByAddressType(string addtypeId)
        {
            return this.Address.Find(x => x.addTypeID == addtypeId).FirstOrDefault();
        }

    }
}

