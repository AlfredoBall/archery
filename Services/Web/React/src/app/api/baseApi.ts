import { createApi } from '@reduxjs/toolkit/query/react'
import { GraphQLClient, request, ClientError } from 'graphql-request'
import { graphqlRequestBaseQuery } from '../graphqlRequestBaseQuery';


 
export const client = new GraphQLClient('https://localhost:44364/graphql/', {
  // credentials: `include`,
  errorPolicy: 'all',
  mode: `cors`
});
 
export const api = createApi({
  // baseQuery: baseQuery,
  baseQuery: graphqlRequestBaseQuery({ client }),
  endpoints: () => ({})
});

