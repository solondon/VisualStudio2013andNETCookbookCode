using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace ServiceExtensibility
{
    class NoNegetiveInvoker : IOperationInvoker
    {
        public NoNegetiveInvoker(IOperationInvoker originalInvoker)
        {
            this.OriginalInvoker = originalInvoker;
        }

        public IOperationInvoker OriginalInvoker { get; set; }

        public object[] AllocateInputs()
        {
            return OriginalInvoker.AllocateInputs();
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            var result=  OriginalInvoker.Invoke(instance, inputs, out outputs);
            result = Math.Abs((double)result);
            return result;
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            return OriginalInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            var oresult= OriginalInvoker.InvokeEnd(instance, out outputs, result);
            oresult = Math.Abs((double)oresult);
            return oresult;
        }

        public bool IsSynchronous
        {
            get { return OriginalInvoker.IsSynchronous; }
        }
    }
}
