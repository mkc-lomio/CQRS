$username = "`$myfirstwebapp-deployment"
$password = "8GK2goolZtx7dwp5EZwqw9DrFLoWnrcf6o6ysFllm4FBgttC9xuwtlrmpAZi"

$base64AuthInfo = [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(("{0}:{1}" -f $username, $password)))
$userAgent = "powershell/1.0"

$apiUrl = "https://myfirstwebapp-deployment.scm.azurewebsites.net:443/api/zip/site/wwwroot/"
$filePath = "wwwroot.zip"
Invoke-RestMethod -Uri $apiUrl -Headers @{Authorization=("Basic {0}" -f $base64AuthInfo)} -UserAgent $userAgent -Method GET -OutFile $filePath -ContentType "multipart/form-data"