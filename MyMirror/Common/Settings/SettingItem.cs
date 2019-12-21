// -----------------------------------------------------------------------
// <copyright file="SettingsManager.cs">
//
// </copyright>
// <summary>Contains class SettingsManager</summary>
// -----------------------------------------------------------------------

namespace Common.Settings
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Contains settings item
    /// </summary>
    public class SettingItem<T>
    {
        /// <summary>
        /// Gets or set settings name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or set settings displayed name
        /// </summary>
        public string Translation { get; set; }

        /// <summary>
        /// Gets or set settings value
        /// </summary>
        [XmlIgnoreAttribute]
        public T Value { get; set; }

        /// <summary>
        /// Gets or set settings possible value
        /// </summary>
        public List<T> PossibleValues { get; set; }

        /// <summary>
        /// Gets or sets value index
        /// </summary>
        public int ValueIndex
        {
            get => PossibleValues?.IndexOf(Value) ?? -1;
            set
            {
                if(PossibleValues != null)
                {
                    if (value > -1 && value < PossibleValues.Count)
                    {
                        Value = PossibleValues[value];
                    }
                }
            }
        }

        /// <summary>
        /// Gets string version of the setting item
        /// </summary>
        /// <returns></returns>
        public SettingItem<string> SettingToString()
        {
            SettingItem<string> ret = new SettingItem<string>
            {
                Name = Name,
                Translation = Name,
                Value = Value?.ToString(),
                PossibleValues = new List<string>()
            };

            if(PossibleValues != null)
            {
                foreach (T val in PossibleValues)
                {
                    ret.PossibleValues.Add(val?.ToString());
                }
            }
            return ret;
        }
    }
}
