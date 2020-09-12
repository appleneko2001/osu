<p align="center">
  <img width="500px" src="assets/lazer.png">
</p>

未來的 [osu!](https://osu.ppy.sh) 的計畫藍圖就在這裏! 目前 衆所周知已知的開發代碼爲 *osu!lazer*.

## 狀態 

該專案處於一個非常艱鉅的開發階段中 但已經在穩定階段 用戶們可以嘗試使用該版本 但要跟正常版 *osu!* 共存. 在本專案完成多次進化後會變成更新替換原本的正常版本 
 
  **重要:** 遊戲規則機制 (以及其他會讓大家知道並喜歡的功能) 在基本恆定狀態. 遊戲的平衡和最終的品質需要等到開發結束  
 主要因爲實驗性質和變更會潛在的**降低可玩性或者實用性**. 作爲開發者和設計家來講會更有效率的前進. 如果這樣有點冒犯到了,
 還請先繼續使用正常版 osu!. 我們不想準備一場火熱的討論關於遊戲機制, 也不會在Github中討論這個話題.

我們 (ppy官方團隊) 接受臭蟲提交 (請儘可能詳細解釋並跟隨現有的 Issue 模板) 同時也接受新的功能建議 但需要理解的是我們對新功能加入的平衡影響非常重要.
可以參考以下資訊了解更多 關於 osu!lazer 專案:
 
- 更詳細的變動日誌 [官方 osu! 頁面](https://osu.ppy.sh/home/changelog/lazer).
- 可以了解更多關於我們對本 [專案的管理 官方頁面](https://github.com/ppy/osu/wiki/Project-management).
- 查看 peppy [部落格po文](https://blog.ppy.sh/a-definitive-lazer-faq/) 探索專案目前的狀態和未來的發展方向.

## 運行 osu!

如果想要不設定開發環境就運行 osu!lazer 可以看一下 [已編譯的發行](https://github.com/ppy/osu/releases). 下面請根據你/妳的作業系統安裝所需版本:

**最新組建:**

| [Windows (x64)](https://github.com/ppy/osu/releases/latest/download/install.exe)  | [macOS 10.12+](https://github.com/ppy/osu/releases/latest/download/osu.app.zip) | [Linux (x64)](https://github.com/ppy/osu/releases/latest/download/osu.AppImage) | [iOS(iOS 10+, 官方版本)](https://osu.ppy.sh/home/testflight) | [Android (5+)](https://github.com/ppy/osu/releases/latest/download/sh.ppy.osulazer.apk)
| ------------- | ------------- | ------------- | ------------- | ------------- |

- 在運行 Windows 7 或者 8.1 作業系統時, **[附加需求](https://docs.microsoft.com/en-us/dotnet/core/install/dependencies?tabs=netcore31&pivots=os-windows)** 可能會被要求.
爲了能夠正確運行 .NET Core 應用程式 請確保作業系統已經使用了最新的服務包 (Service packs) 

如果上述平台表中沒有你/妳需要的 可以按照下面的教程去自己組建它

## 開發或除錯

請確保已滿足下面的幾個要求:

- 至少安裝了桌面版 [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download) 或者最新的.
- 開發行動設備版, [Xamarin](https://docs.microsoft.com/en-us/xamarin/) 是必須的, 應該與 Visual Studio 或者 [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/) 一起安裝.
- 編輯代碼庫的時候, 我們建議使用有代碼高亮或者定位物件定義功能的 IDE, 比如 [Visual Studio 2019+](https://visualstudio.microsoft.com/vs/), [JetBrains Rider](https://www.jetbrains.com/rider/) 或者 [Visual Studio Code](https://code.visualstudio.com/).
- 在Linux運行的時候 請確保作業系統已經安裝了 FFmpeg 用於支援影像解碼

### 下載源代碼

拷貝資料到本地:

```shell
git clone https://github.com/ppy/osu 
cd osu
```

如果要將本地代碼更新到最新的提交, 在 osu 目錄下運行下面的指令:

```shell
git pull
```

### 組建

下面指出的推薦 IDE 已經自身附帶了組建專案功能. 你/妳應使用 IDE 提供的 組建/運行 功能確保不會搞砸一切. 在測試或者組建新的組件時 非常建議使用
`VisualTests` 專案預設 關於更多可以 [瀏覽下方](#貢獻). 

- Visual Studio / Rider 用戶應去使用特定平台的 `.slnf` 檔案去載入專案, 相比主要的 `.sln.` 它會直接載入所有子專案.
- Visual Studio Code 用戶需要在組建之前運行一次 `Restore`.
 
當然也可以使用下面的指令去組建並運行 *osu!* 只需一行搞定:

```shell
dotnet run --project osu.Desktop
```

如果你/妳對 osu! 除錯不感興趣的話 可以在上面的命令加一個 `-c Release` 來進行最佳化. 在這個情況下 應將在該文檔中提到的任何指令中 `Debug` 換成 `Release`  

如果組建失敗 試着復原 NuGet 包去解決 (`dotnet restore`). 

_因爲 .NET Core 和 Xamarin 的歷史特徵差距 有些情況下運行 `dotnet` 可能不會正常運作. 該問題的解決方式可以是指定 `.csproj` 或者使用專案助手在 `build/Desktop.proj`.
該預設的提供已經解決下方所有已支援的 IDE 提到的問題_  

### 測試資源/框架修改

有些時候 交叉測試變更 [osu-resources](https://github.com/ppy/osu-resources) 或者 [osu-framework](https://github.com/ppy/osu-framework) 顯爲非常重要. 
可以使用該文檔 [osu-resources](https://github.com/ppy/osu-resources/wiki/Testing-local-resources-checkout-with-other-projects) 中的指令去執行 以及在 [osu-framework](https://github.com/ppy/osu-framework/wiki/Testing-local-framework-checkout-with-other-projects) 百科頁面中.

### 代碼分析

在提交代碼前 先運行一次代碼格式化. 可以使用 `dotnet format` 在控制台中 或者使用 IDE 提供的格式化代碼功能.

我們 (ppy官方團隊) 使用了一些有跨平台, 編譯器整合的分析器. 在編輯的時候他們可以提供警告和建議, 甚至在 IDE 或者 控制台 中組建的時候也會, 如果他們是編譯器本身提供的. 

JetBrains ReSharper InspectCode 也提供了更廣泛的分析功能和規則. 可以在 PowerShell 中運行 `.\InspectCode.ps1`,
僅限 [Windows 支援](https://youtrack.jetbrains.com/issue/RSRP-410004). 另類的方案是可以安裝 ReSharper
或者使用 Rider 獲得編輯器內的代碼分析建議.

## 貢獻

如果想要提供一些貢獻到該專案, 可以用提交 issues 的方式或者提交 pull 請求. 按照過去的經驗, 我們準備了 [貢獻指導 (暫未翻譯)](CONTRIBUTING.md) 來幫助你/妳與我們 (ppy官方團隊) 的合作過程並回答近期經常被詢問的問題 

由於我們 (ppy官方團隊) 已經定義了太多的標準在這裏, 沒有什麼東西是一成不變的. 如果有關於我們 (ppy官方團隊) 提供的代碼庫的代碼結構化的問題, 或者在貢獻過程中的任何問題 *請提出*他們. 我們 (ppy官方團隊) 接受所有的反饋 這樣在貢獻過程中減少更多的麻煩.

給那些對本專案感興趣並提供了很多的貢獻的用戶, 我們 (ppy官方團隊) 會提供
[賞金](https://docs.google.com/spreadsheets/d/1jNXfj_S3Pb5PErA-czDdC9DUu4IgUbe1Lt8E7CYUJuE/view?&rm=minimal#gid=523803337), 
透過 PayPal 或者 osu!supporter 標籤. 不要猶豫 [請求賞金](https://docs.google.com/forms/d/e/1FAIpQLSet_8iFAgPMG526pBZ2Kic6HSh7XPM3fE8xPcnWNkMzINDdYg/viewform) 爲你/妳對專案獻出的工作和努力.

## 協議聲明

*osu!* 的框架和代碼透過 [MIT 協議](https://opensource.org/licenses/MIT) 發行. 關於更多請閱覽 [協議檔案](LICENCE). 
[tl;dr](https://tldrlegal.com/license/mit-license) 只要在軟體/源代碼的任何副本中包含原始版權和許可聲明, 就可以做任何你/妳想做的事情.

注意! 這 *不包含* 使用 "osu!" 或者 "ppy" 商標在任何軟體, 資源, 推廣或者盈利, 因爲他們受商標法律保護.

另外 本遊戲的資源受單獨的協議保護. 請閱覽 [ppy/osu-resources](https://github.com/ppy/osu-resources) 用於正確理解.
