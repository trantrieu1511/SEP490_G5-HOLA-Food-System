// tslint:disable
export class PHAN_HE {

    public static USER = "User";
    public static ROLE = "Role";
    public static ORDER = "Order";
    public static VOUCHER = "Voucher";
    public static POST = "Post";
    public static TEST = "Test";
    public static SHIPPER = "Shipper";
    public static HOME = "Home"
    public static SHOP_DETAIL = "shopDetail"
    public static CART = "cart"
    public static FOOD = "Food"
    public static CHECKOUT = "checkout"
    public static HUB = "hub"
    public static NOTIFY = "notify"
    public static FOODETAIL = "fooddetail"
    public static ORDERCUSTOMER = "order"
    public static POSTREPORT = "postreport";
    public static FOODREPORT = "foodreport";
    public static PROFILEIMAGE = "profileImage";
    public static PAYMENT = "payment";
    public static WALLET = "wallet";
    public static NEWFEED = "newfeed";
    public static CUSTOMER = "customer";
    public static AUTH = "auth";
    public static DASHBOARD = "dashboard";
}

export class API_TEST {
    public static SIGNIN = "login";
}

export class API_NEWFEED {
    public static GETALLPOST = "getNewFeed";
    public static GetALLCOMMENT = "getcomment";
    public static CREATECOMMENT = "comment";
    public static VOTE_POST = "vote";
}
// Service User
export class API_USER {
  public static REGISTER_SELLER = "registerseller";
  public static REGISTER_SHIPPER = "registershipper";
    public static SIGNIN = "signin";
    public static GET_INFO_CURRENT_USER = "getInfoCurrentUser";
    public static REGISTER = "register";
    public static GETPROFILE = "profile";
    public static GETLICENSEIMAGE = "getAllLicenseImages";
    public static EDITPROFILE = "editprofile";
    public static EDITLICENSEIMAGE = "editLicenseImage";
    public static EDITIDCARD = "editidcard";
    public static VERIFYUSERIDENTITY = "verifyuseridentity";
    public static CHANGEACCOUNTPASSWORD = "changeaccountpassword";
    public static GET_SHIPPERS_AVAILABLE = "getShippersAvailable";
    public static GET_SHIPPERS_BYSELLER = "listshipperbyseller";
    public static GET_SHIPPERS_INVITATION = "listinvitationshipper";
    public static ADD_SHIPPERS_INVITATION = "invitationshipper";
    public static KICK_SHIPPERS = "kickshipper";
    public static DASHBOAD_ADMIN = "dashboadpie";
    public static DASHBOAD_ADMINTOTAL = "dashboadtotal";
    public static FEEDBACK_SELLER = "sellerFeedBack";
    public static REPLY_SELLER = "replyfeedback";
     public static REPORT_SELLER = "reportseller";
     public static REPORT_SELLERCUS = "listreportsellerbyCustomer";
     public static SEND_OTP = "sendotp";
     public static CHECK_OTP = "checkotp";
     public static MAP = "map";

}

// Service Role
export class API_ROLE {
    public static GET_DVI_QLY = "getDviqly";
}

// Service Danh Mục
export class API_ORDER {
    public static GET_ORDER_BY_STATUS = "getOrdersSeller";
    public static CANCEL_ORDER = "cancelOrderSeller";
    public static ACCEPT_ORDER = "acceptOrderSeller";
    public static INTERNAL_ORDER = "internalShipperOrderSeller";
    public static EXTERNAL_ORDER = "externalShipperOrderSeller";
    public static FEEDBACK = "addnewfeedback";

}

