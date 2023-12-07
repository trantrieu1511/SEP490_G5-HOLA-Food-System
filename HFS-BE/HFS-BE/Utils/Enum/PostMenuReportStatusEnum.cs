namespace HFS_BE.Utils.Enum
{
    public static class PostMenuReportStatusEnum
    {
        public enum PostMenuReportStatus
        {
            Pending = 0, // KH moi tao report, post/menu mod chua xu ly report.
            Approved = 1, // Post/Menu mod chap nhan to cao va da xu ly xong thuc pham bi to cao.
            NotApproved = 2, // Post/Menu mod khong chap nhan report (Co le do nguoi report k neu ra duoc noi dung to cao mot cach nghiem tuc)
            Cancelled = 3 // Khach hang report mot post nao do co the cancel cai report voi ly do cu the
        }

        /// <summary>
        /// Get the status string used for display on the API's response body (output)
        /// </summary>
        /// <param name="status">status that is 0/1/2 according to the enum</param>
        /// <returns>The string representation of the status</returns>
        public static string GetStatusString(byte status)
        {
            switch (status)
            {
                case (int)PostMenuReportStatus.Pending:
                    return "Pending";
                case (int)PostMenuReportStatus.Approved:
                    return "Approved";
                case (int)PostMenuReportStatus.NotApproved:
                    return "NotApproved";
                case (int)PostMenuReportStatus.Cancelled:
                    return "Cancelled";
                default:
                    return "unknown";
            }
        }
    }
}
