using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Business
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }
        public AddressModel AddAddress(AddressModel add,string userid)
        {
            try
            {
                return this.addressRL.AddAddress(add,userid);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public AddressModel UpdateAddress(AddressModel edit, string id,string userid)
        {
            try
            {
                return this.addressRL.UpdateAddress(edit,id,userid);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteAddress(string id)
        {
            try
            {
                return this.addressRL.DeleteAddress(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<AddressModel> GetallAddress()
        {
            try
            {
                return this.addressRL.GetallAddress();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public AddressModel GetByAddressType(string addtypeId)
        {
            try
            {
                return this.addressRL.GetByAddressType(addtypeId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
