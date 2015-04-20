module app.contacts.models {

    export interface IContactInfo {
        ContactId: number;
        FirstName: string;
        LastName: string;
    }

    export interface IContact {
        ContactId: number;
        FirstName: string;
        LastName: string;
        IsActive: boolean;
        ContactTypeId: number;
    }
}