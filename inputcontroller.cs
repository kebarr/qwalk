using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Mvc;
using QWalk;
using System.Numerics;



namespace WebApplication1
{
    public class InputController : Controller
    {   // according to http://www.c-sharpcorner.com/UploadFile/ff2f08/actionresult-return-type-in-mvc-3-0/ this should work!
        // this should hopefully do most of the stuff that was done in the forms application- this returns three things potentially.

        public ContentResult ContentResultTest()
        {
            return Content("Hello My Friend!");
        }

        public JsonResult Walk(string left, string right)
        {
            if (inputs.IsValid(left) == false)
            {
                var returnString =
                    "Left coin state value must be of form a, bi, a+bi, sqrt(a), sqrt(b)i or sqrt(a) + sqrt(b)i, default value sqrt(0.5) used for simulation";
                Json(returnString);
                //return null;
            }

            Complex leftInitial = inputs.convert(left);

            if (inputs.IsValid(right) == false)
            {
                var returnString =
                    "Right coin state value must be of form a, bi, a+bi, sqrt(a), sqrt(b)i or sqrt(a) + sqrt(b)i, default value sqrt(0.5) used for simulation";
                return Json(returnString);
                //return null;
            }

            // check initial state is normalised. if not, just reassign to something that is for time being. eventually
            // want to make it so only runs if user puts in valid initial condition, but need it to work for all such conditions first.

            Complex rightInitial = inputs.convert(right);
            if (qWalker.isNormalised(rightInitial, leftInitial) == false)
            {
               // in this case need to notify user aND still run walk.
                rightInitial = new Complex(0, Math.Sqrt(0.5));
                leftInitial = new Complex(0, Math.Sqrt(0.5));
            }
            double[] probabilities = qWalker.runWalk(rightInitial, leftInitial);
            return Json(probabilities);
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}