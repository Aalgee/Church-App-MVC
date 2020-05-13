using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface IGrantFundsManager
    {
        int AddGrantFunds(GrantFunds grantFunds);
        GrantFunds RetrieveGrantFunds();
        bool EditGrantFunds(GrantFunds oldGrantFunds, GrantFunds newGrantFunds);
    }
}
