// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Game.Input.Bindings;
using osu.Game.Overlays.Settings;

namespace osu.Game.Overlays.KeyBinding
{
    public class GlobalKeyBindingsSection : SettingsSection
    {
        public override Drawable CreateIcon() => new SpriteIcon
        {
            Icon = FontAwesome.Solid.Globe
        };

        public override string Header => "Global";

        public GlobalKeyBindingsSection(GlobalActionContainer manager)
        {
            Add(new DefaultBindingsSubsection(manager));
            Add(new AudioControlKeyBindingsSubsection(manager));
            Add(new InGameKeyBindingsSubsection(manager));
            Add(new EditorKeyBindingsSubsection(manager));
        }

        private class DefaultBindingsSubsection : KeyBindingsSubsection
        {
            protected override string Header => string.Empty;

            public DefaultBindingsSubsection(GlobalActionContainer manager)
                : base(null)
            {
                Defaults = manager.GlobalKeyBindings;
            }
        }

        private class InGameKeyBindingsSubsection : KeyBindingsSubsection
        {
            protected override string Header => "遊戲中";

            public InGameKeyBindingsSubsection(GlobalActionContainer manager)
                : base(null)
            {
                Defaults = manager.InGameKeyBindings;
            }
        }

        private class AudioControlKeyBindingsSubsection : KeyBindingsSubsection
        {
            protected override string Header => "音訊";

            public AudioControlKeyBindingsSubsection(GlobalActionContainer manager)
                : base(null)
            {
                Defaults = manager.AudioControlKeyBindings;
            }
        }

        private class EditorKeyBindingsSubsection : KeyBindingsSubsection
        {
            protected override string Header => "做圖";

            public EditorKeyBindingsSubsection(GlobalActionContainer manager)
                : base(null)
            {
                Defaults = manager.EditorKeyBindings;
            }
        }
    }
}
