using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Utils
{
    public class GenerateNotification
    {
        private static readonly GenerateNotification _singleton = new GenerateNotification();

        private GenerateNotification() { }

        public static GenerateNotification GetSingleton()
        {
            return _singleton;
        }

        public void GenNotificationInternalShipper(NotificationAddNewInputDto input, int orderId, string shopName)
        {
            input.Title = "New Order";
            input.Content = $"Order ID: {orderId} transferred from shop: {shopName} for delivery";
        }
        public void GenNotificationNewOrderSeller(NotificationAddNewInputDto input, int orderId)
        {
            input.Title = "New Order";
            input.Content = $"Order ID: {orderId} new orders were requested";
        }
        public void GenNotificationAddNewPost(NotificationAddNewInputDto input, int postId)
        {
            input.Title = "New Post";
            input.Content = $"Post ID: {postId} is waiting for approval";
        }
        public void GenNotificationAddNewFood(NotificationAddNewInputDto input, int menuId)
        {
            input.Title = "New Menu";
            input.Content = $"Menu ID: {menuId} is waiting for approval";
        }
    }
}
