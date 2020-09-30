// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osu.Framework.Utils;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Online.API;
using osu.Game.Users;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Menu
{
    public class Disclaimer : StartupScreen
    {
        private SpriteIcon icon;
        private Color4 iconColour;
        private LinkFlowContainer textFlow;
        private LinkFlowContainer supportFlow;

        private Drawable heart;

        private const float icon_y = -85;
        private const float icon_size = 30;

        private readonly OsuScreen nextScreen;

        private readonly Bindable<User> currentUser = new Bindable<User>();
        private FillFlowContainer fill;

        public Disclaimer(OsuScreen nextScreen = null)
        {
            this.nextScreen = nextScreen;
            ValidForResume = false;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours, IAPIProvider api)
        {
            InternalChildren = new Drawable[]
            {
                icon = new SpriteIcon
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Icon = FontAwesome.Solid.Flask,
                    Size = new Vector2(icon_size),
                    Y = icon_y,
                },
                fill = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                    Y = icon_y,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.TopCentre,
                    Children = new Drawable[]
                    {
                        textFlow = new LinkFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            TextAnchor = Anchor.TopCentre,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Spacing = new Vector2(0, 2),
                            LayoutDuration = 2000,
                            LayoutEasing = Easing.OutQuint
                        },
                        supportFlow = new LinkFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            TextAnchor = Anchor.TopCentre,
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Alpha = 0,
                            Spacing = new Vector2(0, 2),
                        },
                    }
                }
            };

            textFlow.AddText("該項目仍在 ", t => t.Font = t.Font.With(Typeface.Torus, 30, FontWeight.Light));
            textFlow.AddText("完成中", t => t.Font = t.Font.With(Typeface.Torus, 30, FontWeight.SemiBold));

            textFlow.NewParagraph();

            static void format(SpriteText t) => t.Font = OsuFont.GetFont(size: 15, weight: FontWeight.SemiBold);

            textFlow.AddParagraph(getRandomTip(), t => t.Font = t.Font.With(Typeface.Torus, 20, FontWeight.SemiBold));
            textFlow.NewParagraph();

            textFlow.NewParagraph();

            iconColour = colours.Yellow;

            currentUser.BindTo(api.LocalUser);
            currentUser.BindValueChanged(e =>
            {
                supportFlow.Children.ForEach(d => d.FadeOut().Expire());

                if (e.NewValue.IsSupporter)
                {
                    supportFlow.AddText("我們永遠不會忘記你/妳對 osu! 的支持", format);
                }
                else
                {
                    supportFlow.AddText("成爲 ", format);
                    supportFlow.AddLink("osu!supporter", "https://osu.ppy.sh/home/support", creationParameters: format);
                    supportFlow.AddText(" 幫助支持這個遊戲", format);
                }

                heart = supportFlow.AddIcon(FontAwesome.Solid.Heart, t =>
                {
                    t.Padding = new MarginPadding { Left = 5, Top = 3 };
                    t.Font = t.Font.With(size: 12);
                    t.Origin = Anchor.Centre;
                    t.Colour = colours.Pink;
                }).First();

                if (IsLoaded)
                    animateHeart();

                if (supportFlow.IsPresent)
                    supportFlow.FadeInFromZero(500);
            }, true);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            if (nextScreen != null)
                LoadComponentAsync(nextScreen);
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);

            icon.RotateTo(10);
            icon.FadeOut();
            icon.ScaleTo(0.5f);

            icon.Delay(500).FadeIn(500).ScaleTo(1, 500, Easing.OutQuint);

            using (BeginDelayedSequence(3000, true))
            {
                icon.FadeColour(iconColour, 200, Easing.OutQuint);
                icon.MoveToY(icon_y * 1.3f, 500, Easing.OutCirc)
                    .RotateTo(-360, 520, Easing.OutQuint)
                    .Then()
                    .MoveToY(icon_y, 160, Easing.InQuart)
                    .FadeColour(Color4.White, 160);

                fill.Delay(520 + 160).MoveToOffset(new Vector2(0, 15), 160, Easing.OutQuart);
            }

            supportFlow.FadeOut().Delay(2000).FadeIn(500);
            double delay = 500;
            foreach (var c in textFlow.Children)
                c.FadeTo(0.001f).Delay(delay += 20).FadeIn(500);

            animateHeart();

            this
                .FadeInFromZero(500)
                .Then(5500)
                .FadeOut(250)
                .ScaleTo(0.9f, 250, Easing.InQuint)
                .Finally(d =>
                {
                    if (nextScreen != null)
                        this.Push(nextScreen);
                });
        }

        private string getRandomTip()
        {
            string[] tips =
            {
                "可以按 Ctrl-T 在遊戲中的任意位置切換工具欄!",
                "可以按 Ctrl-O 在遊戲中的任意位置去變動遊戲設定!",
                "所有設定都會實時動態變更. 試着在遊戲中選擇別的皮膚!",
                "每次更新都會加入一些新的東西, 記得保持關注!",
                "如果覺得界面太小了, 試着將 \"界面縮放\" 設定在合適的範圍!",
                "試着調整 \"畫面縮放\" 模式去變動你/妳的遊戲中和界面縮放, 甚至在全螢幕模式中!",
                "osu!direct 已開放給所有 osu!lazer 玩家! 需要的時候在任意地方按 Ctrl-D!",
                "在回放中的進度條 (畫面下方) 可被拖動!",
                "多執行緒會在 \"FPS\" 過低的時候保持輸入控制不會被影響!",
                "試着在 Mods 面板中往下翻, 會有很好玩的東西!",
                "大部分線上頁面 (個人資訊, 排名或者其他) 可以在遊戲中直接顯示 (非網頁瀏覽器顯示!)",
                "試着對圖譜右鍵進行更多操作 比如瀏覽圖譜資訊, 編輯圖譜, 收藏圖譜, 遊玩圖譜以及隱藏!",
                "所有的刪除圖譜操作在遊戲關閉之前都是暫時的 如果改變主意了可以回到維護選項中復原他們!",
                "查看 \"時移 (Timeshift)\" 多人遊戲系統，該系統具有本地永久排行榜和播放列表支持!", // [RequestImprove] 不知道怎麼翻譯 用了Google translate 不要打我
                "切換效能資訊顯示可以按 Ctrl-F11!",
                "如果想要知道更詳細的日誌 可以按 Ctrl-F2 在遊戲中顯示他們!",
                "中文化由 github@appleneko2001 提供! 記得支持一下 osu! 遊戲很棒 不是支持我!",
            };

            return tips[RNG.Next(0, tips.Length)];
        }

        private void animateHeart()
        {
            heart.FlashColour(Color4.White, 750, Easing.OutQuint).Loop();
        }
    }
}
