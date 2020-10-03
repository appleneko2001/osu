// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Framework.Input.Bindings;

namespace osu.Game.Input.Bindings
{
    public class GlobalActionContainer : DatabasedKeyBindingContainer<GlobalAction>, IHandleGlobalKeyboardInput
    {
        private readonly Drawable handler;

        public GlobalActionContainer(OsuGameBase game)
            : base(matchingMode: KeyCombinationMatchingMode.Modifiers)
        {
            if (game is IKeyBindingHandler<GlobalAction>)
                handler = game;
        }

        public override IEnumerable<KeyBinding> DefaultKeyBindings => GlobalKeyBindings.Concat(InGameKeyBindings).Concat(AudioControlKeyBindings).Concat(EditorKeyBindings);

        public IEnumerable<KeyBinding> GlobalKeyBindings => new[]
        {
            new KeyBinding(InputKey.F6, GlobalAction.ToggleNowPlaying),
            new KeyBinding(InputKey.F8, GlobalAction.ToggleChat),
            new KeyBinding(InputKey.F9, GlobalAction.ToggleSocial),
            new KeyBinding(InputKey.F10, GlobalAction.ToggleGameplayMouseButtons),
            new KeyBinding(InputKey.F12, GlobalAction.TakeScreenshot),

            new KeyBinding(new[] { InputKey.Control, InputKey.Alt, InputKey.R }, GlobalAction.ResetInputSettings),
            new KeyBinding(new[] { InputKey.Control, InputKey.T }, GlobalAction.ToggleToolbar),
            new KeyBinding(new[] { InputKey.Control, InputKey.O }, GlobalAction.ToggleSettings),
            new KeyBinding(new[] { InputKey.Control, InputKey.D }, GlobalAction.ToggleDirect),
            new KeyBinding(new[] { InputKey.Control, InputKey.N }, GlobalAction.ToggleNotifications),

            new KeyBinding(InputKey.Escape, GlobalAction.Back),
            new KeyBinding(InputKey.ExtraMouseButton1, GlobalAction.Back),

            new KeyBinding(new[] { InputKey.Alt, InputKey.Home }, GlobalAction.Home),

            new KeyBinding(InputKey.Up, GlobalAction.SelectPrevious),
            new KeyBinding(InputKey.Down, GlobalAction.SelectNext),

            new KeyBinding(InputKey.Space, GlobalAction.Select),
            new KeyBinding(InputKey.Enter, GlobalAction.Select),
            new KeyBinding(InputKey.KeypadEnter, GlobalAction.Select),
        };

        public IEnumerable<KeyBinding> EditorKeyBindings => new[]
        {
            new KeyBinding(new[] { InputKey.F1 }, GlobalAction.EditorComposeMode),
            new KeyBinding(new[] { InputKey.F2 }, GlobalAction.EditorDesignMode),
            new KeyBinding(new[] { InputKey.F3 }, GlobalAction.EditorTimingMode),
            new KeyBinding(new[] { InputKey.F4 }, GlobalAction.EditorSetupMode),
        };

        public IEnumerable<KeyBinding> InGameKeyBindings => new[]
        {
            new KeyBinding(InputKey.Space, GlobalAction.SkipCutscene),
            new KeyBinding(InputKey.ExtraMouseButton2, GlobalAction.SkipCutscene),
            new KeyBinding(InputKey.Tilde, GlobalAction.QuickRetry),
            new KeyBinding(new[] { InputKey.Control, InputKey.Tilde }, GlobalAction.QuickExit),
            new KeyBinding(new[] { InputKey.Control, InputKey.Plus }, GlobalAction.IncreaseScrollSpeed),
            new KeyBinding(new[] { InputKey.Control, InputKey.Minus }, GlobalAction.DecreaseScrollSpeed),
            new KeyBinding(InputKey.MouseMiddle, GlobalAction.PauseGameplay),
        };

        public IEnumerable<KeyBinding> AudioControlKeyBindings => new[]
        {
            new KeyBinding(new[] { InputKey.Alt, InputKey.Up }, GlobalAction.IncreaseVolume),
            new KeyBinding(new[] { InputKey.Alt, InputKey.MouseWheelUp }, GlobalAction.IncreaseVolume),
            new KeyBinding(new[] { InputKey.Alt, InputKey.Down }, GlobalAction.DecreaseVolume),
            new KeyBinding(new[] { InputKey.Alt, InputKey.MouseWheelDown }, GlobalAction.DecreaseVolume),

            new KeyBinding(new[] { InputKey.Control, InputKey.F4 }, GlobalAction.ToggleMute),

            new KeyBinding(InputKey.TrackPrevious, GlobalAction.MusicPrev),
            new KeyBinding(InputKey.F1, GlobalAction.MusicPrev),
            new KeyBinding(InputKey.TrackNext, GlobalAction.MusicNext),
            new KeyBinding(InputKey.F5, GlobalAction.MusicNext),
            new KeyBinding(InputKey.PlayPause, GlobalAction.MusicPlay),
            new KeyBinding(InputKey.F3, GlobalAction.MusicPlay)
        };

        protected override IEnumerable<Drawable> KeyBindingInputQueue =>
            handler == null ? base.KeyBindingInputQueue : base.KeyBindingInputQueue.Prepend(handler);
    }

    public enum GlobalAction
    {
        [Description("切換聊天室圖層")]
        ToggleChat,

        [Description("切換社區圖層")]
        ToggleSocial,

        [Description("重設輸入設定")]
        ResetInputSettings,

        [Description("切換工具欄")]
        ToggleToolbar,

        [Description("切換設定")]
        ToggleSettings,

        [Description("切換 osu!direct")]
        ToggleDirect,

        [Description("增加音量")]
        IncreaseVolume,

        [Description("減少音量")]
        DecreaseVolume,

        [Description("切換靜音")]
        ToggleMute,

        // In-Game Keybindings
        [Description("略過片場")]
        SkipCutscene,

        [Description("快速重新開始 (按住)")]
        QuickRetry,

        [Description("截圖")]
        TakeScreenshot,

        [Description("切換遊戲中滑鼠按鍵")]
        ToggleGameplayMouseButtons,

        [Description("返回")]
        Back,

        [Description("增加滾動速度")]
        IncreaseScrollSpeed,

        [Description("減少滾動速度")]
        DecreaseScrollSpeed,

        [Description("選擇")]
        Select,

        [Description("快速退出 (按住)")]
        QuickExit,

        // Game-wide beatmap music controller keybindings
        [Description("下一首")]
        MusicNext,

        [Description("上一首")]
        MusicPrev,

        [Description("播放 / 暫停")]
        MusicPlay,

        [Description("切換正在播放圖層")]
        ToggleNowPlaying,

        [Description("選擇上一個")]
        SelectPrevious,

        [Description("選擇下一個")]
        SelectNext,

        [Description("主頁")]
        Home,

        [Description("切換通知")]
        ToggleNotifications,

        [Description("遊戲暫停")]
        PauseGameplay,

        // Editor
        [Description("設定模式")]
        EditorSetupMode,

        [Description("作曲模式")]
        EditorComposeMode,

        [Description("設計模式")]
        EditorDesignMode,

        [Description("校時模式")] // [RequestImprove] 原文是Timing mode 不知道要怎麼正確翻譯就丟到Google translate查了一下 可能有點不對 個人的理解應該是時間軸 但是意義有點不太準確
        EditorTimingMode,
    }
}
