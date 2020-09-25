using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CoreOracleWebApi.OracleCustom;
using System.Data;
using Dapper;

namespace CoreOracleWebApi.Repositories
{
    public class PatsInHospitalRepository : IPatsInHospitalRepository
    {
        IConfiguration configuration;
        public PatsInHospitalRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public object GetPatsInHospitalDetails(string pat_id, int v_id)
        {
            object result = null;
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("PAT_ID", OracleDbType.Varchar2, ParameterDirection.Input, pat_id);
                dyParam.Add("V_ID", OracleDbType.Int32, ParameterDirection.Input, v_id);
                dyParam.Add("PAT_DETAIL_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                if (conn.State == ConnectionState.Open) 
                {
                    var query = "USP_GETSINGLEPAT";
                    result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }

            return result;
        }

        public object GetPatsInHospitalList() 
        {
            object result = null;
            try
            {
                var dyParam = new OracleDynamicParameters();
                dyParam.Add("INPATSCURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

                var conn = this.GetConnection();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (conn.State == ConnectionState.Open)
                {
                    var query = "USP_GETINPATS";
                    result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }

            return result;
        }

        public IDbConnection GetConnection()
        {
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
            var conn = new OracleConnection(connectionString);
            return conn;
        }

    }
}
