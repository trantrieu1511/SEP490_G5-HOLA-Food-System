using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.SellerDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageFood
{
    public class EnableDisableFoodBusinessLogic : BaseBusinessLogic
    {
        public EnableDisableFoodBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto EnableDisableFood(FoodEnableDisableInputDto input)
        {
            try
            {
                

                var dao = this.CreateDao<FoodDao>();

                var food = dao.GetFoodById(input.FoodId);
                var errors = new List<string>();
                if (food == null)
                {
                    
                    errors.Add($"FoodId: {input.FoodId} not exist!");
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Enable/Disable Failed", errors);
                    //return Output<BaseOutputDto>(Constants.ResultCdFail, $"FoodId: {input.FoodId} not exist!");
                }
                // check status Ban 
                if (food.Status == 3)
                {
                    errors.Add($"FoodId: {input.FoodId} has been banned and cannot be changed!");
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Enable/Disable Failed", errors);
                    /*return Output<BaseOutputDto>(Constants.ResultCdFail,
                        $"FoodId: {input.FoodId} has been banned and cannot be changed!");*/
                }
                // check status Not Approved
                if (!input.isMenuMod) // TH la seller thi se phai validate nhu ben duoi
                {
                    var sellerDao = CreateDao<SellerDao>();

                    if (sellerDao.GetSellerByEmail(input.UserDto.Email) is null)
                        return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Your acccount is not exist");

                    if (sellerDao.GetSellerByEmail(input.UserDto.Email).IsVerified == false)
                        return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Your acccount is not verified");

                    if (sellerDao.GetSellerByEmail(input.UserDto.Email).IsBanned == true)
                        return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Your acccount is banned");

                    if (food.Status == 0)
                    {
                        errors.Add($"FoodId: {input.FoodId} is pending acceptance and cannot be changed!");
                        return Output<BaseOutputDto>(Constants.ResultCdFail, "Enable/Disable Failed", errors);
                        /*return Output<BaseOutputDto>(Constants.ResultCdFail,
                            $"FoodId: {input.FoodId} is pending acceptance and cannot be changed!");*/
                    }
                }

                var output = dao.EnableDisableFood(input);

                return output;
            }
            catch (Exception)
            {

                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail);
            }
        }
    }
}
