// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Game.Graphics.UserInterface;
using osuTK.Input;

namespace osu.Game.Tests.Visual.UserInterface
{
    public class TestSceneOsuMenu : OsuManualInputManagerTestScene
    {
        public override IReadOnlyList<Type> RequiredTypes => new[]
        {
            typeof(OsuMenu),
            typeof(DrawableOsuMenuItem)
        };

        private OsuMenu menu;
        private bool actionPeformed;

        [SetUp]
        public void Setup() => Schedule(() =>
        {
            actionPeformed = false;

            Child = menu = new OsuMenu(Direction.Vertical, true)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Items = new[]
                {
                    new OsuMenuItem("standard", MenuItemType.Standard, performAction),
                    new OsuMenuItem("highlighted", MenuItemType.Highlighted, performAction),
                    new OsuMenuItem("destructive", MenuItemType.Destructive, performAction),
                }
            };
        });

        [Test]
        public void TestClickEnabledMenuItem()
        {
            AddStep("move to first menu item", () => InputManager.MoveMouseTo(menu.ChildrenOfType<DrawableOsuMenuItem>().First()));
            AddStep("click", () => InputManager.Click(MouseButton.Left));

            AddAssert("action performed", () => actionPeformed);
        }

        [Test]
        public void TestDisableMenuItemsAndClick()
        {
            AddStep("disable menu items", () =>
            {
                foreach (var item in menu.Items)
                    ((OsuMenuItem)item).Enabled.Value = false;
            });

            AddStep("move to first menu item", () => InputManager.MoveMouseTo(menu.ChildrenOfType<DrawableOsuMenuItem>().First()));
            AddStep("click", () => InputManager.Click(MouseButton.Left));

            AddAssert("action not performed", () => !actionPeformed);
        }

        [Test]
        public void TestEnableMenuItemsAndClick()
        {
            AddStep("disable menu items", () =>
            {
                foreach (var item in menu.Items)
                    ((OsuMenuItem)item).Enabled.Value = false;
            });

            AddStep("enable menu items", () =>
            {
                foreach (var item in menu.Items)
                    ((OsuMenuItem)item).Enabled.Value = true;
            });

            AddStep("move to first menu item", () => InputManager.MoveMouseTo(menu.ChildrenOfType<DrawableOsuMenuItem>().First()));
            AddStep("click", () => InputManager.Click(MouseButton.Left));

            AddAssert("action performed", () => actionPeformed);
        }

        private void performAction() => actionPeformed = true;
    }
}
