using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaggingText
{
    public class SmartTaggerText : ITagger<SmartTagText>
    {
        private ITextBuffer buffer;
        private ITextView view;
        private SmartTaggerProviderText provider;
        private bool m_disposed;
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public SmartTaggerText(ITextBuffer buffer, ITextView view, SmartTaggerProviderText provider)
        {
            this.buffer = buffer;
            this.view = view;
            this.provider = provider;
            view.LayoutChanged += OnLayoutChanged;
        }
        private void OnLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            ITextSnapshot snapshot = e.NewSnapshot;
            //don't do anything if this is just a change in case 
            if (!snapshot.GetText().ToLower().Equals(e.OldSnapshot.GetText().ToLower()))
            {
                SnapshotSpan span = new SnapshotSpan(snapshot, new Span(0, snapshot.Length));
                EventHandler<SnapshotSpanEventArgs> handler = this.TagsChanged;
                if (handler != null)
                {
                    handler(this, new SnapshotSpanEventArgs(span));
                }
            }
        }
        public IEnumerable<ITagSpan<SmartTagText>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            ITextSnapshot snapshot = buffer.CurrentSnapshot;
            if (snapshot.Length == 0)
                yield break; //don't do anything if the buffer is empty 

            //set up the navigator
            ITextStructureNavigator navigator = provider.NavigatorService.GetTextStructureNavigator(buffer);

            foreach (var span in spans)
            {
                ITextCaret caret = view.Caret;
                SnapshotPoint point;

                if (caret.Position.BufferPosition > 0)
                    point = caret.Position.BufferPosition - 1;
                else
                    yield break;

                TextExtent extent = navigator.GetExtentOfWord(point);
                //don't display the tag if the extent has whitespace 
                if (extent.IsSignificant)
                    yield return new TagSpan<SmartTagText>(extent.Span, new SmartTagText(GetSmartTagActions(extent.Span)));
                else yield break;
            }
        }

        private ReadOnlyCollection<SmartTagActionSet> GetSmartTagActions(SnapshotSpan span)
        {
            List<SmartTagActionSet> actionSetList = new List<SmartTagActionSet>();
            List<ISmartTagAction> actionList = new List<ISmartTagAction>();

            ITrackingSpan trackingSpan = span.Snapshot.CreateTrackingSpan(span, SpanTrackingMode.EdgeInclusive);
            actionList.Add(new UpperCaseSmartTagAction(trackingSpan));
            actionList.Add(new LowerCaseSmartTagAction(trackingSpan));
            SmartTagActionSet actionSet = new SmartTagActionSet(actionList.AsReadOnly());
            actionSetList.Add(actionSet);
            return actionSetList.AsReadOnly();
        }
    }
}
