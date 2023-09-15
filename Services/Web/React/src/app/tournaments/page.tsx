"use client"

import { Suspense } from 'react';
import CircularProgress from '@mui/material/CircularProgress';
import { Tournament } from '../../gql/generated';

import { useGetTournamentsQuery } from '../../gql/generated';

export default function Page() {

    const { data,
    isLoading,
    isSuccess,
    isError,
    error } = useGetTournamentsQuery();

    var tournaments = data?.tournaments as Array<Tournament>;

    let content;

    // create a League component

    if (isLoading) {
        content = <CircularProgress/>
      } else if (isSuccess) {
        content = tournaments.map(tournament => <div key={tournament.id}>{tournament.name}</div>)
      } else if (isError) {
        content = <div>{error.toString()}</div>
      }

    return (
        <main>
            Tournaments
            {content}
        </main>
    )
}