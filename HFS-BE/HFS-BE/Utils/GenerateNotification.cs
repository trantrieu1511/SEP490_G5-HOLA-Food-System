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
            notifyLst[0].Content = $"New Post ID: {postId} is waiting for approval";

            notifyLst[1].Title = "Bài post mới";
            notifyLst[1].Content = $"Bài post mới ID: {postId} đang chờ để được chấp nhận";

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

        public List<NotificationAddNewInputDto> GenNotificationFoodReport(string receiver, int foodId, string userId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "New Report";
            notifyLst[0].Content = $"Food Id({foodId}) has a new report from user id: {userId}";

            notifyLst[1].Title = "Báo cáo mới";
            notifyLst[1].Content = $"Food Id({foodId}) có báo cáo mới từ người dùng id: {userId}";

            return notifyLst;
        }

        public List<NotificationAddNewInputDto> GenNotificationPostReport(string receiver, int postId, string userId)
        {
            var notifyLst = GenNotificationBase(receiver, "System", "System");

            notifyLst[0].Title = "New Report";
            notifyLst[0].Content = $"Post Id({postId}) has a new report from user id: {userId}";

            notifyLst[1].Title = "Báo cáo mới";
            notifyLst[1].Content = $"Post Id({postId}) có báo cáo mới từ người dùng id: {userId}";

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
