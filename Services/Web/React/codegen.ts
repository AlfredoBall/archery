import type { CodegenConfig } from '@graphql-codegen/cli';

const config: CodegenConfig = {
  overwrite: true,
  schema: "https://localhost:44364/graphql",
  documents: "src/app/api/graphql/*.graphql",
  generates: {
    "src/gql/generated.ts": {
      // preset: "client",
      plugins: [
        'typescript',
        'typescript-operations',
        {
          'typescript-rtk-query': {
            importBaseApiFrom: 'src/app/api/baseApi',
            exportHooks: true
          }
        }
      ]
    },
    "./graphql.schema.json": {
      plugins: ["introspection"]
    }
  }
};

export default config;
