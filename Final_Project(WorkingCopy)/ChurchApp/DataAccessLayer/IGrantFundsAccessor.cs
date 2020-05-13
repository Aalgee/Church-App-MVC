using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// interface for interacting with the GrantFunds Table
    /// </summary>
    public interface IGrantFundsAccessor
    {
        int InsertGrantFunds(GrantFunds grantFunds);
        GrantFunds SelectGrantFunds();
        int UpdateGrantFunds(GrantFunds oldGrantFunds, GrantFunds newGrantFunds);
    }
}
