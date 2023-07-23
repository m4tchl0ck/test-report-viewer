# Hasura

Hasura is a backend for frontend, which expose the database via GraphQL queries.
And many more...

## My scripts

### Configure connection to Hasura instance and Postrgres

You need to start with creating file `.env` with your secrets

```
PG_DATABASE_URL=postgres://postgres:postgres@postgres:5432/postgres
HASURA_PROJECT_URL=http://your-other-instance.com
HASURA_ADMIN_SECRET=your-admin-secret
```
### Update medata files

When you change something in Hasura configuration you can update it with

```pwsh
update-metadata-from-cloud.ps1
```

### Migration

When you change something in database schema, you should create a migration

```pwsh
create-migration.ps1 <migration name>
```

### Deploy

When you change something in the files manually you need do deploy to Hasura instance

```pwsh
deploy-to-cloud.ps1
```
