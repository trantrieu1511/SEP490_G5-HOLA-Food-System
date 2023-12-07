using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.PostReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using Mailjet.Client.Resources;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.DAO.MenuReportDao
{
    public class FoodReportDao : BaseDao
    {
        public FoodReportDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListFoodReportOutputDto GetAllFoodReportsByUserId(string userId)
        {
            try
            {
                List<FoodReportOutputDto> foodReportOutputDtos = new List<FoodReportOutputDto>();
                string userRole = userId.Substring(0, 2);
                switch (userRole)
                {
                    case "MM": // Menu moderator can see all food reports
                        foodReportOutputDtos = context.MenuReports
                            .Include(mr => mr.Food)
                            .Select(mr => new FoodReportOutputDto
                            {
                                FoodId = mr.FoodId,
                                FoodName = mr.Food.Name,
                                SellerId = mr.Food.SellerId,
                                ReportBy = mr.ReportBy,
                                ReportContent = mr.ReportContent,
                                CreateDate = mr.CreateDate,
                                UpdateDate = mr.UpdateDate,
                                UpdateBy = mr.UpdateBy,
                                Status = PostMenuReportStatusEnum.GetStatusString(mr.Status),
                                Note = mr.Note
                            })
                            .OrderByDescending(pr => pr.CreateDate)
                            .ToList();
                        break;
                    case "CU": // Customer can only see their food reports
                        foodReportOutputDtos = context.MenuReports
                            .Include(mr => mr.Food)
                            .Include(mr => mr.Food.Seller)
                            .Where(mr => mr.ReportBy.Equals(userId))
                            .Select(mr => new FoodReportOutputDto
                            {
                                FoodId = mr.FoodId,
                                FoodName = mr.Food.Name,
                                ShopName = mr.Food.Seller.ShopName,
                                ReportBy = mr.ReportBy,
                                ReportContent = mr.ReportContent,
                                CreateDate = mr.CreateDate,
                                UpdateDate = mr.UpdateDate,
                                UpdateBy = mr.UpdateBy,
                                Status = PostMenuReportStatusEnum.GetStatusString(mr.Status),
                                Note = mr.Note
                            })
                            .OrderByDescending(pr => pr.CreateDate)
                            .ToList();
                        break;
                }
                var outputDto = Output<ListFoodReportOutputDto>(Constants.ResultCdSuccess);
                outputDto.FoodReports = foodReportOutputDtos;
                return outputDto;
            }
            catch (Exception e)
            {
                return Output<ListFoodReportOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.StackTrace + e.InnerException);
            }
        }

        public BaseOutputDto CreateNewFoodReport(CreateNewFoodReportInputDto inputDto, string reportBy)
        {
            try
            {
                // Create a new food report instance with the info of FoodReportInputDto
                MenuReport foodReport = new MenuReport()
                {
                    FoodId = inputDto.FoodId,
                    ReportBy = reportBy,
                    ReportContent = inputDto.ReportContent,
                    CreateDate = DateTime.Now,
                    Status = 0
                };
                // Add to the context
                context.MenuReports.Add(foodReport);
                // Save changes
                context.SaveChanges();
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.StackTrace + e.InnerException);
            }
        }

        public BaseOutputDto ApproveNotApproveFoodReport(ApproveNotApproveFoodReportInputDto inputDto, string updateBy)
        {
            try
            {
                // Check report approval limit (25 per day), neu lon hon 0 thi moi thuc hien viec approve/not approve. Khong thi khong approve/not approve nua
                // Phong truong hop co nguoi lam nguoi khong lam (ReportApprovalLimit duoc reset vao 23h59 moi ngay)
                if (context.MenuModerators.SingleOrDefault(mm => mm.ModId.Equals(updateBy)).ReportApprovalLimit > 0)
                {
                    // Find the food report by foodId and reportBy in the context
                    MenuReport? foodReport = context.MenuReports.Find(new object[] { inputDto.FoodId, inputDto.ReportBy });
                    // Check whether the food report exist or not
                    if (foodReport == null)
                    {
                        return Output<BaseOutputDto>(Constants.ResultCdFail, $"The food report with foodId = {inputDto.FoodId} and reportBy = {inputDto.ReportBy} is not exist. Please try again.");
                    }

                    // Update status to approve or not approve, and add some note (optional)
                    foodReport.UpdateBy = updateBy;
                    foodReport.UpdateDate = DateTime.Now;
                    if (inputDto.IsApproved)
                    {
                        foodReport.Status = 1;
                        context.Foods.SingleOrDefault(f => f.FoodId == foodReport.FoodId).ReportedTimes++;
                    }
                    else
                    {
                        foodReport.Status = 2;
                        int? reportedTimes = context.Foods.SingleOrDefault(f => f.FoodId == foodReport.FoodId).ReportedTimes;
                        if (reportedTimes > 0)
                        {
                            context.Foods.SingleOrDefault(f => f.FoodId == foodReport.FoodId).ReportedTimes--;
                        }
                    }
                    foodReport.Note = inputDto.Note; // optional

                    // Reduce approval limit
                    context.MenuModerators.SingleOrDefault(mm => mm.ModId.Equals(updateBy)).ReportApprovalLimit -= 1;

                    // Save the changes of the food report entity of the context to the db
                    context.SaveChanges();
                    return Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }
                else
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Your limit of approve/not approve 25 food per day have reached.");
                }
            }
            catch (Exception e)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.StackTrace + e.InnerException);
            }
        }

        public DashboardMenumodFoodReportDataDaoOutputDto GetAllFoodReportsByModId(DashboardMenuModInputDto inputDto)
        {
            try
            {
                var foodReports = context.MenuReports
                    .Where(pr => pr.UpdateBy.Equals(inputDto.ModId))
                    .ToList();
                var output = Output<DashboardMenumodFoodReportDataDaoOutputDto>(Constants.ResultCdSuccess);
                output.FoodReports = foodReports;
                return output;
            }
            catch (Exception e)
            {
                return this.Output<DashboardMenumodFoodReportDataDaoOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.InnerException + e.StackTrace);
            }
        }

        public DashboardMenumodFoodReportDataDaoOutputDto GetAllFoodReports()
        {
            try
            {
                var foodReports = context.MenuReports
                    .ToList();
                var output = Output<DashboardMenumodFoodReportDataDaoOutputDto>(Constants.ResultCdSuccess);
                output.FoodReports = foodReports;
                return output;
            }
            catch (Exception e)
            {
                return this.Output<DashboardMenumodFoodReportDataDaoOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.InnerException + e.StackTrace);
            }
        }
    }
}
