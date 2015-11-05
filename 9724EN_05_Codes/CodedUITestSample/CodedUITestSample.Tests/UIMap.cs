namespace CodedUITestSample.Tests
{
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Input;
    using System.CodeDom.Compiler;
    using System.Text.RegularExpressions;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;


    public partial class UIMap
    {

        /// <summary>
        /// The record will test Abhishek and madam and show proper message
        /// </summary>
        public void ReverseStringTest()
        {
            #region Variable Declarations
            WpfEdit uITxtInputEdit = this.UIMainWindowWindow.UITxtInputEdit;
            WpfText uIThereverseiskehsihbAText = this.UIMainWindowWindow.UIThereverseiskehsihbAText;
            WpfButton uIReverseButton = this.UIMainWindowWindow.UIReverseButton;
            WpfButton uITryAgainButton = this.UIMainWindowWindow.UITryAgainButton;
            WpfButton uICloseButton = this.UIMainWindowWindow.UIMainWindowTitleBar.UICloseButton;
            #endregion

            // Type 'Abhishek' in 'txtInput' text box
            uITxtInputEdit.Text = this.ReverseStringTestParams.UITxtInputEditText;

            // Click 'Reverse' button
            Mouse.Click(uIReverseButton, new Point(30, 13));

            uITryAgainButton.WaitForControlExist(5000);
            
            //Checks whether the label contains reverse string
            string reverseString = this.ReverseString(this.ReverseStringTestParams.UITxtInputEditText);
            StringAssert.Contains(uIThereverseiskehsihbAText.DisplayText, reverseString);

            // Click 'Try Again' button
            Mouse.Click(uITryAgainButton, new Point(184, 10));

            uITxtInputEdit.WaitForControlPropertyEqual("Text", "");

            // Type 'madam' in 'txtInput' text box
            uITxtInputEdit.Text = this.ReverseStringTestParams.UITxtInputEditText1;

            uIReverseButton.WaitForControlEnabled();

            // Click 'Reverse' button
            Mouse.Click(uIReverseButton, new Point(37, 10));

            //Checks whether the label contains reverse string
            
            reverseString = this.ReverseString(this.ReverseStringTestParams.UITxtInputEditText1);
            bool isPalindrome = this.ReverseStringTestParams.UITxtInputEditText1.Equals(reverseString);

            if (isPalindrome)
                Assert.IsTrue(uITxtInputEdit.Text.Equals(reverseString));

            StringAssert.Contains(uIThereverseiskehsihbAText.DisplayText, "palindrome");

        }

        private string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public virtual ReverseStringTestParams ReverseStringTestParams
        {
            get
            {
                if ((this.mReverseStringTestParams == null))
                {
                    this.mReverseStringTestParams = new ReverseStringTestParams();
                }
                return this.mReverseStringTestParams;
            }
        }

        private ReverseStringTestParams mReverseStringTestParams;
    }
    /// <summary>
    /// Parameters to be passed into 'ReverseStringTest'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "11.0.50727.1")]
    public class ReverseStringTestParams
    {

        #region Fields
        /// <summary>
        /// Type 'Abhishek' in 'txtInput' text box
        /// </summary>
        public string UITxtInputEditText = "Abhishek";

        /// <summary>
        /// Type 'madam' in 'txtInput' text box
        /// </summary>
        public string UITxtInputEditText1 = "madam";
        #endregion
}
}
