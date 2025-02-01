# powershell -Command .\environment.ps

[System.Environment]::SetEnvironmentVariable("SSSWare_DB_CONNECTION", "Data Source=DESKTOP-9NUP2PU\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False", [System.EnvironmentVariableTarget]::User)
