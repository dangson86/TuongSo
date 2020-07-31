export interface IUserInput {
    name?: string;
    nickName?: string;
    day?: string;
    month?: string;
    year?: string;
}

export interface IBirthGrid {
    name: INumberGrid;
    date: INumberGrid;
}
export interface INumberGrid {
    n1: number[];
    n2: number[];
    n3: number[];
    n4: number[];
    n5: number[];
    n6: number[];
    n7: number[];
    n8: number[];
    n9: number[];
}

