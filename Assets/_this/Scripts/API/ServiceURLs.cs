public class ServiceURLs
{
    public static readonly string BaseUrl = "https://admin.aimaxonmarketing.com/api/";
    public static readonly string Login = BaseUrl + "/gameUser/auth/login";
    public static readonly string VerifyOtp = BaseUrl + "/gameUser/auth/verifyOtp";
    public static readonly string UpdateProfile = BaseUrl + "/gameUser/auth/updateProfile";
    public static readonly string GetProfile = BaseUrl + "/gameUser/auth/getProfile";
    public static readonly string GetWallet = BaseUrl + "/gameUser/wallet/getWallet";
    public static readonly string UpdateWallet = BaseUrl + "/gameUser/wallet/updateWallet";
    public static readonly string GetTransaction = BaseUrl + "/gameUser/wallet/getTransactions?transactionType=credit&pointType=gaming";
    public static readonly string GetWithdrawalRequest = BaseUrl + "/gameUser/wallet/gameWithdrawalReq";
    public static readonly string GetMyTickets = BaseUrl + "/gameUser/getMyTickets";
    public static readonly string Support = BaseUrl + "/gameUser/sendSupport";
    public static readonly string GetTermsPolicy = BaseUrl + "/gameUser/getTermsPolicy";
    public static readonly string GetAllGiveawayPrizes = BaseUrl + "/admin/getAllGiveawayPrizes";
    public static readonly string GetSettings = BaseUrl + "/gameUser/getSettings";
    public static readonly string UpdateSettings = BaseUrl + "/gameUser/updateSettings";
    public static readonly string SendRequest = BaseUrl + "/gameUser/sendRequest";
    public static readonly string Image = "https://admin.aimaxonmarketing.com/api/Uploads/";
}