using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SmartTaggingText
{
    internal class UpperCaseSmartTagAction : ISmartTagAction
    {
        private ITrackingSpan span;
        private string upper;
        private string display;
        private ITextSnapshot snapshot;
        public UpperCaseSmartTagAction(ITrackingSpan span)
        {
            this.span = span;
            snapshot = span.TextBuffer.CurrentSnapshot;
            upper = span.GetText(snapshot).ToUpper();
            display = "Convert to upper case";
        }
        public string DisplayText
        {
            get { return display; }
        }
        public ImageSource Icon
        {
            get { return null; }
        }
        public bool IsEnabled
        {
            get { return true; }
        }

        public ISmartTagSource Source
        {
            get;
            private set;
        }

        public ReadOnlyCollection<SmartTagActionSet> ActionSets
        {
            get { return null; }
        }

        public void Invoke()
        {
            span.TextBuffer.Replace(span.GetSpan(snapshot), upper);
        }
    }
}
