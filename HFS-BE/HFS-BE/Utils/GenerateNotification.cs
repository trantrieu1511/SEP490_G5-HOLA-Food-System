using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using HFS_BE.Utils.Enum;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Utils
{
    public class GenerateNotification
    {
        private static class SingletonHelper
        {
            internal static readonly GenerateNotification INSTANCE = new GenerateNotification();
        }

        private GenerateNotification() { }

        public static GenerateNotification GetSingleton()
        {
            return SingletonHelper.INSTANCE;
        }

        public List<NotificationAddNewInputDto> GenNotificationInternalShipper(string receiver, int orderId, string shopName)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");
            //english
            notifyLst[0].Title = "New Order";
            notifyLst[0].Content = $"Order ID: {orderId} transferred from shop: {shopName} for delivery";
            //vietnamese
            notifyLst[1].Title = "Đơn hàng mới";
            notifyLst[1].Content = $"Đơn hàng ID: {orderId} được chuyển từ cửa hàng: {shopName} để giao hàng";

            return notifyLst;
        }
        public List<NotificationAddNewInputDto> GenNotificationNewOrderSeller(string receiver, int orderId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "New Order";
            notifyLst[0].Content = $"Order ID: {orderId}, there is a new order request";

            notifyLst[1].Title = "Đơn hàng mới";
            notifyLst[1].Content = $"Đơn hàng ID: {orderId}, có một yêu cầu đặt đơn hàng mới";

            return notifyLst;
        }
        public List<NotificationAddNewInputDto> GenNotificationAddNewPost(string receiver, int postId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "New Post";
            notifyLst[0].Content = $"Post ID: {postId} has been created";

            notifyLst[1].Title = "Bài post mới";
            notifyLst[1].Content = $"Bài post ID: {postId} được tạo mới";

            return notifyLst;
        }
        public List<NotificationAddNewInputDto> GenNotificationAddNewFood(string receiver, int menuId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");
            notifyLst[0].Title = "New Menu";
            notifyLst[0].Content = $"Menu ID: {menuId} is waiting for approval";

            notifyLst[1].Title = "Menu mới";
            notifyLst[1].Content = $"Menu mới ID: {menuId} đang chờ để được chấp nhận";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationAcceptOrder(string receiver, int orderId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "Update order";
            notifyLst[0].Content = $"Order ID: {orderId} has been accepted";

            notifyLst[1].Title = "Cập nhật đơn hàng";
            notifyLst[1].Content = $"Đơn hàng ID: {orderId} đã được chấp nhận";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationCancelOrder(string receiver, int orderId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System"); 

            notifyLst[0].Title = "Update order";
            notifyLst[0].Content = $"Order ID: {orderId} has been cancelled";

            notifyLst[1].Title = "Cập nhật đơn hàng";
            notifyLst[1].Content = $"Đơn hàng ID: {orderId} đã bị từ chối";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationOrderShipping(string receiver, int orderId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "Update order";
            notifyLst[0].Content = $"Order ID: {orderId} is on the way";

            notifyLst[1].Title = "Cập nhật đơn hàng";
            notifyLst[1].Content = $"Đơn hàng ID: {orderId} đang giao";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationOrderShippedSuccess(string receiver, int orderId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");
            notifyLst[0].Title = "Update order";
            notifyLst[0].Content = $"Order ID: {orderId} has been delivered successfully";

            notifyLst[1].Title = "Cập nhật đơn hàng";
            notifyLst[1].Content = $"Đơn hàng ID: {orderId} đã giao thành công";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationOrderShippedFail(string receiver, int orderId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "Update order";
            notifyLst[0].Content = $"Order ID: {orderId} was delivered unsuccessfully";

            notifyLst[1].Title = "Cập nhật đơn hàng";
            notifyLst[1].Content = $"Đơn hàng ID: {orderId} đã giao không thành công";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationOrderFeedBack(string receiver, int foodId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "New Feedback";
            notifyLst[0].Content = $"Food ID: {foodId} has additional feedback from buyers";

            notifyLst[1].Title = "Phản hồi mới";
            notifyLst[1].Content = $"Món ID: {foodId} có thêm phản hồi từ người mua";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationFoodReport(string receiver, int foodId, string userId, string? note)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "New Report";
            notifyLst[0].Content = note is null || note == "" ?
                $"Food Id({foodId}) has a new report from user id: {userId}":
                $"Food Id({foodId}) has a new report from user id: {userId}, Note: {note}";

            notifyLst[1].Title = "Báo cáo mới";
            notifyLst[1].Content = note is null || note == "" ?
                $"Food Id({foodId}) có báo cáo mới từ người dùng id: {userId}" :
                $"Food Id({foodId}) có báo cáo mới từ người dùng id: {userId}, Ghi chú: {note}";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationPostReport(string receiver, int postId, string userId, string? note)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "New Report";
            notifyLst[0].Content = note is null || note == "" ?
                $"Post Id({postId}) has a new report from user id: {userId}" :
                $"Post Id({postId}) has a new report from user id: {userId}, Note {note}";

            notifyLst[1].Title = "Báo cáo mới";
            notifyLst[1].Content = note is null || note == "" ?
                $"Bài viết Id({postId}) có báo cáo mới từ người dùng id: {userId}" :
                $"Post Id({postId}) có báo cáo mới từ người dùng id: {userId}, Ghi chú: {note}";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationBanPost(string receiver, int postId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "Post Banned";
            notifyLst[0].Content = $"Post Id({postId}) has a banned based on user reports";

            notifyLst[1].Title = "Bài viết bị cấm";
            notifyLst[1].Content = $"Post Id({postId}) đã bị cấm dựa theo báo cáo của người dùng";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationUnBanPost(string receiver, int postId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "Post UnBanned";
            notifyLst[0].Content = $"Post Id({postId}) has been unbanned";

            notifyLst[1].Title = "Bài viết gỡ bị cấm";
            notifyLst[1].Content = $"Post Id({postId}) đã được gỡ cấm";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationBanFood(string receiver, int foodId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "Food Banned";
            notifyLst[0].Content = $"Food Id({foodId}) has a banned based on user reports";

            notifyLst[1].Title = "Món ăn bị cấm";
            notifyLst[1].Content = $"Món Id({foodId}) đã bị cấm dựa theo báo cáo của người dùng";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationUnBanFood(string receiver, int foodId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "Food UnBanned";
            notifyLst[0].Content = $"Food Id({foodId}) has been unbanned";

            notifyLst[1].Title = "Món ăn gỡ cấm";
            notifyLst[1].Content = $"Món Id({foodId}) đã được gỡ cấm";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationReplyFeedBack(string receiver, string contentFeedback)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "Reply Feedback";
            notifyLst[0].Content = $"Your feedback has been answered by the store. Content: {contentFeedback}";

            notifyLst[1].Title = "Trả lời phản hồi";
            notifyLst[1].Content = $"Feedback của bạn đã được cửa hàng trả lời. Nội dung: {contentFeedback}";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationOrderCustomerCancel(string receiver, int orderId, string note)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "Update order";
            notifyLst[0].Content = $"Order ID: {orderId} customer canceled. Reason: {note}";

            notifyLst[1].Title = "Cập nhật đơn hàng";
            notifyLst[1].Content = $"Đơn hàng ID: {orderId} khách hàng đã hủy. Lý do: {note}";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationFoodReportSeller(string receiver, int foodId, string foodName, string? note)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "New Report";
            notifyLst[0].Content = note is null || note == "" ?
                $"{foodName}({foodId}) has a new report" :
                $"{foodName}({foodId}) has a new report, Note: {note}";

            notifyLst[1].Title = "Báo cáo mới";
            notifyLst[1].Content = note is null || note == "" ?
                $"{foodName}({foodId}) có báo cáo mới" :
                $"{foodName}({foodId}) có báo cáo mới, Ghi chú: {note}";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationPostReportSeller(string receiver, int postId, string? note)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "New Report";
            notifyLst[0].Content = note is null || note == "" ?
                $"Post Id ({postId}) has a new report" :
                $"Post Id ({postId}) has a new report, Note {note}";

            notifyLst[1].Title = "Báo cáo mới";
            notifyLst[1].Content = note is null || note == "" ?
                $"Bài viết Id ({postId}) có báo cáo mới" :
                $"Bài viết Id ({postId}) có báo cáo mới, Ghi chú: {note}";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationApproveReport(string receiver, string reportName, string? note, int type)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");
            var objectReportEng = type == 0 ? "Food" : "Post";
            var objectReportVN = type == 0 ? "Món ăn" : "Bài viết";

            notifyLst[0].Title = $"{objectReportEng} Report";
            notifyLst[0].Content = note is null || note == "" ? 
                $"The report about the {objectReportEng} ({reportName}) was approved" :
                $"The report about the {objectReportEng} ({reportName}) was approved, Note: {note}";

            notifyLst[1].Title = $"Báo cáo {objectReportVN}";
            notifyLst[1].Content = note is null || note == "" ? 
                $"Báo cáo về {objectReportVN} ({reportName}) đã được chấp thuận" :
                $"Báo cáo về {objectReportVN} ({reportName}) đã được chấp thuận, Ghi chú: {note}";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationNotApproveReport(string receiver, string foodName, string note, int type)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");
            var objectReportEng = type == 0 ? "Food" : "Post";
            var objectReportVN = type == 0 ? "Món ăn" : "Bài viết";

            notifyLst[0].Title = $"{objectReportEng} Report";
            notifyLst[0].Content = note is null || note == "" ? 
                $"The report about the {objectReportEng} ({foodName}) was not approved" :
                $"The report about the {objectReportEng} ({foodName}) was not approved, Note: {note}";

            notifyLst[1].Title = $"Báo cáo {objectReportVN}";
            notifyLst[1].Content = note is null || note == "" ? 
                $"Báo cáo về {objectReportVN} ({foodName}) không được chấp thuận" :
                $"Báo cáo về {objectReportVN} ({foodName}) không được chấp thuận, Ghi chú: {note}";

            return notifyLst;
        }


        private List<NotificationAddNewInputDto> GenNotificationBase(string receiver, string sendBy, string notifyType)
        {
            NotificationAddNewInputDto inputNoti = new NotificationAddNewInputDto
            {
                CreateDate = DateTime.Now,
                SendBy = sendBy,
                Receiver = receiver,
                Type = NotificationTypeEnum.GetNotifyValue(notifyType)
            };

            List<NotificationAddNewInputDto> notifications = new List<NotificationAddNewInputDto>
            {
                new NotificationAddNewInputDto
                {
                    Lang = LangTypeEnum.GetLangValue("English"),
                    CreateDate = inputNoti.CreateDate,
                    SendBy = inputNoti.SendBy,
                    Receiver = inputNoti.Receiver,
                    Type = inputNoti.Type
                },
                new NotificationAddNewInputDto
                {
                    Lang = LangTypeEnum.GetLangValue("Vietnamese"),
                    CreateDate = inputNoti.CreateDate,
                    SendBy = inputNoti.SendBy,
                    Receiver = inputNoti.Receiver,
                    Type = inputNoti.Type
                }
            };

            return notifications;
        }
    }
}
