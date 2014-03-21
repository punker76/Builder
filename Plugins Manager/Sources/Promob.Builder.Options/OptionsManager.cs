using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Promob.Builder.Reflection;

namespace Promob.Builder.Options
{
    public class OptionsManager
    {
        #region Constructor

        private OptionsManager()
        {
        }

        #endregion

        #region Singleton

        private static OptionsManager _instance;

        public static OptionsManager GetManager()
        {
            if (OptionsManager._instance == null)
                OptionsManager._instance = new OptionsManager();

            return OptionsManager._instance;
        }

        #endregion

        #region Attributes and Properties

        private OptionsForm _form;
        public OptionsForm Form
        {
            get { return _form; }
            set { _form = value; }
        }

        Dictionary<string, AbstractOptions> _optionsCollection;
        public Dictionary<string, AbstractOptions> OptionsCollection
        {
            get
            {
                if (this._optionsCollection == null)
                    this._optionsCollection = new Dictionary<string, AbstractOptions>();

                return this._optionsCollection;
            }
            set
            {
                this._optionsCollection = value;
            }
        }

        private static string _path;
        public static string Path
        {
            get
            {
                return _path;
            }
        }

        #endregion

        #region Public Methods

        public void Save()
        {
            foreach (AbstractOptions options in this.OptionsCollection.Values)
                options.Save();
        }

        public void Initialize(string path, Assembly applicationAssembly)
        {
            _path = path;
            this.OptionsCollection.Clear();

            List<Type> types = ReflectionHelper.GetSubClasses(typeof(AbstractOptions), applicationAssembly);

            foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles())
            {
                string optionsName = optionsName = fileInfo.Name.Remove(fileInfo.Name.IndexOf(".options"));

                Type curType = types.Find(new Predicate<Type>(t => t.Name.Equals(optionsName)));

                if (curType == null)
                    continue;

                AbstractOptions options = null;

                try
                {
                    string optionsPath = System.IO.Path.Combine(path, curType.Name + ".options");
                    options = Activator.CreateInstance(curType, optionsPath) as AbstractOptions;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("AbstractOptions options = Activator.CreateInstance(curType) as AbstractOptions;:\n\n" + ex);
                }

                options.Load();

                this.OptionsCollection.Add(curType.Name, options);

                types.Remove(curType);
            }

            foreach (Type type in types)
            {
                this.OptionsCollection.Add(type.Name, Activator.CreateInstance(type) as AbstractOptions);
            }

            this._form = new OptionsForm(applicationAssembly);
        }

        #endregion
    }
}
