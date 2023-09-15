import { api } from 'src/app/api/baseApi';
export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
export type MakeEmpty<T extends { [key: string]: unknown }, K extends keyof T> = { [_ in K]?: never };
export type Incremental<T> = T | { [P in keyof T]?: P extends ' $fragmentName' | '__typename' ? T[P] : never };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: { input: string; output: string; }
  String: { input: string; output: string; }
  Boolean: { input: boolean; output: boolean; }
  Int: { input: number; output: number; }
  Float: { input: number; output: number; }
  Any: { input: any; output: any; }
  DateTime: { input: any; output: any; }
};

export type CreateLeagueInput = {
  name: Scalars['Any']['input'];
};

export type CreateLeaguePayload = {
  __typename?: 'CreateLeaguePayload';
  league?: Maybe<League>;
};

export type CreatePlayerInput = {
  memberID: Scalars['Any']['input'];
  teamID: Scalars['Any']['input'];
};

export type CreatePlayerPayload = {
  __typename?: 'CreatePlayerPayload';
  player?: Maybe<Player>;
};

export type CreateReservationInput = {
  teamID: Scalars['Any']['input'];
  tournamentID: Scalars['Any']['input'];
};

export type CreateReservationPayload = {
  __typename?: 'CreateReservationPayload';
  reservation?: Maybe<Reservation>;
};

export type CreateTournamentInput = {
  endTime: Scalars['Any']['input'];
  laneID: Scalars['Any']['input'];
  leagueID: Scalars['Any']['input'];
  name: Scalars['Any']['input'];
  startTime: Scalars['Any']['input'];
};

export type CreateTournamentPayload = {
  __typename?: 'CreateTournamentPayload';
  tournament?: Maybe<Tournament>;
};

export type DeleteTournamentInput = {
  tournamentID: Scalars['Any']['input'];
};

export type DeleteTournamentPayload = {
  __typename?: 'DeleteTournamentPayload';
  boolean?: Maybe<Scalars['Boolean']['output']>;
};

export type Lane = {
  __typename?: 'Lane';
  id: Scalars['Int']['output'];
  identifier: Scalars['Int']['output'];
  tournaments: Array<Tournament>;
};

export type League = {
  __typename?: 'League';
  id: Scalars['Int']['output'];
  lanes: Array<Lane>;
  name: Scalars['String']['output'];
  players: Array<Player>;
  teams: Array<Team>;
  tournaments: Array<Tournament>;
};

export type Member = {
  __typename?: 'Member';
  id: Scalars['Int']['output'];
  identityKey: Scalars['String']['output'];
  name: Scalars['String']['output'];
  player: Player;
};

export type Mutation = {
  __typename?: 'Mutation';
  createLeague: CreateLeaguePayload;
  createPlayer: CreatePlayerPayload;
  createReservation: CreateReservationPayload;
  createTournament: CreateTournamentPayload;
  deleteTournament: DeleteTournamentPayload;
  updateLeague: UpdateLeaguePayload;
  updateTournament: UpdateTournamentPayload;
};


export type MutationCreateLeagueArgs = {
  input: CreateLeagueInput;
};


export type MutationCreatePlayerArgs = {
  input: CreatePlayerInput;
};


export type MutationCreateReservationArgs = {
  input: CreateReservationInput;
};


export type MutationCreateTournamentArgs = {
  input: CreateTournamentInput;
};


export type MutationDeleteTournamentArgs = {
  input: DeleteTournamentInput;
};


export type MutationUpdateLeagueArgs = {
  input: UpdateLeagueInput;
};


export type MutationUpdateTournamentArgs = {
  input: UpdateTournamentInput;
};

export type Player = {
  __typename?: 'Player';
  id: Scalars['Int']['output'];
  member: Member;
  memberID: Scalars['Int']['output'];
  scores: Array<Score>;
  team: Team;
  teamID: Scalars['Int']['output'];
};

export type Query = {
  __typename?: 'Query';
  lanes: Array<Lane>;
  leagues: Array<League>;
  members: Array<Member>;
  players: Array<Player>;
  reservations: Array<Reservation>;
  scores: Array<Score>;
  sets: Array<Set>;
  teams: Array<Team>;
  tournaments: Array<Tournament>;
};

