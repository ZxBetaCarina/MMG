public class ServiceURLs
{
    public readonly string BaseUrl = "http://82.180.132.164:3900/api";
    public readonly string Login = "/gameUser/auth/login";
    public readonly string VerifyOtp = "/gameUser/auth/verifyOtp";
    public readonly string UpdateProfile = "/gameUser/auth/updateProfile";
    public readonly string GetProfile = "/gameUser/auth/getProfile";
    public readonly string GetWallet = "/gameUser/wallet/getWallet";
    public readonly string GetTransaction = "/gameUser/wallet/getTransactions?transactionType=credit&pointType=gaming";
    public readonly string GetWithdrawalRequest = "/gameUser/wallet/gameWithdrawalReq";
    public readonly string GetMyTickets = "/gameUser/getMyTickets";
    public readonly string Support = "/gameUser/sendSupport";
    public readonly string GetTermsPolicy = "/gameUser/getTermsPolicy";
    public readonly string GetAllGiveawayPrizes = "/admin/getAllGiveawayPrizes";
    public readonly string GetSettings = "/gameUser/getSettings";
    public readonly string UpdateSettings = "/gameUser/updateSettings";
}