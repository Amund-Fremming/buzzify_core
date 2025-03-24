export enum Category {
    Random,
    Friendly,
    Dirty,
    Flirty,
    ForTheBoys,
    ForTheGirls,
}
export interface Challenge {
    id: number;
    roundId: number;
    participantsCount: number;
    text: string;
    readBeforeSpin: boolean;
}

export interface SpinGame {
    category: Category;
    state: SpinGameState;
    hubGroupName: string;
    players: SpinPlayer[];
    challenges: Challenge[];
    votes: SpinVote[];
    id: number;
    universalId: string;
    creatorId: number;
    creator: Player;
    name: string;
    iterationsCount: number;
    currentIteration: number;
}

export enum SpinGameState {
    Initialized,
    RoundStarted,
    Spinner,
    RoundFinished,
}
export interface SpinPlayer {
    id: number;
    spinGameId: number;
    playerId: number;
    host: boolean;
}

export interface SpinVote {
    id: number;
    gameId: number;
    playerId: number;
    active: boolean;
}

export interface CreateGameRequest {
    name: string;
    description: string;
}

export interface Player {
    id: number;
    hash: string;
    create: Player;
    empty: Player;
}

export interface AskGame {
    category: Category;
    state: AskGameState;
    description: string;
    questions: Question[];
    id: number;
    universalId: string;
    creatorId: number;
    creator: Player;
    name: string;
    iterationsCount: number;
    currentIteration: number;
}

export enum AskGameState {
    Initialized,
    Started,
    Finished,
}
export interface AskVote {
    id: number;
    gameId: number;
    playerId: number;
    active: boolean;
}

export interface Question {
    id: number;
    askGameId: number;
    text: string;
}

