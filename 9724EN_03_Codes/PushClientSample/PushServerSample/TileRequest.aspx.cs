using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PushServerSample
{
    public partial class TileRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Please replace this with your own url.
            this.lblSubscription.Text = "http://sn1.notify.live.net/throttledthirdparty/01.00/AAEehP3nirB1RI8a1m4Uji74AGAAAADAQAAAQUZm52IjLzOECFDRcjfx3fs";
        }

        protected void ButtonSendTile_Click(object sender, EventArgs e)
        {
            try
            {
                string subscriptionUri = this.lblSubscription.Text.ToString();
                string tileMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                   "<wp:Notification xmlns:wp=\"WPNotification\">" +
                       "<wp:Tile>" +
                         "<wp:BackgroundImage>" + txtBackgroundBackImage.Text + "</wp:BackgroundImage>" +
                         "<wp:Count>" + txtBadgeCounter.Text + "</wp:Count>" +
                         "<wp:Title>" + txtTitle.Text + "</wp:Title>" +
                         "<wp:BackBackgroundImage>" + txtBackgroundBackImage.Text + "</wp:BackBackgroundImage>" +
                         "<wp:BackTitle>" + txtBackTitle.Text + "</wp:BackTitle>" +
                         "<wp:BackContent>" + txtBackContent.Text + "</wp:BackContent>" +
                      "</wp:Tile> " +
                   "</wp:Notification>";
                PushNotifier.SendPushNotification(subscriptionUri, tileMessage);
            }
            catch
            {
            }

        }
    }
}