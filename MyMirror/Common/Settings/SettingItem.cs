// -----------------------------------------------------------------------
// <copyright file="SettingsManager.cs">
//
// </copyright>
// <summary>Contains class SettingsManager</summary>
// -----------------------------------------------------------------------

namespace Common.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Contains settings item
    /// </summary>
    public class SettingItem
    {
        /// <summary>
        /// Gets or set the type of the parameter
        /// </summary>
        public PamameterValueType Type { get; set; }  
        
        /// <summary>
        /// Gets or set settings name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or set settings displayed name
        /// </summary>
        public string Translation { get; set; }

        /// <summary>
        /// Gets or set settings value as string
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or set settings possible value
        /// </summary>
        public List<string> PossibleValues { get; set; }

        /// <summary>
        /// Gets or sets value index
        /// </summary>
        public int ValueIndex
        {
            get => PossibleValues?.IndexOf(Value) ?? -1;
            set
            {
                if (PossibleValues != null)
                {
                    if (value > -1 && value < PossibleValues.Count)
                    {
                        Value = PossibleValues[value];
                    }
                }
            }
        }

        /// <summary>
        /// Returns the value of the setting, in its normal type
        /// </summary>
        public object GetRealValue()
        {
            object ret = null;
            switch (Type)
            {
                case (PamameterValueType.Boolean):
                {
                        ret = Value.ToLower().Equals("true");
                        break;
                }   
                case (PamameterValueType.ListOfInteger):
                {     
                        Int32.TryParse(Value,  out int val);
                        ret = val;
                        break;
                }
                case (PamameterValueType.ListOfString):
                {
                        ret = Value;
                        break;
                }
                case (PamameterValueType.FieldInteger):
                {
                        Int32.TryParse(Value, out int val);
                        ret = val;
                        break;
                }
                case (PamameterValueType.FieldString):
                {
                        ret = Value;
                        break;
                }
                case (PamameterValueType.FieldUrl):
                {
                        ret = Value;
                        break;
                }
            }

            return ret;
        }
    }
}
