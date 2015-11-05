using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;

namespace StatementCompletionAdorner
{
    public class CompletionSource : ICompletionSource
    {
        private CompletionSourceProvider sourceProvider;
        private ITextBuffer txtBuffer;
        private List<Completion> lstCompletion;

        public CompletionSource(CompletionSourceProvider sourceProvider, ITextBuffer textBuffer)
        {
            this.sourceProvider = sourceProvider;
            this.txtBuffer = textBuffer;
        }
        void ICompletionSource.AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
        {
            List<string> elList = new List<string> { ".NET", ".COM", "Microsoft", "PacktPub", "Visual Studio", "Managed Extensibility Framework", 
                "Windows Presentation Foundation", "Packt Publications"};
            lstCompletion = new List<Completion>();
            foreach (string el in elList)
                lstCompletion.Add(new Completion(el, el, el, null, null));

            completionSets.Add(new CompletionSet(
                "Tokens",    
                "Tokens",    
                FindTokenSpanAtPosition(session.GetTriggerPoint(txtBuffer), session),
                lstCompletion,
                null));
        }
        private ITrackingSpan FindTokenSpanAtPosition(ITrackingPoint point, ICompletionSession session)
        {
            SnapshotPoint currentPoint = (session.TextView.Caret.Position.BufferPosition) - 1;
            ITextStructureNavigator navigator = sourceProvider.NavigatorService.GetTextStructureNavigator(txtBuffer);
            TextExtent extent = navigator.GetExtentOfWord(currentPoint);
            return currentPoint.Snapshot.CreateTrackingSpan(extent.Span, SpanTrackingMode.EdgeInclusive);
        }

        private bool isDisposed;
        public void Dispose()
        {
            if (!isDisposed)
            {
                GC.SuppressFinalize(this);
                isDisposed = true;
            }
        }
    }
}
