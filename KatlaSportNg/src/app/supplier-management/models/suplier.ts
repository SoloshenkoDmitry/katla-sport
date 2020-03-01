export class Supplier {
    constructor(
        public id: number,
        public companyName: string,        
        public address: string,
        public phone: string,
        public isDeleted: boolean,
        public avatar: string,
        public lastUpdated: string = null,        
    ) { }
}
