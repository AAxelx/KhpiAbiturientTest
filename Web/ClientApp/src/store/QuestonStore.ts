import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';
import { Answer, Question, User } from './Entity';
import axios from 'axios'

export interface QuestinorState {
    user: User;
    questions: Question[];
}

interface PostAnswers {
    user: User;
    answers: Answer[];
}

export interface GetQuestionsAction {
    type: 'GET_QUESTION',
    questions: Question[];
}
export interface PostUserAction {
    type: 'POST_ANSWER',
    
}

export type KnownAction = GetQuestionsAction | PostUserAction;

export const actionCreators = {
    getQuestion: (user: User): AppThunkAction<KnownAction> => (dispatch, getState) => {
        axios.post("api/getquestion", user)
            .then(({ data }) => dispatch({ type: 'GET_QUESTION', questions: data}))
    },
    postAnswers: (post: PostAnswers): AppThunkAction<KnownAction> => (dispatch, getState) => {
        axios.post("api/getquestion", post)
            .then(({ data }) => dispatch({ type: 'POST_ANSWER' }))
    },
};

const unloadedState: QuestinorState = { user: null, questions: [] };

export const reducer: Reducer<QuestinorState> = (state: QuestinorState | undefined, incomingAction: Action): QuestinorState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'GET_QUESTION':
            return { ...state, questions: action.questions };
        case 'POST_ANSWER':
            return { ...state };
        default:
            return state;
    }
};

