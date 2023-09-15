"use client"

import { Suspense } from 'react';
import CircularProgress from '@mui/material/CircularProgress';
import Item from './components/item';
import { League } from '../../gql/generated';

import { useGetLeaguesQuery } from '../../gql/generated';

export default function Page() {

    const { data,
    isLoading,
    isSuccess,
    isError,
    error } = useGetLeaguesQuery();

    var leagues = data?.leagues as Array<League>;

    let content;

    // create a League component

    if (isLoading) {
        content = <CircularProgress/>
      } else if (isSuccess) {
        content = leagues.map(league => <Item key={league.id}  league={league}/>)
      } else if (isError) {
        content = <div>{error.toString()}</div>
      }

    return (
        <main>
            Leagues
            {content}
        </main>
    )
}