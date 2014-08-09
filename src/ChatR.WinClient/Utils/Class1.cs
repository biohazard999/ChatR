using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace ChatR.WinClient.Utils
{
    public class MefServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private readonly CompositionContainer compositionContainer;

        public MefServiceLocatorAdapter(CompositionContainer compositionContainer)
        {
            this.compositionContainer = compositionContainer;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            var list = new List<object>();

            var enumerable = compositionContainer.GetExports(serviceType, null, (string)null);

            list.AddRange(from export in enumerable select export.Value);

            return list;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            var source = this.compositionContainer.GetExports(serviceType, null, key);
            if ((source == null) || (!source.Any()))
            {
                throw new ActivationException(this.FormatActivationExceptionMessage((Exception)new CompositionException("Export not found"), (Type)serviceType, key));
            }
            return source.Single<Lazy<object, object>>().Value;
        }
    }
}