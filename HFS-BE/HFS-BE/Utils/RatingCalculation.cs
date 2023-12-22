using HFS_BE.Models;

namespace HFS_BE.Utils
{
    public static class RatingCalculation
    {
        public static int CalculateFoodStar(List<Feedback> feedbacks)
        {
            // no enough emount feedback -> no calculate
            /*if(feedbacks.Count < 5)
                return 0;*/

            var starSum = feedbacks.Sum(feedback => feedback.Star ?? 0);

            // 4-> 4,5 : round = 4
            // 4,6 -> 4,9 : round = 5
            return (int)Math.Round((double)starSum / feedbacks.Count, MidpointRounding.AwayFromZero);
        }
    }
}
