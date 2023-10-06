import {AbstractAuditing} from "../../shared-module/models/abstract-auditing.model";

export class AppRole implements AbstractAuditing {

    constructor() {
    }

    roleId: string;
    roleKey: string;
    id: number;
    roleName: string;
    roleDescribe: string;
    active: boolean;
    createdBy: string;
    createdDate: any;
    lastModifiedBy: string;
    lastModifiedDate: any;
}
