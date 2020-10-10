# osu! 非官方中文化翻譯
  
免費 開源的節奏遊戲 節奏只需*點擊*即可! 

*非官方中文化由我一人完成 有的時候還需要感謝 Google翻譯 如果想要幫助我翻譯的話歡迎提交 issue 並告訴我. 在一些翻譯不太好的地方我都在註釋後面加了 [RequestImprove] 作爲標記 (我才注意到Forked Repository不能找代碼內容 可能要找個機會列出在這裏了).*

## 關於翻譯進度 

目前進度是 就只有圖譜編輯器 部分通知什麼的沒完成了. 關於圖譜編輯器的話 我等它的完成度到一個程度的時候再翻譯, 通知的話 其實我懶xD (開玩笑).

## 已知問題:
* 不能玩多人, 需要使用官方版本的遊戲 
* 版本更新會比官方慢一些 可能至少一天過後才能有新的中文化更新內容
* 要求更多的硬碟空間 (因爲安裝這個之後相當於安裝了兩個 osulazer)

## 如何安裝:
* 需要 Windows x64 OS (Linux環境下也可以 不過要啟動的話需要使用終端執行 dotnet osu.dll)
* 需要 [.NET Core 3.1 運行環境](https://dotnet.microsoft.com/download)
* 當前專案只支援x64架構OS 對應的也需要處理器支援 amd64 不然無法執行 (雖然x86可以執行 但是問題會很多 而且運行效率沒有x64更好)

*提示: 由於我的計算機上的 Linux 不小心被我弄掉了 所以短時間內不會有AppImage 但是可以下載壓縮檔解包 然後使用dotnet osu.dll指令執行 效果差不多一樣 只是稍微麻煩一點而已*
在以上要求滿足後 請選擇下面的下載選擇. 另外 安裝包目前只可以將應用安裝到 C:\Users\<用戶名>\AppData\Local\osulazer-zh-tw 中. 關於更多可以閱覽該 [Issue](https://github.com/Squirrel/Squirrel.Windows/issues/1002) 我已經將該Repository進行了fork 我可能會自己改動Squirrel安裝器 嘗試實現讓用戶選擇安裝位置. 當然如果不行的話我會把那個劃掉再說明原因...

| ![下載Windows安裝包](https://img.shields.io/github/downloads/appleneko2001/osu/latest/install.exe?color=blue&label=%E4%B8%8B%E8%BC%89Windows%E5%AE%89%E8%A3%9D%E5%8C%85&logo=windows&logoColor=lightblue) | ![下載壓縮檔](https://img.shields.io/github/downloads/appleneko2001/osu/latest/osulazer-zh-tw-win-x64.zip?color=blue&label=%E4%B8%8B%E8%BC%89%E5%A3%93%E7%B8%AE%E6%AA%94) |
| ------------- | ------------- |

下載完安裝包後 按兩次執行安裝 過後osulazer將會自己啟動 如果需要解除安裝的話 可以用一般方式進行解除安裝 (還能是什麼 控制台裏面的那個程式與功能啦 Windows 10的話是可以透過設定裏的系統設定 差不多一樣 只是界面有點區別而已).
當然 如果下載的是壓縮檔 只需要將裏面的所有內容物解包到任意檔案夾中 需要啟動的話只需要按兩次 osu!.exe 執行檔. 當然 解除安裝也比較簡單 對準那個檔案夾按住 Shift+Del 就好了.

## 關於提交翻譯 issues

如果有任何想要提交的翻譯建議和糾錯 issues 可以提交到 [這個Repository](https://github.com/appleneko2001/osu-zhtw-translate-issues).

如果是遊戲本身的問題, 我更建議去官方 repository, 提交任何遊戲本身問題和建議的issues 並且儘可能跟隨官方issues模板 語言只用英語.

# 官方Readme內容大致翻譯:
爲了確保版面整潔度, 以及確保這個Repository討論的東西都於中文化有關, 我把官方Readme頁面的個人大致翻譯分離到另一個檔案. 如果有需要查看的話可以[點擊這裏](README_zh-tw.md)

*內容中的 "我們" 是指ppy官方團隊, 避免大家誤會我是ppy官方團隊的一員. 我提供中文化只是爲了做我喜歡做的事情 _僅此而已_*
