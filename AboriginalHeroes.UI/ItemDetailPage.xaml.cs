﻿using AboriginalHeroes.UI.Common;
using AboriginalHeroes.UI.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AboriginalHeroes.Data;
using AboriginalHeroes.Entities;

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace AboriginalHeroes.UI
{
    /// <summary>
    /// A page that displays details for a single item within a group.
    /// </summary>
    public sealed partial class ItemDetailPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public ItemDetailPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="Common.NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {          
            var item = await DataSource.GetItemAsync((String)e.NavigationParameter);

            this.DefaultViewModel["Item"] = item;

            DataItem dataItem = (DataItem)item;
            if (dataItem.GroupType == GroupType.Person) pageTitle.Text = "Indigenous Servicemen Photo: " + item.UniqueId;
         
            if (string.IsNullOrWhiteSpace(item.Description))
                description.Visibility = Visibility.Collapsed;
            if (string.IsNullOrWhiteSpace(item.Content))
                content.Visibility = Visibility.Collapsed;

            ItemVideo.Visibility = dataItem.GroupType == GroupType.Video ? Visibility.Visible : Visibility.Collapsed;
            ItemImage.Visibility = dataItem.GroupType == GroupType.Video ? Visibility.Collapsed : Visibility.Visible;
            SetupMap(dataItem);
        }

        private void SetupMap(DataItem item)
        {
            if (item.GroupType != GroupType.PersonWithMap)
            {
                myMap.Visibility = Visibility.Collapsed;
                return;
            }

            var locationCollection = new Bing.Maps.LocationCollection
            {
                 new Bing.Maps.Location(item.PlaceOfBirthLat,item.PlaceOfBirthLong)
                 , new Bing.Maps.Location(item.PlaceOfDeathLat,item.PlaceOfDeathLong)
            };

            var locationRect = new Bing.Maps.LocationRect(locationCollection);

            var locationRect2 = new Bing.Maps.LocationRect(locationRect.Center,locationRect.Width + locationRect.Width * 0.7, locationRect.Height + locationRect.Height * 0.20);
            
            myMap.SetView(locationRect2, TimeSpan.FromSeconds(3));
            AddMapPushPin(item);
        }

        public void AddMapPushPin(DataItem item)
        {
            var pushpinBorn = new Bing.Maps.Pushpin();
            pushpinBorn.Text = "1";
            pushpinBorn.Background = new SolidColorBrush(Windows.UI.Colors.Blue);
            
            Bing.Maps.MapLayer.SetPosition(pushpinBorn, new Bing.Maps.Location(item.PlaceOfBirthLat, item.PlaceOfBirthLong));
            myMap.Children.Add(pushpinBorn);

            var pushpinDeath = new Bing.Maps.Pushpin();
            pushpinDeath.Text = "2";
            pushpinDeath.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            Bing.Maps.MapLayer.SetPosition(pushpinDeath, new Bing.Maps.Location(item.PlaceOfDeathLat, item.PlaceOfDeathLong));
            myMap.Children.Add(pushpinDeath);
        }
    


        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="Common.NavigationHelper.LoadState"/>
        /// and <see cref="Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void ItemVideo_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            txtVideoError.Visibility = Visibility.Visible;
        }
    }
}