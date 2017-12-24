using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamHouse.Models;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.Entity;

namespace DreamHouse.DataReader
{
    public class AnnonsCreator
    {
        public ObservableCollection<AnnouncementsModel> AnnouncementsList { get; set; }
        public ObservableCollection<AnnouncementsModel> NewAnnouncementsList { get; set; }

        public ObservableCollection<AnnouncementsModel> GetAnnounsmentList()
        {
    //   GetFromBd();
     //  SerilisationAnnon(); 
        DesirilisationAnnon();
            //AnnouncementsList = new ObservableCollection<AnnouncementsModel>();
            //AnnouncementsList.Add(new AnnouncementsModel("Аксессуары", null, null, null) { Photopath1= "/ImagePathWPF;component/Images/1.jpg" });
            //AnnouncementsList.Add(new AnnouncementsModel("#DreamЕда", null, null, null));
            //AnnouncementsList.Add(new AnnouncementsModel("#DreamДети", null, null, null));
            //AnnouncementsList.Add(new AnnouncementsModel("Осеннее вдохновение от RUBAN", null, null, null));
            //AnnouncementsList.Add(new AnnouncementsModel("#DreamКрасота", null, null, null));
            //AnnouncementsList.Add(new AnnouncementsModel("#Тренд", null, null, null));
            //AnnouncementsList.Add(new AnnouncementsModel("Новая коллекция в Chobi", null, null, null));
            //AnnouncementsList.Add(new AnnouncementsModel("Пляжный костюм", null, null, null));
            return AnnouncementsList; 
        }
        public void GetFromBd()
        {
            AnnouncementsList = new ObservableCollection<AnnouncementsModel>();
            using (DreamHousseBd bd = new DreamHousseBd())
            {
                var annons = bd.Announs.ToList();
                foreach (var anon in annons)
                {
                    AnnouncementsList.Add(anon);
                }
            }
        }
        public async Task GetFromBdAsync()
        {
            AnnouncementsList = new ObservableCollection<AnnouncementsModel>();
            using (DreamHousseBd bd = new DreamHousseBd())
            {
                var annons = await bd.Announs.ToListAsync();
                foreach (var anon in annons)
                {
                    AnnouncementsList.Add(anon);
                }
            }
        }
        public void SerilisationAnnon()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("AnnonBin.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, AnnouncementsList);
            }
        }
        ~AnnonsCreator()
        { }
        public void DesirilisationAnnon()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            AnnouncementsList = new ObservableCollection<AnnouncementsModel>();
            using (FileStream fs = new FileStream("AnnonBin.dat", FileMode.OpenOrCreate))
            {
                AnnouncementsList = (ObservableCollection<AnnouncementsModel>)formatter.Deserialize(fs);
            }
        }
        public AnnonsCreator()
        {
           
        }
    }
}
