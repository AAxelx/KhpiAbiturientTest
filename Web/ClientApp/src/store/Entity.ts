
export interface Question{
    id: string;
    question: string;
    firstoption: string;
    secondoption: string;
    thirdoption: string;
}

export interface User {
    id: string;
    email: string;
    name: string;
}

export interface Answer {
    id: string;
    answer: string;
}