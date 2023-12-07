namespace HFS_BE.Utils.Enum
{
    public class LangTypeEnum
    {
        public enum LangType
        {
            VI = 0,
            EN = 1
        }

        public static string GetLangtring(int type)
        {
            switch (type)
            {
                case (int)LangType.VI:
                    return "Vietnamese";
                case (int)LangType.EN:
                    return "English";
                default:
                    return "unknown";
            }
        }

        public static int GetLangValue(string type)
        {
            switch (type)
            {
                case "Vietnamese":
                case "VI":
                case "vi":
                    return (int)LangType.VI;
                case "English":
                case "EN":
                case "en":
                    return (int)LangType.EN;
                default:
                    return -1;
            }
        }
    }
}