export class API_SHIPPER {
    public static GET_All = "order";
    public static CHANGE_STATUS = "orderprogress";
    public static HISTORY = "history";
    public static LIST_INV = "listinvitationshipperbyshipper";
    public static ACCEPT = "acceptshipper";
}
export class API_MANAGE{
  public static LIST_CUS = "listcustomer";
  public static BAN_CUS = "bancustomer";
   public static HIS_CUS = "bancustomerhistory";
    public static LIST_SELLER = "listseller";
    public static BAN_SELLER = "banseller";
    public static ACTIVE_SELLER = "activeseller";
    public static REJECT_SELLER = "rejectseller";
    public static HIS_SELLER = "bansellerhistory";
    public static HIS_SHIPPER = "banshipperhistory";
    public static LIST_SHIPPER = "listshipperbyadmin";
    public static BAN_SHIPPER = "banshipper";
    public static ACTVIVE_SHIPPER = "activeshipper";
    public static LIST_PM = "listpostmoderator";
    public static ADD_PM = "addpostmoderator";
    public static BAN_PM = "banpostmoderator";
    public static LIST_MM = "listmenumoderator";
    public static LIST_AC = "listaccountant";
    public static ADD_MM = "addmenumoderator";
    public static ADD_AC = "addaccountants";
    public static BAN_MM = "banmenumoderator";
    public static BAN_AC = "banaccountant";
    public static LIST_CATEGORY = "getallcategory";
    public static EDIT_CATE = "updateCategory";
    public static ADD_CATE = "createcategory";
    public static ED_CATE = "edcategory";
    public static ENABLE_DISABLE_CATE ="edcategory"
    public static LIST_REPORT = "listreportseller";
    public static REPLY_REPORT = "updatereportseller";
}

// Service QTHT
export class API_VOUCHER {
    public static GET_ALL_ROLE = "getAllRole";
    public static GET_ALL_MENU = "getAllMenu";
    public static INSERT_APP_ROLE = "insertAppRole";
    public static UPDATE_APP_ROLE = "updateAppRole";
    public static DELETE_APP_ROLE = "deleteAppRole";
    public static DELETE_LIST_APP_ROLE = "deleteListAppRole";
    public static INSERT_APP_MENU = "insertAppMenu";
    public static UPDATE_APP_MENU = "updateAppMenu";
    public static DELETE_APP_MENU = "deleteAppMenu";
    public static DELETE_LIST_APP_MENU = "deleteListAppMenu";
    public static GET_ALL_USER = "getAllUser";
    public static INSERT_APP_USER = "insertAppUser";
    public static UPDATE_APP_USER = "updateAppUser";
    public static DELETE_APP_USER = "deleteAppUser";
    public static DELETE_LIST_APP_USER = "deleteListAppUser";
    public static GET_ALL_VOUCHER = "getListvoucher";
    public static CREATE_VOUCHER = "addNewVoucher";
    public static EDIT_VOUCHER = "updatevoucher";
    public static ENABLE_DISABLE_VOUCHER = "Enable_Disable"
}

// API related to post entity
export class API_POST {
    public static ADD_POST_SELLER = "addPostSeller";
    public static UPDATE_POST = "updatePost";
    // public static GET_POST_SELLER = "getPostsSeller";
    public static GET_POST = "getPosts"; // Ong post mod va seller dung chung. Em phan quyen cho no r - Trieu
    public static ENABLE_DISABLE_SELLER = "enableDisableSeller";
    public static BAN_UNBAN = "banunban";
}

export class API_HOME {
    public static DISPLAY_SHOP = "displayshop";
    public static HOT_FOOD = "hotfoods";
    public static SEARCH = "search";
    public static GET_CATEGORY = "getcategory";
    public static SEND_CONFIRM_EMAIL = "sendconfirm";
}

export class API_SHOP_DETAIL {
    public static DISPLAY_MENU = "foods";
    public static DISPLAY_INFOR = "shopinfor";
}

export class API_CART {
    public static ADDTOCART = "addtocart"
    public static CART_DETAIL = "getcartitem"
    public static UPDATE_AMOUNT = "updateamount"
    public static DELETE_ITEM = "deleteitem"
    public static CART_ITEM_POPUP = "getcartitempopup"
}

export class API_FOOD {
    public static ADD_FOOD = "addNewFood";
    public static UPDATE_FOOD = "updateFood";
    // public static GET_FOOD_SELLER = "getFoodsSeller";
    public static GET_FOOD = "getFoods"; // Danh cho ca seller va menu mod. Em de dieu kien cho cai api roi - Trieu
    public static ENABLE_DISABLE = "enableDisable";
    public static BAN_UNBAN = "banunban";
}

