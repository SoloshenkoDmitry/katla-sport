export class Note {
    constructor(
        public id: number,
        public parentId: number,
        public note: string,
        public isDeleted: boolean,
        public supplierId: number,
        public lastUpdated: string
    ) { }
}