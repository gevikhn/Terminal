//----------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionsTab.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>Wrapper and helper for instantiating and interacting with the Options tab of the properties dialog.</summary>
//----------------------------------------------------------------------------------------------------------------------
namespace Conhost.UIA.Tests.Elements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MS.Internal.Mita.Foundation;
    using MS.Internal.Mita.Foundation.Controls;
    using MS.Internal.Mita.Foundation.Waiters;

    using Conhost.UIA.Tests.Common;
    using NativeMethods = Conhost.UIA.Tests.Common.NativeMethods;

    public class OptionsTab : TabBase
    {
        public List<CheckBoxMeta> Checkboxes { get; private set; }
        public CheckBoxMeta GlobalV1V2Box { get; private set; }
        public Hyperlink MoreInfoLink { get; private set; }

        public OptionsTab(PropertiesDialog propDialog) : base(propDialog, " Options ")
        {
        }

        protected override void PopulateItemsOnNavigate(Window propWindow)
        {
            this.GlobalV1V2Box = new CheckBoxMeta(propWindow, "Use legacy console (requires relaunch)", "ForceV2", false, true, false, NativeMethods.WinConP.PKEY_Console_ForceV2);

            this.Checkboxes = new List<CheckBoxMeta>();
            this.Checkboxes.Add(new CheckBoxMeta(propWindow, "Enable line wrapping selection", "LineSelection", false, false, true, NativeMethods.WinConP.PKEY_Console_LineSelection));
            this.Checkboxes.Add(new CheckBoxMeta(propWindow, "Filter clipboard contents on paste", "FilterOnPaste", false, false, true, NativeMethods.WinConP.PKEY_Console_FilterOnPaste));
            this.Checkboxes.Add(new CheckBoxMeta(propWindow, "Enable Ctrl key shortcuts", "CtrlKeyShortcutsDisabled", true, false, true, NativeMethods.WinConP.PKEY_Console_CtrlKeysDisabled));
            this.Checkboxes.Add(new CheckBoxMeta(propWindow, "Extended text selection keys", "ExtendedEditKey", false, true, false, null));

            this.MoreInfoLink = new Hyperlink(propWindow.Descendants.Find(UICondition.CreateFromName("new console features")));
        }

        public override IEnumerable<UIObject> GetObjectsDisabledForV1Console()
        {
            return this.Checkboxes.Select(meta => meta.Box);
        }

        public override IEnumerable<UIObject> GetObjectsUnaffectedByV1V2Switch()
        {
            return new UIObject[] { this.GlobalV1V2Box.Box, this.MoreInfoLink };
        }

        public override IEnumerable<CheckBoxMeta> GetCheckboxesForVerification()
        {
            return this.Checkboxes;
        }

        public override IEnumerable<SliderMeta> GetSlidersForVerification()
        {
            return new SliderMeta[0];
        }
    }
}