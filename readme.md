*![](https://img.shields.io/badge/SGS-OAD-orange) 
![](https://img.shields.io/badge/proj-WindowsService-purple) 
![](https://img.shields.io/badge/-9-512BD4?logo=dotnet)
![](https://img.shields.io/badge/Git-555?logo=git)
![](https://img.shields.io/badge/gitignore-204ECF?logo=gitignore.io)
![](https://img.shields.io/badge/GitHub-555?logo=github)
![](https://img.shields.io/badge/Gitea-555?logo=gitea)
![](https://img.shields.io/badge/Sourcetree-0052CC?logo=sourcetree)
![](https://img.shields.io/badge/OpenAI-412991?logo=openai) 
![](https://img.shields.io/badge/GitHub_Copilot-555?logo=githubcopilot)
![](https://img.shields.io/badge/Anthropic-191919?logo=anthropic)
![](https://img.shields.io/badge/draw.io-555?logo=diagrams.net)
![](https://img.shields.io/badge/Markdown-555?logo=markdown)
![](https://img.shields.io/badge/Mermaid-555?logo=mermaid)
![](https://img.shields.io/badge/Shields.io-555?logo=shieldsdotio)

# 🎯demoWindowsService

>[!NOTE]
>示範如何建立 Windows Service，包含導入 Serilog 與介接 Seq

# ⚡Quick Sample

```sh
dotnet new worker -n demoWindowsService
cd demoWindowsService
dotnet watch run #直接 run 起來測試
```

# ✨Features

- 改用 `HostApplicationBuilder`
- 參數盡可能使用設定檔 `appsettings.json`
- 設定檔依照環境拆開為不同檔案
- 導入 `Serilog` 與 `Seq`
- 注入 `ILogger` 與 `IConfiguration`

# 🛠️Run as Service

```cs
// 設定為 Windows Service
builder.Services.AddWindowsService();
// 添加 Worker 服務
builder.Services.AddHostedService<Worker>();
```

>[!IMPORTANT]
>服務預設啟動為主控台，必須加入上述設定才會成為背景服務

# 📦Publish

```sh
dotnet build
dotnet publish -c Release -r win-x64 --self-contained true -o ./publish
```

>[!NOTE]
>`self-containe` 雖然檔案會變多(變大)，但不依賴機器 SDK，未來問題會少一點

# 🚀Deployment

```sh
# 新增服務
sc create {MyService} binPath= "path-to-your-service.exe"
# 撰寫註解
sc description {MyService} "descrip your service"
# 啟動服務
sc start {MyService}
# 設定為自動啟動
sc config {MyService} start= auto
# 停止服務
sc stop {MyService}
# 刪除服務
sc delete {MyService}
# 檢視狀態
sc query {MyService}
```

- `{MyService}` 請替換為服務名稱
- ⚠️除了最後 **檢視狀態** 以外，所有操作均需要 **管理者權限**