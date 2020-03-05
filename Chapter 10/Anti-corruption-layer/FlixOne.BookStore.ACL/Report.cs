using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.ACL
{
    public class Report : IReport
    {
        private readonly Common.ICommon _common;
        public Report() => _common = new Common.Common();
        public Report(Common.ICommon common) => _common = common;
        public IEnumerable<Common.ViewModels.ShippingViewModel> Get(Guid productId) => _common.Get(productId);
    }
}
