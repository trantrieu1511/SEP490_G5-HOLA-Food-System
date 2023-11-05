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
}

export class API_TEST{
    public static SIGNIN = "login";
}

// Service User
export class API_USER {
    public static SIGNIN = "signin";
    public static GET_INFO_CURRENT_USER = "getInfoCurrentUser";
    public static REGISTER = "register";
    public static GETPROFILE = "profile";
    public static EDITPROFILE = "editprofile";
    public static GET_SHIPPERS_AVAILABLE = "getShippersAvailable";
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
}

export class API_SHIPPER {
    public static GET_All = "order";
    public static CHANGE_STATUS = "orderprogress";
    public static HISTORY = "history";
}
export class API_MANAGE{
  public static LIST_CUS = "listcustomer";
  public static BAN_CUS = "bancustomer";
   public static HIS_CUS = "bancustomerhistory";
    public static LIST_SELLER = "listcustomer";
    public static LIST_SHIPPER = "listcustomer";
    public static LIST_PM = "listpostmoderator";
    public static ADD_PM = "addpostmoderator";
    public static LIST_MM = "listcustomer";
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

}

// API related to post entity
export class API_POST{
    public static ADD_POST_SELLER = "addPostSeller";
    public static UPDATE_POST = "updatePost";
    // public static GET_POST_SELLER = "getPostsSeller";
    public static GET_POST = "getPosts"; // Ong post mod va seller dung chung. Em phan quyen cho no r - Trieu
    public static ENABLE_DISABLE_SELLER = "enableDisableSeller";
    public static BAN_UNBAN = "banunban";
}

export class API_HOME{
    public static DISPLAY_SHOP = "displayshop";
}

export class API_SHOP_DETAIL{
    public static DISPLAY_MENU = "foods";
    public static DISPLAY_INFOR = "shopinfor";
}

export class API_CART{
    public static ADDTOCART = "addtocart"
    public static CART_DETAIL = "getcartitem"
    public static UPDATE_AMOUNT = "updateamount"
    public static DELETE_ITEM = "deleteitem"
}

export class API_FOOD{
    public static ADD_FOOD = "addNewFood";
    public static UPDATE_FOOD = "updateFood";
    // public static GET_FOOD_SELLER = "getFoodsSeller";
    public static GET_FOOD = "getFoods"; // Danh cho ca seller va menu mod. Em de dieu kien cho cai api roi - Trieu
    public static ENABLE_DISABLE = "enableDisable";
    public static BAN_UNBAN = "banunban";
}

export class API_CHECKOUT{
    public static CREATE_ORDER = "createorder";
    public static GET_ADDRESS = "address";
}

export class API_HUB{
    public static DATA_REALTIME = "dataRealTime";
}
