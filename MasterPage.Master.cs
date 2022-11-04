
using System;
using System.IO;
using System.Web;
using System.Web.UI;

namespace erpSoftTickets.Secured
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public string strCon = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["InstituteID"] == null)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.User = null;
                System.Web.Security.FormsAuthentication.SignOut();
                Response.Redirect("~/default.aspx", true);
                return;
            }

           
            if (!IsPostBack)
            {
            }

        }

        public string DisplayTallyConfigLink()
        {
            string strLink = "";
            if (Session["InstituteID"].ToString() == "2")
                strLink = "<li><a class='dropdown' href='/Secured/menuFile/TallyConfig.aspx'>Tally Configuration</a></li>";
            else
                if (Session["InstituteID"].ToString() == "11")
                {
                    strLink += "<li><a class='dropdown' href='/Secured/menuFile/AccountConfig.aspx'>Account Configuration</a></li>";
                    strLink += "<li><a class='dropdown' href='/Secured/menuFile/PassLedgerEntries.aspx'>Pass Ledger Entries</a></li>";
                    strLink += "<li><a class='dropdown' href='/Secured/menuFile/PassReceiptEntriesCOL.aspx'>Pass Receipt Entries</a></li>";
                    strLink += "<li><a class='dropdown' href='/Secured/menuFile/PassJVEntriesCOL.aspx'>Pass JV Entries</a></li>";
                }
                else
                {
                    strLink += "<li><a class='dropdown' href='/Secured/menuFile/AccountConfig.aspx'>Account Configuration</a></li>";
                    strLink += "<li><a class='dropdown' href='/Secured/menuFile/PassLedgerEntries.aspx'>Pass Ledger Entries</a></li>";
                    strLink += "<li><a class='dropdown' href='/Secured/menuFile/PassReceiptEntries.aspx'>Pass Receipt Entries</a></li>";
                }
            return strLink;
        }
        protected void lnkbtnLogout_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.User = null;
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("~/default.aspx", true);
        }
        protected void lnkTallyConfig_Click(object sender, EventArgs e)
        {

        }
    }
}