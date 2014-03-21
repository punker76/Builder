using System;
using System.Collections.Generic;
using System.IO;

namespace Promob.Builder.Translation
{
    public class TranslationManager
    {
        #region Singleton

        private TranslationManager()
        {

        }

        private static TranslationManager _instance;

        public static TranslationManager GetManager()
        {
            if (TranslationManager._instance == null)
                TranslationManager._instance = new TranslationManager();

            return TranslationManager._instance;
        }

        #endregion

        #region Attributes and Properties

        public static readonly Dictionary<int, string> LanguageCodes = new Dictionary<int, string>() 
        {
            {1033, "English - United States" },
            //{1034, "Spanish - Traditional" },
            {1046, "Portuguese - Brazil" }
        };

        private Dictionary<int, Dictionary<string, string>> _translations;
        public Dictionary<int, Dictionary<string, string>> Translations
        {
            get
            {
                if (this._translations == null)
                    this._translations = new Dictionary<int, Dictionary<string, string>>();

                return _translations;
            }
        }

        private int _currentLanguage;
        public int CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                _currentLanguage = value;
                this.NotifyCurrentLanguageChanged();
            }
        }

        #endregion

        #region Events

        public event EventHandler OnCurrentLanguageChanged;

        #endregion

        #region Private Methods

        private void NotifyCurrentLanguageChanged()
        {
            if (OnCurrentLanguageChanged != null)
                OnCurrentLanguageChanged(this, new EventArgs());
        }

        #endregion

        #region Public Methods

        public void Initialize(string path, int currentLanguage)
        {
            this._currentLanguage = currentLanguage;

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            foreach (FileInfo fileInfo in dirInfo.GetFiles("*.translations"))
            {
                int lcid;

                if (!Int32.TryParse(Path.GetFileNameWithoutExtension(fileInfo.Name), out lcid) ||
                    !TranslationManager.LanguageCodes.ContainsKey(lcid))
                    continue;

                using (StreamReader reader = new StreamReader(fileInfo.FullName))
                {
                    Dictionary<string, string> translations = new Dictionary<string, string>();

                    while (reader.Peek() > -1)
                    {
                        string line = reader.ReadLine();

                        if (line.StartsWith("//") || line.Trim().Equals(""))
                            continue;

                        int firstSharp = line.IndexOf('#');

                        if (firstSharp == -1)
                            continue;

                        string key = line.Substring(0, firstSharp);
                        string translation = line.Substring(firstSharp + 1);

                        if (!translations.ContainsKey(key))
                            translations.Add(key, translation);
                    }

                    this.Translations.Add(lcid, translations);
                }
            }
        }

        public string Translate(string key, params string[] values)
        {
            string translation = this.Translate(key);
            string[] translated = new string[values.Length];

            for (int i = 0; i < translated.Length; i++)
                translated[i] = this.Translate(values[i]);

            return string.Format(translation, translated);
        }

        public string Translate(string key)
        {
            if (!this.Translations.ContainsKey(this.CurrentLanguage))
                return key;

            Dictionary<string, string> translation = this.Translations[this.CurrentLanguage];

            if (translation.ContainsKey(key))
            {
                string translated = translation[key];
                translated = translated.Replace("\\r\\n", "\r\n");
                return translated;
            }

            return key;
        }

        #endregion
    }
}
