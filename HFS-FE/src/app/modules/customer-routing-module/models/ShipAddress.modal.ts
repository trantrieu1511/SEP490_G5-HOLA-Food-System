export class ShipAddress {
    addressId: number;
    customerId: string;
    addressInfo: string;
    detailAddressInfo: string;
    isDefaultAddress: boolean;
}

export class CreateNewShipAddressInputValidation {
    isValidDetailAddressInfo: boolean = true;
    detailAddressInfoValidationMessage: string = "";
    isValidProvince: boolean = true;
    provinceValidationMessage: string = "";
    isValidDistrict: boolean = true;
    districtValidationMessage: string = "";
    isValidWard: boolean = true;
    wardValidationMessage: string = "";
}

export class UpdateShipAddressInputValidation {
    isValidAddressId: boolean = true;
    addressIdValidationMessage: string = "";
    isValidDetailAddressInfo: boolean = true;
    detailAddressInfoValidationMessage: string = "";
    isValidProvince: boolean = true;
    provinceValidationMessage: string = "";
    isValidDistrict: boolean = true;
    districtValidationMessage: string = "";
    isValidWard: boolean = true;
    wardValidationMessage: string = "";
}

export class DeleteShipAddressInputValidation {
    isValidAddressId: boolean = true;
    addressIdValidationMessage: string = "";
}

export class SetDefaultShipAddressInputValidation {
    isValidAddressId: boolean = true;
    addressIdValidationMessage: string = "";
}