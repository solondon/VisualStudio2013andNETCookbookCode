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
    public class LowerCaseSmartTagAction : ISmartTagAction
    {
         private ITrackingSpan span;
        private string lower;
        private string display;
        private ITextSnapshot tsnapshot;
        public LowerCaseSmartTagAction(ITrackingSpan span)
        {
            this.span = span;
            tsnapshot = span.TextBuffer.CurrentSnapshot;
            lower = span.GetText(tsnapshot).ToLower();
            display = "Convert to lower case";
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
            span.TextBuffer.Replace(span.GetSpan(tsnapshot), lower);
        }
    }
}
