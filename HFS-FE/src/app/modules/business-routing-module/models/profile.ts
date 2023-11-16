export class Profile {
    userId: string;
    firstName: string;
    lastName: string;
    gender: string;
    birthDate: string;
    roleId: number;
    email: string;
    phoneNumber: string;
    // avatar: string;
    shopName: string;
    shopAddress: string;
    isOnline: boolean;
    isVerified: boolean;
    walletBalance: number;
    manageBy: number;
}

export class ProfileDisplay {
    userId: string;
    firstName: string;
    lastName: string;
    gender: string;
    birthDate: string;
    roleIEd: number;
    email: string;
    phoneNumber: string;
    // avatar: string;
    shopName: string;
    shopAddress: string;
    isOnline: boolean;
    isVerified: boolean;
    walletBalance: number;
    manageBy: number;
}

export class ProfileImage {
    imageId: number;
    userId: number;
    path: string;
    isReplaced: boolean;
    imageBase64: string; // Base64 of the image name (with its path: Resource/...)
    size: string;
}

export class EditProfileInputValidation{
    isValidLastName: boolean = true;
    LastNameValidationMessage: string = null;
    isValidFirstName: boolean = true;
    FirstNameValidationMessage: string = null;
    isValidShopName: boolean = true;
    ShopNameValidationMessage: string = null;
    isValidShopAddress: boolean = true;
    ShopAddressValidationMessage: string = null;
  }