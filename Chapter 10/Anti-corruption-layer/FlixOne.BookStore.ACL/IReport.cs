using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.ACL
{
    public interface IReport
    {
        public IEnumerable<Common.ViewModels.ShippingViewModel> Get(Guid productId);

    }
}
