namespace osu.Game.Localisation
{
    /// <summary>
    /// Still preparing, do not use it.
    /// </summary>
    public class LocalisedStringDictionary
    {

    }

    /// <summary>
    /// Provides traditional chinese translated texts.
    /// </summary>
    public static class TaiwanLocalisedStringDicts
    {
        /// <summary>
        /// Patch for translate some osu!framework strings.
        /// </summary>
        public static readonly StringResourceStore FRAMEWORK_PATCH = new StringResourceStore(
                // Basic text translation.
                new string[2] { "FRAME LIMITER", "幀數限制" },
                new string[2] { "RAW INPUT", "原生輸入" },
                new string[2] { "SCREEN MODE", "畫面模式" },
                new string[2] { "SCREEN RESOLUTION", "畫面解析度" },
                new string[2] { "AUDIO DEVICE", "音訊輸出設備" },
                new string[2] { "CURSOR SENSITIVITY", "指針靈敏度" },
                new string[2] { "Default", "默認設定" },
                new string[2] { "VSync", "垂直同步" },
                new string[2] { "2x refresh rate", "雙倍更新速率" },
                new string[2] { "4x refresh rate", "四倍更新速率" },
                new string[2] { "8x refresh rate", "八倍更新速率" },
                new string[2] { "Unlimited", "關閉" },
                new string[2] { "Multithreaded", "多線緒處理" },
                new string[2] { "Single thread", "單處理緒" },
                new string[2] { "Fullscreen", "全熒幕" },
                new string[2] { "Always", "總是" },
                new string[2] { "Never", "永不" },
                new string[2] { "Borderless", "無邊框窗體" },
                new string[2] { "Windowed", "窗體化" },
                new string[2] { "enabled", "已啟用" },
                new string[2] { "disabled", "已關閉" }

                // Translate for frame counter
                // Actually they cannot be injected to framework so I use comments to disable them.
                //new string[2] { "Input", "輸入" },
                //new string[2] { "Audio", "音訊" },
                //new string[2] { "Update", "邏輯處理" },
                //new string[2] { "Draw", "圖像" },

                // Error notification translate
                // And this one cannot be injected too cuz it uses TextFlowContainer to rendering text, they separated a lot parts so our injector cannot work. 
                //new string[2] { "Texture could not be loaded via STB; falling back to ImageSharp.", "無法使用STB載入圖像, 將使用ImageSharp處理." }
                );
        /// <summary>
        ///  It doesn't work lol 
        /// </summary>
        public static readonly StringResourceStore GAME_PATCH = new StringResourceStore(
            new string[2] { "No scores have been set yet. Maybe you can be the first!", "暫時還沒有成績. 也許你可以成爲第一個!" },
            new string[2] { "None of your friends have set a score on this map yet.", "你的朋友們好像還沒有在這張圖上計入成績." },
            new string[2] { "No one from your country has set a score on this map yet.", "在你的國家中還沒有一個人在這張圖上計入成績." },

            new string[2] { "Unranked beatmap", "未上榜的圖譜" },

            new string[2] { "load replies", "顯示回覆" },
            new string[2] { "show more", "顯示更多" },
            new string[2] { "deleted", "已刪除" },

            new string[2] { "Your music volume is set to 0%! Click here to restore it.", "音樂音量設定爲 0%! 點擊以復原設定." },
            new string[2] { "This link type is not yet supported!", "該鏈接類型還沒有被支援!" }

            );
    }
}
