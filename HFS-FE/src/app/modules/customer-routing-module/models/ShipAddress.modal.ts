export class ShipAddress {
    addressId: number;
    customerId: string;
    addressInfo: string;
    isDefaultAddress: boolean;
}

export class CreateNewShipAddressInputValidation {
    isValidAddressInfo: boolean = true;
    addressInfoValidationMessage: string = "";
}

export class UpdateShipAddressInputValidation {
    isValidAddressId: boolean = true;
    addressIdValidationMessage: string = "";
    isValidAddressInfo: boolean = true;
    addressInfoValidationMessage: string = "";
}

export class DeleteShipAddressInputValidation {
    isValidAddressId: boolean = true;
    addressIdValidationMessage: string = "";
}

export class SetDefaultShipAddressInputValidation {
    isValidAddressId: boolean = true;
    addressIdValidationMessage: string = "";
}