export enum Category {
    Random,
    Friendly,
    Dirty,
    Flirty,
    ForTheBoys,
    ForTheGirls,
}
export interface Round {
    id: number;
    spinGameId: number;
    completed: boolean;
}

export enum SpinGameState {
    Initialized,
    RoundStarted,
    Spinner,
    RoundFinished,
}
export enum AskGameState {
    Initialized,
    Started,
    Finished,
}
