using FirstPhoneApp.Model.Base;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FirstPhoneApp.Model
{
    public class MainDataContext : PropertyBase
    {

        BitmapImage avatarImage;
        public BitmapImage AvatarImage
        {
            get { return this.avatarImage; }
            set { this.avatarImage = value; this.OnPropertyChanged("AvatarImage"); }
        }

        public ICommand ChoosePhoto
        {
            get
            {
                return new RelayCommand((e) =>
                {
                    PhotoChooserTask pTask = new PhotoChooserTask();
                    pTask.Completed += pTask_Completed;
                    pTask.Show();

                });
            }
        }
        void pTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                var bitmap = new BitmapImage();
                bitmap.SetSource(e.ChosenPhoto);
                this.AvatarImage = bitmap;
            }
        }

        List<string> longList;
        public List<string> LongList
        {
            get
            {
                this.longList = this.longList ?? this.LoadList();
                return this.longList;
            }
        }

        private List<string> LoadList()
        {
            List<string> longList = new List<string>();

            foreach (var itemName in this.GetType().GetProperties())
                longList.Add(itemName.Name);
            return longList;
        }
    }
}
