using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AboriginalHeroes.Entities;
using Windows.Storage;
using Windows.Data.Json;

namespace AboriginalHeroes.Data
{
    public sealed class DataSource
    {
        private static DataSource _dataSource = new DataSource();

        private ObservableCollection<DataGroup> _groups = new ObservableCollection<DataGroup>();
        public ObservableCollection<DataGroup> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<DataGroup>> GetGroupsAsync()
        {
            await _dataSource.GetDataAsync();

            return _dataSource.Groups;
        }

        public static async Task<DataGroup> GetGroupAsync(string uniqueId)
        {
            await _dataSource.GetDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _dataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<DataItem> GetItemAsync(string uniqueId)
        {
            await _dataSource.GetDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _dataSource.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }


        private async Task GetDataAsync()
        {
            if (this._groups.Count != 0)
                return;

            var dataService = new DataService();

            DataGroup group1 = await dataService.GetDataGroup1();
            DataGroup group2 = await dataService.GetDataGroup2();
            DataGroup videos = await dataService.GetDataGroupVideos();
            DataGroup group4 = await dataService.GetDataGroup4();
            //DataGroup group5 = await dataService.GetDataGroup5(); dataset is broken

            this.Groups.Add(group1);
            this.Groups.Add(group2);
            Groups.Add(videos);
            this.Groups.Add(group4);
            //this.Groups.Add(group5);
        }

    }
}
