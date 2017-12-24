using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.FileProcessing
{
    public class AnnonsLoader
    {
        public ObservableCollection<AnnouncementsModels> AnnouncementsList { get; set; }
        //public void GetFromBd()
        //{
        //    AnnouncementsList = new ObservableCollection<AnnouncementsModels>();
        //    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
        //    {
        //        var annons = bd.AnnouncementsModels.ToList();
        //        foreach (var anon in annons)
        //        {
        //            AnnouncementsList.Add(anon);
        //        }
        //    }
        //}
        public async Task GetFromBdAsync()
        {
            AnnouncementsList = new ObservableCollection<AnnouncementsModels>();
            using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
            {
                var annons = await bd.AnnouncementsModels.ToListAsync();
                foreach (var anon in annons)
                {
                    AnnouncementsList.Add(anon);
                }
            }
        }
        //public async Task<ObservableCollection<AnnouncementsModels>> GetAnnouncementList()
        //{
        //    using (DreamHouseAdminBd bd = new DreamHouseAdminBd())
        //    {
        //        var annons = await bd.AnnouncementsModels.ToListAsync();
        //        return new ObservableCollection<AnnouncementsModels>(annons); 
        //    }
        //}
        public AnnonsLoader()
        {
            //GetFromBd();
        }
        public ObservableCollection<AnnouncementsModels> GetAnnons()
        {
            return AnnouncementsList; 
        }
    }
}
