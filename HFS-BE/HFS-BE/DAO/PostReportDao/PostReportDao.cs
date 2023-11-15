using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.DAO.PostReportDao
{
    public class PostReportDao : BaseDao
    {
        public PostReportDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListPostReportOutputDto GetAllPostReportsByUserId(string userId)
        {
            try
            {
                List<PostReportOutputDto> postReportOutputDtos = new List<PostReportOutputDto>();
                string userRole = userId.Substring(0, 2);
                switch (userRole)
                {
                    case "PM": // Post moderator can see all post reports
                        postReportOutputDtos = context.PostReports
                            .Select(pr => new PostReportOutputDto
                            {
                                PostId = pr.PostId,
                                ReportBy = pr.ReportBy,
                                ReportContent = pr.ReportContent,
                                CreateDate = pr.CreateDate,
                                UpdateDate = pr.UpdateDate,
                                UpdateBy = pr.UpdateBy,
                                Status = PostMenuReportStatusEnum.GetStatusString(pr.Status),
                                Note = pr.Note
                            })
                            .ToList();
                        break;
                    case "CU": // Customer can only see their post reports
                        postReportOutputDtos = context.PostReports
                            .Include(pr => pr.Post)
                            .Include(pr => pr.Post.Seller)
                            .Where(pr => pr.ReportBy.Equals(userId))
                            .Select(pr => new PostReportOutputDto
                            {
                                PostId = pr.PostId,
                                SellerName = pr.Post.Seller.LastName + " " + pr.Post.Seller.FirstName,
                                PostContent = pr.Post.PostContent,
                                ReportBy = pr.ReportBy,
                                ReportContent = pr.ReportContent,
                                CreateDate = pr.CreateDate,
                                UpdateDate = pr.UpdateDate,
                                UpdateBy = pr.UpdateBy,
                                Status = PostMenuReportStatusEnum.GetStatusString(pr.Status),
                                Note = pr.Note
                            })
                            .ToList();
                        break;
                }
                var outputDto = Output<ListPostReportOutputDto>(Constants.ResultCdSuccess);
                outputDto.PostReports = postReportOutputDtos;
                return outputDto;
            }
            catch (Exception e)
            {
                return Output<ListPostReportOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.StackTrace + e.InnerException);
            }
        }

        public BaseOutputDto CreateNewPostReport(CreateNewPostReportInputDto inputDto, string reportBy)
        {
            try
            {
                // Create a new Post report instance with the info of PostReportInputDto
                PostReport postReport = new PostReport()
                {
                    PostId = inputDto.PostId,
                    ReportBy = reportBy,
                    ReportContent = inputDto.ReportContent,
                    CreateDate = DateTime.Now,
                    Status = 0
                };
                // Add to the context
                context.PostReports.Add(postReport);
                // Save changes
                context.SaveChanges();
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.StackTrace + e.InnerException);
            }
        }

        public BaseOutputDto ApproveNotApprovePostReport(ApproveNotApprovePostReportInputDto inputDto, string updateBy)
        {
            try
            {
                // Find the post report by postId and reportBy in the context
                PostReport? postReport = context.PostReports.Find(new object[] { inputDto.PostId, inputDto.ReportBy });
                // Check whether post report exist or not
                if (postReport == null)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, $"The post report with postId = {inputDto.PostId} and reportBy = {inputDto.ReportBy} is not exist. Please try again.");
                }
                // Update status to approve or not approve, and add some note (optional)
                postReport.UpdateBy = updateBy;
                postReport.UpdateDate = DateTime.Now;
                if (inputDto.IsApproved)
                {
                    postReport.Status = 1;
                }
                else
                {
                    postReport.Status = 2;
                }
                postReport.Note = inputDto.Note; // optional
                // Save the changes of the post report entity in the context to the db
                context.SaveChanges();
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.StackTrace + e.InnerException);
            }
        }

        //public BaseOutputDto CancelPostReport(CancelPostReportInputDto inputDto, string customerId)
        //{
        //    try
        //    {
        //        // Lay ra ban ghi report ma customer muon cancel o trong context
        //        PostReport? postReport = context.PostReports.Find(new object[] { inputDto.PostId, customerId });
        //        if (postReport == null) return Output<BaseOutputDto>(Constants.ResultCdSuccess, $"Cannot find the post report with id: {inputDto.PostId} of user with id: {customerId}");
        //        postReport.Status = 3; // Set status to cancel, and the customer cannot undo this action
        //        postReport.Note = $"Cancel reason: {inputDto.Note}"; // Reason is required
        //        context.SaveChanges();

        //        return Output<BaseOutputDto>(Constants.ResultCdSuccess);
        //    }
        //    catch (Exception e)
        //    {
        //        return Output<BaseOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.StackTrace + e.InnerException);
        //        throw;
        //    }
        //}
    }
}
