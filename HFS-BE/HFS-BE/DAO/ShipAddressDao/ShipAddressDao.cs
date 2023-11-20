using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using Mailjet.Client.Resources;

namespace HFS_BE.DAO.ShipAddressDao
{
    public class ShipAddressDao : BaseDao
    {
        public ShipAddressDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListShipAddressOutputDto GetAllShipAddressByUserId(string userId)
        {
            try
            {
                var shipAddresses = context.ShipAddresses
                    .Where(sa => sa.CustomerId.Equals(userId))
                    .OrderByDescending(sa => sa.IsDefaultAddress == true)
                    .ToList();
                var output = Output<ListShipAddressOutputDto>(Constants.ResultCdSuccess);
                output.ShipAddresses = mapper.Map<List<ShipAddress>, List<ShipAddressOutputDto>>(shipAddresses);
                return output;
            }
            catch (Exception e)
            {
                return Output<ListShipAddressOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.InnerException + e.StackTrace);
            }
        }

        public BaseOutputDto CreateNewShipAddress(CreateNewShipAddressInputDto inputDto, string userId)
        {
            try
            {
                // Get the customer's list of ship addresses
                var shipAddresses = context.ShipAddresses
                    .Where(sa => sa.CustomerId.Equals(userId))
                    .ToList();

                // Check the customer's ship address list to see if it is empty or not.
                // If yes, set the first ship address to default one when created
                if (shipAddresses.Count < 1)
                {
                    context.ShipAddresses.Add(new ShipAddress { AddressInfo = inputDto.AddressInfo, CustomerId = userId, IsDefaultAddress = true });
                }
                else // If no, not setting it default upon creation
                {
                    context.ShipAddresses.Add(new ShipAddress { AddressInfo = inputDto.AddressInfo, CustomerId = userId, IsDefaultAddress = false });
                }
                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.InnerException + e.StackTrace);
            }
        }

        public BaseOutputDto UpdateShipAddress(UpdateShipAddressInputDto inputDto)
        {
            try
            {
                // Find customer's ship address by addressId
                var shipAddress = context.ShipAddresses.SingleOrDefault(sa => sa.AddressId == inputDto.AddressId);

                // Check whether the ship address is null or not
                if (shipAddress == null)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "The ship address cannot be found.");
                }

                // Update its info if it exist
                shipAddress.AddressInfo = inputDto.AddressInfo;
                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.InnerException + e.StackTrace);
            }
        }

        public BaseOutputDto DeleteShipAddress(DeleteShipAddressInputDto inputDto)
        {
            try
            {
                // Find customer's ship address by addressId
                var shipAddress = context.ShipAddresses.SingleOrDefault(sa => sa.AddressId == inputDto.AddressId);

                // Check whether the ship address is null or not
                if (shipAddress == null)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "The ship address cannot be found.");
                }

                // Remove it if found
                context.ShipAddresses.Remove(shipAddress);
                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.InnerException + e.StackTrace);
            }
        }

        public BaseOutputDto SetDefaultShipAddress(SetDefaultShipAddressInputDto inputDto, string userId)
        {
            try
            {
                // Find customer's ship address by addressId
                var shipAddress = context.ShipAddresses.SingleOrDefault(sa => sa.AddressId == inputDto.AddressId);

                // Check whether the ship address is null or not
                if (shipAddress == null)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "The ship address cannot be found.");
                }

                // Unset default to the old default ship address
                var oldDefaultShipAddress = context.ShipAddresses
                    .SingleOrDefault(sa => sa.IsDefaultAddress == true && sa.CustomerId.Equals(userId));
                oldDefaultShipAddress.IsDefaultAddress = false;

                // Set default to the ship address found at step 1
                shipAddress.IsDefaultAddress = true;
                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.InnerException + e.StackTrace);
            }
        }
    }
}
