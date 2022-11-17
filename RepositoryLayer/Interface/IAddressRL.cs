using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public AddressModel AddAddress(AddressModel add,string userid);

        public AddressModel UpdateAddress(AddressModel edit, string id,string userid);

        public bool DeleteAddress(string id);

        public IEnumerable<AddressModel> GetallAddress();

        public AddressModel GetByAddressType(string addtypeId);





    }
}
