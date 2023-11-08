using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;

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
                    case "PM": // Post moderator can see all post reports
                        foodReportOutputDtos = context.MenuReports
                            .Select(mr => new FoodReportOutputDto
                            {
                                FoodId = mr.FoodId,
                                ReportBy = mr.ReportBy,
                                ReportContent = mr.ReportContent,
                                CreateDate = mr.CreateDate,
                                UpdateDate = mr.UpdateDate,
                                UpdateBy = mr.UpdateBy,
                                Status = PostMenuReportStatusEnum.GetStatusString(mr.Status),
                                Note = mr.Note
                            })
                            .ToList();
                        break;
                    case "CU": // Customer can only see their menu reports
                        foodReportOutputDtos = context.MenuReports
                            .Where(mr => mr.ReportBy.Equals(userId))
                            .Select(mr => new FoodReportOutputDto
                            {
                                FoodId = mr.FoodId,
                                ReportBy = mr.ReportBy,
                                ReportContent = mr.ReportContent,
                                CreateDate = mr.CreateDate,
                                UpdateDate = mr.UpdateDate,
                                UpdateBy = mr.UpdateBy,
                                Status = PostMenuReportStatusEnum.GetStatusString(mr.Status),
                                Note = mr.Note
                            })
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
                }
                else
                {
                    foodReport.Status = 2;
                }
                foodReport.Note = inputDto.Note; // optional

                // Save the changes of the food report entity of the context to the db
                context.SaveChanges();
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.StackTrace + e.InnerException);
            }
        }
    }
}
