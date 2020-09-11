// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Utils;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics.UserInterface;
using osu.Game.Online.API;
using osu.Game.Overlays.Settings;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Overlays.AccountCreation
{
    public class ScreenEntry : AccountCreationScreen
    {
        private ErrorTextFlowContainer usernameDescription;
        private ErrorTextFlowContainer emailAddressDescription;
        private ErrorTextFlowContainer passwordDescription;

        private OsuTextBox usernameTextBox;
        private OsuTextBox emailTextBox;
        private OsuPasswordTextBox passwordTextBox;

        [Resolved]
        private IAPIProvider api { get; set; }

        private ShakeContainer registerShake;
        private IEnumerable<Drawable> characterCheckText;

        private OsuTextBox[] textboxes;
        private LoadingLayer loadingLayer;

        [Resolved]
        private GameHost host { get; set; }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            FillFlowContainer mainContent;

            InternalChildren = new Drawable[]
            {
                mainContent = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Padding = new MarginPadding(20),
                    Spacing = new Vector2(0, 10),
                    Children = new Drawable[]
                    {
                        new OsuSpriteText
                        {
                            Margin = new MarginPadding { Vertical = 10 },
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Font = OsuFont.GetFont(size: 20),
                            Text = "創建 osu! 帳號!",
                        },
                        usernameTextBox = new OsuTextBox
                        {
                            PlaceholderText = "暱稱",
                            RelativeSizeAxes = Axes.X,
                            TabbableContentContainer = this
                        },
                        usernameDescription = new ErrorTextFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y
                        },
                        emailTextBox = new OsuTextBox
                        {
                            PlaceholderText = "E-mail地址",
                            RelativeSizeAxes = Axes.X,
                            TabbableContentContainer = this
                        },
                        emailAddressDescription = new ErrorTextFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y
                        },
                        passwordTextBox = new OsuPasswordTextBox
                        {
                            PlaceholderText = "密碼",
                            RelativeSizeAxes = Axes.X,
                            TabbableContentContainer = this,
                        },
                        passwordDescription = new ErrorTextFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y
                        },
                        new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Children = new Drawable[]
                            {
                                registerShake = new ShakeContainer
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Child = new SettingsButton
                                    {
                                        Text = "註冊!",
                                        Margin = new MarginPadding { Vertical = 20 },
                                        Action = performRegistration
                                    }
                                }
                            }
                        },
                    },
                },
                loadingLayer = new LoadingLayer(mainContent)
            };

            textboxes = new[] { usernameTextBox, emailTextBox, passwordTextBox };

            usernameDescription.AddText("暱稱將成爲你/妳的公開名字 不要包含褻瀆文字或者僞裝他人的暱稱 當然也不要把你/妳的個人資訊公開啦");
            //This will be your public presence. No profanity, no impersonation. Avoid exposing your own personal details, too!
            emailAddressDescription.AddText("將用於獲取通知, 帳號驗證和重設密碼. 我們承諾不會發送垃圾信封到這個E-mail.");
            emailAddressDescription.AddText(" 確定這裏是對的!", cp => cp.Font = cp.Font.With(Typeface.Torus, weight: FontWeight.Bold));

            passwordDescription.AddText("至少 ");
            characterCheckText = passwordDescription.AddText("有 8 個字長");
            passwordDescription.AddText(". 選擇一個不錯的 不會忘記的密碼 比如一首你/妳最喜歡的曲名.");

            passwordTextBox.Current.ValueChanged += password => { characterCheckText.ForEach(s => s.Colour = password.NewValue.Length == 0 ? Color4.White : Interpolation.ValueAt(password.NewValue.Length, Color4.OrangeRed, Color4.YellowGreen, 0, 8, Easing.In)); };
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            loadingLayer.Hide();

            if (host?.OnScreenKeyboardOverlapsGameWindow != true)
                focusNextTextbox();
        }

        private void performRegistration()
        {
            if (focusNextTextbox())
            {
                registerShake.Shake();
                return;
            }

            usernameDescription.ClearErrors();
            emailAddressDescription.ClearErrors();
            passwordDescription.ClearErrors();

            loadingLayer.Show();

            Task.Run(() =>
            {
                bool success;
                RegistrationRequest.RegistrationRequestErrors errors = null;

                try
                {
                    errors = api.CreateAccount(emailTextBox.Text, usernameTextBox.Text, passwordTextBox.Text);
                    success = errors == null;
                }
                catch (Exception)
                {
                    success = false;
                }

                Schedule(() =>
                {
                    if (!success)
                    {
                        if (errors != null)
                        {
                            usernameDescription.AddErrors(errors.User.Username);
                            emailAddressDescription.AddErrors(errors.User.Email);
                            passwordDescription.AddErrors(errors.User.Password);
                        }
                        else
                        {
                            passwordDescription.AddErrors(new[] { "有些事情發生了... 但我們不確定是什麼." });
                        }

                        registerShake.Shake();
                        loadingLayer.Hide();
                        return;
                    }

                    api.Login(usernameTextBox.Text, passwordTextBox.Text);
                });
            });
        }

        private bool focusNextTextbox()
        {
            var nextTextbox = nextUnfilledTextbox();

            if (nextTextbox != null)
            {
                Schedule(() => GetContainingInputManager().ChangeFocus(nextTextbox));
                return true;
            }

            return false;
        }

        private OsuTextBox nextUnfilledTextbox() => textboxes.FirstOrDefault(t => string.IsNullOrEmpty(t.Text));
    }
}
