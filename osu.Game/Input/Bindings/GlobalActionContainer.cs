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
        [Description("������ѫǹϼh")]
        ToggleChat,

        [Description("�������Ϲϼh")]
        ToggleSocial,

        [Description("���]��J�]�w")]
        ResetInputSettings,

        [Description("�����u����")]
        ToggleToolbar,

        [Description("�����]�w")]
        ToggleSettings,

        [Description("���� osu!direct")]
        ToggleDirect,

        [Description("�W�[���q")]
        IncreaseVolume,

        [Description("��֭��q")]
        DecreaseVolume,

        [Description("�����R��")]
        ToggleMute,

        // In-Game Keybindings
        [Description("���L����")]
        SkipCutscene,

        [Description("�ֳt���s�}�l (����)")]
        QuickRetry,

        [Description("�I��")]
        TakeScreenshot,

        [Description("�����C�����ƹ�����")]
        ToggleGameplayMouseButtons,

        [Description("��^")]
        Back,

        [Description("�W�[�u�ʳt��")]
        IncreaseScrollSpeed,

        [Description("��ֺu�ʳt��")]
        DecreaseScrollSpeed,

        [Description("���")]
        Select,

        [Description("�ֳt�h�X (����)")]
        QuickExit,

        // Game-wide beatmap music controller keybindings
        [Description("�U�@��")]
        MusicNext,

        [Description("�W�@��")]
        MusicPrev,

        [Description("���� / �Ȱ�")]
        MusicPlay,

        [Description("�������b����ϼh")]
        ToggleNowPlaying,

        [Description("��ܤW�@��")]
        SelectPrevious,

        [Description("��ܤU�@��")]
        SelectNext,

        [Description("�D��")]
        Home,

        [Description("�����q��")]
        ToggleNotifications,

        [Description("�C���Ȱ�")]
        PauseGameplay,

        // Editor
        [Description("�]�w�Ҧ�")]
        EditorSetupMode,

        [Description("�@���Ҧ�")]
        EditorComposeMode,

        [Description("�]�p�Ҧ�")]
        EditorDesignMode,

        [Description("�ծɼҦ�")] // [RequestImprove] ���OTiming mode �����D�n��򥿽T½Ķ�N���Google translate�d�F�@�U �i�঳�I���� �ӤH���z�����ӬO�ɶ��b ���O�N�q���I���ӷǽT
        EditorTimingMode,
    }
}
