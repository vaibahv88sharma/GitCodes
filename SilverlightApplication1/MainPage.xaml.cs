using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SharePoint.Client;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private List oList;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ClientContext clientContext = new ClientContext("http://contoso:42812/sites/cslteam/"))
            {
                Web site = clientContext.Web;
                oList = site.Lists.GetByTitle("Announcements1");
                ListItem oListItem = oList.AddItem(new ListItemCreationInformation());
                oListItem["Title"] = "My new item";
                oListItem["Body"] = "This is my new Silverlight item and it is rceated at" + DateTime.Today.ToString();
                oListItem.Update();

                clientContext.Load(oList,list=>list.Title);
                clientContext.ExecuteQueryAsync(onQuerySucceeded, onQueryFailed);
            }
        }
        private void onQuerySucceeded(object sender, ClientRequestSucceededEventArgs args)
        {
            UpdateUIMethod updateUI = DisplayInfo;
            this.Dispatcher.BeginInvoke(updateUI);
        }
        private void DisplayInfo() 
        { 
            TextBox1.Text = "New item created in " + oList.Title; 
        }
        private delegate void UpdateUIMethod();
        private void onQueryFailed(object sender, ClientRequestFailedEventArgs args) 
        { 
            MessageBox.Show("Request failed. " + args.Message + "\n" + args.StackTrace); 
        }
    }
}
