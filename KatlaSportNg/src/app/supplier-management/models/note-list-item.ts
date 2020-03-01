export class NoteListItem {
    constructor(
        public id: number,
        public parentId: number,
        public note: string,
        public isDeleted: boolean,
        public supplierId: number
    ) { }
}