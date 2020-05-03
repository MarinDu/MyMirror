// -----------------------------------------------------------------------
// <copyright file="StringSettingItem.cs">
//
// </copyright>
// <summary>Contains class StringSettingItem</summary>
// -----------------------------------------------------------------------

using System;

namespace Common.Settings.Items
{
    /// <summary>
    /// Setting item of time String
    /// </summary>
    public class StringSettingItem : SettingItemBase<string>
    {
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
        public override PamameterValueType DisplayType => PamameterValueType.Field;

        /// <inheritdoc />
        public override void InitializeFields(Type Resources)
        {
        }
    }
}
