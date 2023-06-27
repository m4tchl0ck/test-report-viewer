param($migrationName)
.\import.env.ps1
hasura migrate create $migrationName --from-server --schema test_executions --database-name test-report-viewer --endpoint $env:HASURA_PROJECT_URL --admin-secret $env:HASURA_ADMIN_SECRET
