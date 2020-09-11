// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Game.Users;

namespace osu.Game.Overlays.Profile.Header.Components
{
    public class OverlinedTotalPlayTime : CompositeDrawable, IHasTooltip
    {
        public readonly Bindable<User> User = new Bindable<User>();

        public string TooltipText { get; set; }

        private OverlinedInfoContainer info;

        public OverlinedTotalPlayTime()
        {
            AutoSizeAxes = Axes.Both;

            TooltipText = "0 �Ӥp��";
        }

        [BackgroundDependencyLoader]
        private void load(OverlayColourProvider colourProvider)
        {
            InternalChild = info = new OverlinedInfoContainer
            {
                Title = "�`�C���ɶ�",
                LineColour = colourProvider.Highlight1,
            };

            User.BindValueChanged(updateTime, true);
        }

        private void updateTime(ValueChangedEvent<User> user)
        {
            TooltipText = (user.NewValue?.Statistics?.PlayTime ?? 0) / 3600 + " �Ӥp��";
            info.Content = formatTime(user.NewValue?.Statistics?.PlayTime);
        }

        private string formatTime(int? secondsNull)
        {
            if (secondsNull == null) return "0����";

            int seconds = secondsNull.Value;
            string time = "";

            int days = seconds / 86400;
            seconds -= days * 86400;
            if (days > 0)
                time += days + "�� ";

            int hours = seconds / 3600;
            seconds -= hours * 3600;
            time += hours + "�p�� ";

            int minutes = seconds / 60;
            time += minutes + "����";

            return time;
        }
    }
}
