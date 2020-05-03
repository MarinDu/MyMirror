// -----------------------------------------------------------------------
// <copyright file="SettingsManager.cs">
//
// </copyright>
// <summary>Contains class SettingsManager</summary>
// -----------------------------------------------------------------------

namespace Common.Settings.Items
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Setting item of type List<string>
    /// </summary>
    public class ListSettingItem : SettingItemBase<string>
    {
        /// </summary>
        /// Gets or set settings possible value
        /// </summary>
        public List<string> PossibleValues { get; set; }

        /// <inheritdoc />
        public override PamameterValueType DisplayType => PamameterValueType.List;

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
                        StringValue = PossibleValues[value];
                    }
                }
            }
        }

        /// <inheritdoc />
        public override string StringValue
        {
            get => Value;
            set
            {
                Value = value;
                NotifyPropertyChanged(nameof(Value));
            }
        }

        /// <inheritdoc />
        public override void InitializeFields(Type Resources)
        {

        }
    }
}
