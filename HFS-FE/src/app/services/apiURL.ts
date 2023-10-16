// tslint:disable
export class PHAN_HE {

    public static USER = "User";
    public static ROLE = "Role";
    public static ORDER = "Order";
    public static VOUCHER = "Voucher";
    public static POST = "Post";
    public static TEST = "Test";
    public static POSTMODERATORMANAGEPOST = "manage";
}

export class API_POSTMODERATOR{
    public static GETPOST = "viewposts";
}

export class API_TEST{
    public static SIGNIN = "login";
}

// Service User
export class API_USER {
    public static SIGNIN = "signin";
    public static GET_INFO_CURRENT_USER = "getInfoCurrentUser";
    public static REGISTER = "register";
}

// Service Role
export class API_ROLE {
    public static GET_DVI_QLY = "getDviqly";
}

// Service Danh Mục
export class API_ORDER {
    public static GET_DVI_QLY = "getDviqly";
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

export class API_POST{
    public static LOGIN = "Identity";
    public static GET_ALL_LOGIN = "Account";
}

