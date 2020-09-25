using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreOracleWebApi.Repositories;

namespace CoreOracleWebApi.Controllers
{
    [Produces("application/json")]
    public class InpatsController : Controller
    {
        IPatsInHospitalRepository inpatRepository;
        public InpatsController(IPatsInHospitalRepository _inpatRepository)
        {
            inpatRepository = _inpatRepository;
        }

        [Route("api/GetInpatsList")]
        public ActionResult GetInpatList() 
        {
            var result = inpatRepository.GetPatsInHospitalList();
            if (result == null) 
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("api/GetSingleInpat/{pat_id}/{v_id}")]
        public ActionResult GetSingleInPat(string pat_id, int v_id)
        {
            var result = inpatRepository.GetPatsInHospitalDetails(pat_id, v_id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
