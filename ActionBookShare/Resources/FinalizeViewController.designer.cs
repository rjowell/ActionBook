// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ActionBookShare
{
    [Register ("FinalizeViewController")]
    partial class FinalizeViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton cancelButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel chooseContentLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel choosePageLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton doButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel finalizePostLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton giveButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel headlineLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton learnButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView pagePicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView selectedImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton submitButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (cancelButton != null) {
                cancelButton.Dispose ();
                cancelButton = null;
            }

            if (chooseContentLabel != null) {
                chooseContentLabel.Dispose ();
                chooseContentLabel = null;
            }

            if (choosePageLabel != null) {
                choosePageLabel.Dispose ();
                choosePageLabel = null;
            }

            if (doButton != null) {
                doButton.Dispose ();
                doButton = null;
            }

            if (finalizePostLabel != null) {
                finalizePostLabel.Dispose ();
                finalizePostLabel = null;
            }

            if (giveButton != null) {
                giveButton.Dispose ();
                giveButton = null;
            }

            if (headlineLabel != null) {
                headlineLabel.Dispose ();
                headlineLabel = null;
            }

            if (learnButton != null) {
                learnButton.Dispose ();
                learnButton = null;
            }

            if (pagePicker != null) {
                pagePicker.Dispose ();
                pagePicker = null;
            }

            if (selectedImage != null) {
                selectedImage.Dispose ();
                selectedImage = null;
            }

            if (submitButton != null) {
                submitButton.Dispose ();
                submitButton = null;
            }
        }
    }
}