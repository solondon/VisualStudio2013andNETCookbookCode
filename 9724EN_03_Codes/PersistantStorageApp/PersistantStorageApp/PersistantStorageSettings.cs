using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistantStorageApp
{
    public class PersistantStorageSettings
    {
        IsolatedStorageSettings settings;

        public PersistantStorageSettings()
        {
            settings = IsolatedStorageSettings.ApplicationSettings;
        }

        public bool AddOrUpdateValue(string key, object value)
        {
            bool valueChanged = false;
            if (settings.Contains(key))
            {
                if (settings[key] != value)
                {
                    settings[key] = value;
                    valueChanged = true;
                }
            }
            else
            {
                settings.Add(key, value);
                valueChanged = true;
            }
            return valueChanged;
        }
        public T GetValueOrDefault<T>(string key)
        {
            T value;
            if (settings.Contains(key))
                value = (T)settings[key];
            else
                value = default(T);
            return value;
        }
        public void Save()
        {
            settings.Save();
        }
        
    }

    public class MySetttings : PersistantStorageSettings
    {
        public string Settings1
        {
            get
            {
                return base.GetValueOrDefault<string>("settings1");
            }
            set
            {
                if (base.AddOrUpdateValue("settings1", value))
                    base.Save();
            }
        }
    }
}
