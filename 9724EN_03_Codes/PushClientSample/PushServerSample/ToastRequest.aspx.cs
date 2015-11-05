using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PushServerSample
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblSubscription.Text = "http://sn1.notify.live.net/throttledthirdparty/01.00/AAEehP3nirB1RI8a1m4Uji74AGAAAADAQAAAQUZm52IjLzOECFDRcjfx3fs";
        }

        protected void ButtonSendToast_Click(object sender, EventArgs e)
        {
            try
            {
                string subscriptionUri = this.lblSubscription.Text.ToString();
                string toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                    "<wp:Notification xmlns:wp=\"WPNotification\">" +
                                       "<wp:Toast>" +
                                            "<wp:Text1>" + this.txtTitle.Text.ToString() + "</wp:Text1>" +
                                            "<wp:Text2>" + this.txtMessage.Text.ToString() + "</wp:Text2>" +
                                            "<wp:Param>/Page2.xaml?NavigatedFrom=Toast Notification</wp:Param>" +
                                       "</wp:Toast> " +
                                    "</wp:Notification>";
                PushNotifier.SendPushNotification(subscriptionUri, toastMessage);
            }
            catch 
            {
            }

        }
    }
}