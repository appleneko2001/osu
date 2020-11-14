using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Screens.Play;
using osu.Game.Users;
using osuTK;

namespace osu.Game.Overlays.Profile.Header.Components
{
    public class SpectatePlayerButton : ProfileHeaderButton
    {
        public readonly Bindable<User> User = new Bindable<User>();

        [Resolved(canBeNull: true)]
        private OsuGame game { get; set; }

        private bool isUserOnline;

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new FillFlowContainer
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Direction = FillDirection.Horizontal,
                Padding = new MarginPadding { Right = 10 },
                Children = new Drawable[]
                {
                    new SpriteIcon
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Icon = FontAwesome.Solid.Eye,
                        FillMode = FillMode.Fit,
                        Size = new Vector2(50, 14)
                    },
                    new OsuSpriteText
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        Text = "觀看"
                    }
                }
            };

            Enabled.BindTarget = new BindableBool(isUserOnline);
            User.BindValueChanged(user => updateStatus(user.NewValue), true);

            Action = () =>
            {
                if (isUserOnline)
                    game?.PerformFromScreen(s => s.Push(new Spectator(User.Value)));
            };
        }
        
        private void updateStatus(User user)
        { 
            isUserOnline = (user != null) ? // If user instance is not null
                user.IsOnline : false; // set by user.IsOnline otherwize false
        } 
    }
}
