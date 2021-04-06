SELECT "ServiceName", "MethodName", "ExecutionTime", "Exception"
FROM public."AbpAuditLogs" 
WHERE "Exception" IS NOT NULL AND "ServiceName" = 'Legalpedia.Searches.TeamAppService'
ORDER BY "ExecutionTime" DESC
    LIMIT 100