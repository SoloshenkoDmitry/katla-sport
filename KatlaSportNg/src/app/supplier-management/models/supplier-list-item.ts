export class SupplierListItem {
    constructor(
        public id: number,
        public companyName: string,
        public phone: string,
        public avatar: string,
        public isDeleted: boolean
    ) { }
}