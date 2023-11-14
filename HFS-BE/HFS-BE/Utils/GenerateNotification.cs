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

        public NotificationAddNewInputDto GenNotificationInternalShipper(string receiver, int orderId, string shopName)
        {
            var notifyBase = GenNotificationBase(receiver, "System", "System");

            notifyBase.Title = "New Order";
            notifyBase.Content = $"Order ID: {orderId} transferred from shop: {shopName} for delivery";
            return notifyBase;
        }
        public NotificationAddNewInputDto GenNotificationNewOrderSeller(string receiver, int orderId)
        {
            var notifyBase = GenNotificationBase(receiver, "System", "System");
            notifyBase.Title = "New Order";
            notifyBase.Content = $"Order ID: {orderId} new orders were requested";
            return notifyBase;
        }
        public NotificationAddNewInputDto GenNotificationAddNewPost(string receiver, int postId)
        {
            var notifyBase = GenNotificationBase(receiver, "System", "System");
            notifyBase.Title = "New Post";
            notifyBase.Content = $"Post ID: {postId} is waiting for approval";
            return notifyBase;
        }
        public NotificationAddNewInputDto GenNotificationAddNewFood(string receiver, int menuId)
        {
            var notifyBase = GenNotificationBase(receiver, "System", "System");
            notifyBase.Title = "New Menu";
            notifyBase.Content = $"Menu ID: {menuId} is waiting for approval";
            return notifyBase;
        }

        public NotificationAddNewInputDto GenNotificationAcceptOrder(string receiver, int orderId)
        {
            var notifyBase = GenNotificationBase(receiver, "System", "System");
            notifyBase.Title = "Update order";
            notifyBase.Content = $"Order ID: {orderId} has been accepted";
            return notifyBase;
        }

        public NotificationAddNewInputDto GenNotificationCancelOrder(string receiver, int orderId)
        {
            var notifyBase = GenNotificationBase(receiver, "System", "System");
            notifyBase.Title = "Update order";
            notifyBase.Content = $"Order ID: {orderId} has been cancelled";
            return notifyBase;
        }

        public NotificationAddNewInputDto GenNotificationOrderShipping(string receiver, int orderId)
        {
            var notifyBase = GenNotificationBase(receiver, "System", "System");
            notifyBase.Title = "Update order";
            notifyBase.Content = $"Order ID: {orderId} is on the way";
            return notifyBase;
        }

        public NotificationAddNewInputDto GenNotificationOrderShippedSuccess(string receiver, int orderId)
        {
            var notifyBase = GenNotificationBase(receiver, "System", "System");
            notifyBase.Title = "Update order";
            notifyBase.Content = $"Order ID: {orderId} has been delivered successfully";
            return notifyBase;
        }

        public NotificationAddNewInputDto GenNotificationOrderShippedFail(string receiver, int orderId)
        {
            var notifyBase = GenNotificationBase(receiver, "System", "System");
            notifyBase.Title = "Update order";
            notifyBase.Content = $"Order ID: {orderId} was delivered unsuccessfully";
            return notifyBase;
        }


        private NotificationAddNewInputDto GenNotificationBase(string receiver, string sendBy, string notifyType)
        {
            NotificationAddNewInputDto inputNoti = new NotificationAddNewInputDto
            {
                CreateDate = DateTime.Now,
                SendBy = sendBy,
                Receiver = receiver,
                Type = NotificationTypeEnum.GetNotifyValue(notifyType)
            };

            return inputNoti;
        }
    }
}