export type Reservation = {
  __typename?: 'Reservation';
  id: Scalars['Int']['output'];
  team: Team;
  teamID: Scalars['Int']['output'];
  tournament: Tournament;
  tournamentID: Scalars['Int']['output'];
};

export type Score = {
  __typename?: 'Score';
  id: Scalars['Int']['output'];
  player: Player;
  playerID: Scalars['Int']['output'];
  points: Scalars['Int']['output'];
  set: Set;
  setID: Scalars['Int']['output'];
};

export type Set = {
  __typename?: 'Set';
  id: Scalars['Int']['output'];
  ordinal: Scalars['Int']['output'];
  scores: Array<Score>;
  tournament: Tournament;
  tournamentID: Scalars['Int']['output'];
};

export type Team = {
  __typename?: 'Team';
  id: Scalars['Int']['output'];
  name: Scalars['String']['output'];
  players: Array<Player>;
  reservations: Array<Reservation>;
  tournaments: Array<Tournament>;
};

export type Tournament = {
  __typename?: 'Tournament';
  endTime: Scalars['DateTime']['output'];
  id: Scalars['Int']['output'];
  lane: Lane;
  laneID: Scalars['Int']['output'];
  league: League;
  leagueID: Scalars['Int']['output'];
  name: Scalars['String']['output'];
  players: Array<Player>;
  reservations: Array<Reservation>;
  sets: Array<Set>;
  startTime: Scalars['DateTime']['output'];
  teams: Array<Team>;
};

export type UpdateLeagueInput = {
  id: Scalars['Any']['input'];
  name: Scalars['Any']['input'];
};

export type UpdateLeaguePayload = {
  __typename?: 'UpdateLeaguePayload';
  league?: Maybe<League>;
};

export type UpdateTournamentInput = {
  endTime: Scalars['Any']['input'];
  name: Scalars['Any']['input'];
  startTime: Scalars['Any']['input'];
  tournamentID: Scalars['Any']['input'];
};

export type UpdateTournamentPayload = {
  __typename?: 'UpdateTournamentPayload';
  tournament?: Maybe<Tournament>;
};

export type GetLeaguesQueryVariables = Exact<{ [key: string]: never; }>;


export type GetLeaguesQuery = { __typename?: 'Query', leagues: Array<{ __typename?: 'League', name: string, id: number }> };

export type GetTournamentsQueryVariables = Exact<{ [key: string]: never; }>;


export type GetTournamentsQuery = { __typename?: 'Query', tournaments: Array<{ __typename?: 'Tournament', name: string }> };

export type UpdateLeagueMutationVariables = Exact<{
  input: UpdateLeagueInput;
}>;


export type UpdateLeagueMutation = { __typename?: 'Mutation', updateLeague: { __typename?: 'UpdateLeaguePayload', league?: { __typename?: 'League', name: string, id: number } | null } };


export const GetLeaguesDocument = `
    query getLeagues {
  leagues {
    name
    id
  }
}
    `;
export const GetTournamentsDocument = `
    query getTournaments {
  tournaments {
    name
  }
}
    `;
export const UpdateLeagueDocument = `
    mutation updateLeague($input: UpdateLeagueInput!) {
  updateLeague(input: $input) {
    league {
      name
      id
    }
  }
}
    `;

const injectedRtkApi = api.injectEndpoints({
  endpoints: (build) => ({
    getLeagues: build.query<GetLeaguesQuery, GetLeaguesQueryVariables | void>({
      query: (variables) => ({ document: GetLeaguesDocument, variables })
    }),
    getTournaments: build.query<GetTournamentsQuery, GetTournamentsQueryVariables | void>({
      query: (variables) => ({ document: GetTournamentsDocument, variables })
    }),
    updateLeague: build.mutation<UpdateLeagueMutation, UpdateLeagueMutationVariables>({
      query: (variables) => ({ document: UpdateLeagueDocument, variables })
    }),
  }),
});

export { injectedRtkApi as api };
export const { useGetLeaguesQuery, useLazyGetLeaguesQuery, useGetTournamentsQuery, useLazyGetTournamentsQuery, useUpdateLeagueMutation } = injectedRtkApi;

