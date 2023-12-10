using DAL.Data;
using System.Collections.Generic;


namespace FlowMeterTeamProject.DAL.DataServices
{
    public class CounterEntityComparer : IEqualityComparer<Counter>
    {
        public bool Equals(Counter x, Counter y)
        {
            return x != null && y != null &&
                   x.Account == y.Account &&
                   x.TypeOfAccount == y.TypeOfAccount;
        }

        public int GetHashCode(Counter obj)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + obj.Account.GetHashCode();
                hash = hash * 23 + obj.TypeOfAccount.GetHashCode();
                return hash;
            }
        }
    }
}
