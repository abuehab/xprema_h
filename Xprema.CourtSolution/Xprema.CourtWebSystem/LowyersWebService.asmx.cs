using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Xprema.CourtWebSystem
{
    /// <summary>
    /// Summary description for LowyersWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LowyersWebService : System.Web.Services.WebService
    {
        static DbCourtDataContext context = new DbCourtDataContext ();

        #region "    All Lawyers Method             "

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]

        public void GetAllLawyers()
        {
            context = new DbCourtDataContext();
            context.DeferredLoadingEnabled = false;
            context.ObjectTrackingEnabled = false;

            var q = context.Lawyers.ToList();
            var s = new JavaScriptSerializer().Serialize(q.ToList());

            Context.Response.Write(s);
        }

        #endregion 

        #region "   Save New Lawyer Method          "
        [WebMethod]
        public int SaveNewLawyer(string LawyerNam , string phone , string address, string describe)
        {
            try
            {
                int status = 0;
                using (DbCourtDataContext context = new DbCourtDataContext())
                {

                    Lawyer obj = new Lawyer() { LawyerName = LawyerNam , Address = address , Describe = describe , Phone = phone  };

                    context.Lawyers .InsertOnSubmit(obj);
                    context.SubmitChanges();
                    status = obj.Id;
                }
                return status;
            }
            catch
            {
                return -1;
            }

        }

        #endregion

        #region "    Update Lawyer Information      "
        [WebMethod]
        public int EditNewLawyer(string LawyerNam, string phone, string address, string describe , int id)
        {
            try
            {
                int status = 0;
                using (DbCourtDataContext context = new DbCourtDataContext())
                {

                    Lawyer obj =  context.Lawyers .Single (c=> c.Id == id);
                         obj.LawyerName = LawyerNam;
                         obj.Address = address;
                         obj. Describe = describe;
                         obj. Phone = phone ;       
         
                  
                    context.SubmitChanges();
                    status = obj.Id;
                }
                return status;
            }
            catch
            {
                return -1;
            }

        }

        #endregion

        #region "   Delete Lawyer Information       "

        [WebMethod]
        public void DeleteLowyer(int id)
        {
            using (DbCourtDataContext context = new DbCourtDataContext())
            {
                Lawyer obj = context.Lawyers.Single(c => c.Id == id);
                context.Lawyers.DeleteOnSubmit(obj);
                context.SubmitChanges();
            }
        }

        #endregion

    }
}
