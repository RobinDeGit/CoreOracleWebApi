using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOracleWebApi.Repositories
{
    public interface IPatsInHospitalRepository
    {
        object GetPatsInHospitalList();
        object GetPatsInHospitalDetails(string pat_id, int v_id);
    }
}
