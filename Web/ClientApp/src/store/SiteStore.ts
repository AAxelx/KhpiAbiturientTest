import { Action, Reducer } from 'redux';
import axios from 'axios'
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CounterState {
    question?: Question[],
    email?: string,
    answers?: Answers[],
    trueEmail?: boolean
    seconds?: number
}

export interface Question {
    id: string,
    question: string,
    first: string,
    second: string,
    third: string
}

export interface Answers {
    id: string,
    answer: string
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

export interface GetEmailAction { type: 'GET_EMAIL', trueemail: boolean, email: string }
export interface GetQuestionAction { type: 'GET_QUESTION', questions: Question[] }
export interface EditQuestionAction { type: 'EDIT_QUESTION', id:string, value: string }

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
export type KnownAction = GetEmailAction | GetQuestionAction | EditQuestionAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    sendEmail: (email: string): AppThunkAction<KnownAction> => (dispatch, getState) =>
    {
        axios.post("api/user/GetByEmai", email).
            then(_ => { dispatch({ type: 'GET_EMAIL', trueemail: _.data, email: email }) })
    },

    getQuestion: (): AppThunkAction<KnownAction> => (dispatch, getState) =>
    {
        axios.get('api/question/getall').
            then(_ => { dispatch({ type: 'GET_QUESTION', questions: _.data }) })
    },

    sendAnswers: (email: string, answers: Answers[]): AppThunkAction<KnownAction> => (dispatch, getState) => {
        axios.post("api/question/sendresult", { email: email, answers: answers })
    },

    editAnswers: (id: string, value: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        dispatch({ type: 'EDIT_QUESTION', id, value })
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer: Reducer<CounterState> = (state: CounterState | undefined, incomingAction: Action): CounterState => {
    if (state === undefined) {
        return { };
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'GET_EMAIL':
            {
                action.trueemail && actionCreators.getQuestion();
                return {
                    trueEmail: action.trueemail,
                    email: action.email
                };
            };
        case 'GET_QUESTION':
            var a: Answers[] = action.questions.map(_ => { return { id: _.id, answer: "" } });
            return {
                question: action.questions,
                answers: a,
            };
        //case 'EDIT_QUESTION':
        //    return {
        //        answers: state.answers!.map(_ => _.id === action.id ? { id: _.id, answer: action.value } : _)
        //    };
        default:
            return state;
    }
};