export class API_CHECKOUT {
    public static CREATE_ORDER = "createorder";
    public static GET_ADDRESS = "address";
    public static GET_VOUCHER = "checkvoucher";
}

export class API_HUB {
    public static DATA_REALTIME = "dataRealTime";
    public static NOTIFY_REALTIME = "notifyRealTime";
}

export class API_NOTIFY {
    public static GET_ALL_NOTIFIES = "getAllNotify";
    public static UPDATE_NOTIFY = "updateNotify";
    public static MARK_ALL_NOTIFY_READ = "markAllRead";
    public static GET_DETAIL_NOTIFY = "getDetailNotify";
}

export class API_FOODDETAIL {
    public static GET_FOOD = "getfood";
    public static GET_SIMILARFOOD = "similarfood"
    public static GET_FEEDBACK = "feedbck"
    public static VOTE_FEEDBACK = "vote"
    public static GET_FEEDBACK_IMAGE = "getfeedback"
}

export class API_ORDERCUSTOMER {
    public static GET_ORDER = "history";
    public static CANCEL_ORDER = "cancel";
    public static RATE_FOOD = "feedback"
    public static SHIPPER_INFOR = "shipperinfo"
}

export class API_POSTREPORT {
    public static GET_ALL_POSTREPORT = "getallpostreports";
    public static APPROVE_NOTAPPROVE_POSTREPORT = "approvenotapprovepostreport";
    public static CREATE_NEW_POSTREPORT = "createnewpostreport"
}

export class API_FOODREPORT {
    public static GET_ALL_FOODREPORT = "getallfoodreports";
    public static APPROVE_NOTAPPROVE_FOODREPORT = "approvenotapprovefoodreport";
    public static CREATE_NEW_FOODREPORT = "createnewfoodreport"
}

export class API_PROFILEIMAGE {
    public static GET_PROFILEIMAGE = "getProfileImage";
    public static IMPORT_PROFILEIMAGE = "importProfileImage";
}

export class API_PAYMENT {
    public static GET_URL = "getpaymenturl"
    public static VERIFY = "verify"
}

export class API_SHIPADDRESS {
    public static GETALLSHIPADDRESS = "getallshipaddress";
    public static CREATENEWSHIPADDRESS = "createnewshipaddress";
    public static UPDATESHIPADDRESS = "updateshipaddress";
    public static DELETESHIPADDRESS = "deleteshipaddress";
    public static SETDEFAULTSHIPADDRESS = "setdefaultshipaddress";
}
export class API_AUTH {
    public static REFRESH_TOKEN = "refresh";
    public static REVOKE_TOKEN = "revoke";
}

export class API_DASHBOARD {
    public static DASHBOARD_SELLER = "dashboardSeller";
    public static  DASHBOARD_SHIPPER = "dashboardshipper";
    public static DASHBOARD_SHIPPER_TOTAL = "dashboardshippertotal";
    public static  DASHBOARD_ADMINLINE = "dashboadadminline";
    // ---------------- Post moderator ------------------
    public static DASHBOARD_POSTMOD_ALLTIMESTATISTICS = "getalltimestatisticspostmoderator";
    // public static DASHBOARD_POSTMOD_THISMONTHSTATISTICS = "getthismonthstatisticspostmoderator";
    public static DASHBOARD_POSTMOD_MYSTATISTICS = "getMyStatisticsPostmod";
    public static DASHBOARD_POSTMOD_SYSTEMSTATISTICS = "getSystemStatisticsPostmod";

    // ---------------- Menu moderator ------------------
    public static DASHBOARD_MENUMOD_ALLTIMESTATISTICS = "getAllTimeStatisticsMenuModerator";
    public static DASHBOARD_MENUMOD_MYSTATISTICS = "getMyStatisticsMenumod";
    public static DASHBOARD_MENUMOD_SYSTEMSTATISTICS = "getSystemStatisticsMenumod";

}

export class API_WALLET {
    public static GET_BAlANCE = "getbalance"
    public static HISTORY = "history"
    public static SEND_CODE = "sendmail"
    public static VERIFY = "verifycode"
    public static WITHDRAW = "withdraw"
    public static UPDATE_WITHDRAW = "updatewithdrawstatus"
    public static DASH_BOARD = "dashboard"
}
