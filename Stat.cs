using System.Collections.Generic;

namespace SurvivalFramework
{
    public class Stat
    {
        public string DisplayName { get; private set; } = "";

        #region MaxValue
        private bool useMaxValue { get; set; }

        public bool UseMaxValue
        {
            get => useMaxValue;
            set
            {
                if (value == useMaxValue) return;
                useMaxValue = value;
                if (value) maxValue = useMinValue && maxValue < minValue ? minValue : maxValue;
                RecalculateValues();
            }
        }
        private float maxValue { get; set; }
        public float MaxValue
        {
            get => maxValue;
            set
            {
                float nv = useMinValue && value < minValue ? minValue : value;
                if (nv != maxValue)
                {
                    maxValue = nv;
                    if (useMaxValue && currentValue > maxValue) currentValue = maxValue;
                    RecalculateValues();
                }
            }
        }
        private float RealMaxValueCache;
        public float RealMaxValue
        {
            get
            {
                return RealMaxValueCache;
            }
        }
        #endregion

        #region MinValue
        private bool useMinValue { get; set; }

        public bool UseMinValue
        {
            get => useMinValue;
            set
            {
                if (value == useMinValue) return;
                useMinValue = value;
                if (value) minValue = useMaxValue && maxValue < minValue ? maxValue : minValue;
                RecalculateValues();
            }
        }
        private float minValue { get; set; }
        public float MinValue
        {
            get => minValue;
            set
            {
                float nv = useMaxValue && value > maxValue ? maxValue : value;
                if (nv != minValue)
                {
                    minValue = nv;
                    if (useMinValue && currentValue < minValue) currentValue = minValue;
                    RecalculateValues();
                }
            }
        }
        private float RealMinValueCache;
        public float RealMinValue
        {
            get
            {
                return RealMinValueCache;
            }
        }
        #endregion

        #region Value
        private float currentValue { get; set; }
        public float Value
        {
            get => currentValue;
            set
            {
                float nv = value > maxValue ? maxValue : value < minValue ? minValue : value;
                if (nv != currentValue)
                {
                    currentValue = nv;
                    RecalculateValues();
                }
            }
        }

        private float RealValueCache;
        public float RealValue
        {
            get
            {
                return RealValueCache;
            }
        }
        #endregion

        #region Modifiers
        private Dictionary<string, ModifierEntry> modifiers { get; set; }

        #region ModifiersValues
        //Modifies the value of the stat by an absolute amount. This modifier is applied first. Example: 100 --(mod 0.9)--> 100.9 # This is equal to +0.9
        private float modifiersVA { get; set; }
        //Modifies the value of the stat by a percentual amount. This modifier is applied second. Example: 100 --(mod 0.9)--> 190 # This is equal to +90%
        private float modifiersVP { get; set; }
        //Modifies the value of the stat by an absolute percentual amount. This modifier is applied third and therefore last. Example: 100 --(mod 0.9)--> 90 # This is equal to 90%
        private float modifiersVAP { get; set; }
        private float modifiersMinA { get; set; }
        private float modifiersMinP { get; set; }
        private float modifiersMinAP { get; set; }
        private float modifiersMaxA { get; set; }
        private float modifiersMaxP { get; set; }
        private float modifiersMaxAP { get; set; }
        #endregion

        public float GetModifiersSum(ModifierType type)
        {
            switch (type)
            {
                case ModifierType.ValueAbsolute:
                    return modifiersVA;
                case ModifierType.ValuePercentual:
                    return modifiersVP;
                case ModifierType.ValueAbsolutePercentual:
                    return modifiersVAP;
                case ModifierType.MinAbsolute:
                    return modifiersMinA;
                case ModifierType.MinPercentual:
                    return modifiersMinP;
                case ModifierType.MinAbsolutePercentual:
                    return modifiersMinAP;
                case ModifierType.MaxAbsolute:
                    return modifiersMaxA;
                case ModifierType.MaxPercentual:
                    return modifiersMaxP;
                case ModifierType.MaxAbsolutePercentual:
                    return modifiersMaxAP;
                default:
                    return 0f;
            }
        }
        public float GetModifier(string name)
        {
            if (modifiers.ContainsKey(name)) return modifiers[name].value;
            return 0f;
        }
        public float SetModifier(string name, ModifierType type, float value)
        {
            if (modifiers.ContainsKey(name))
            {
                if (modifiers[name].value == value) return GetModifiersSum(type);
                modifiers[name].value = value;
                modifiers[name].type = type;
            }
            else  modifiers.Add(name, new ModifierEntry(){value = value, type = type});
            CalculateModifiersSum();
            return GetModifiersSum(type);
        }
        public void ResetModifier(string name)
        {
            if (!modifiers.ContainsKey(name)) return;
            modifiers.Remove(name);
            CalculateModifiersSum();
        }
        private void CalculateModifiersSum()
        {
            float sumVA = 0f;
            float sumVP = 0f;
            float sumVAP = 1f;
            float sumMinA = 0f;
            float sumMinP = 0f;
            float sumMinAP = 1f;
            float sumMaxA = 0f;
            float sumMaxP = 0f;
            float sumMaxAP = 1f;
            foreach (KeyValuePair<string, ModifierEntry> keyValuePair in modifiers)
            {
                float v = keyValuePair.Value.value;
                switch (keyValuePair.Value.type)
                {
                    case ModifierType.ValueAbsolute:
                        sumVA += v;
                        break;
                    case ModifierType.ValuePercentual:
                        sumVP += v;
                        break;
                    case ModifierType.ValueAbsolutePercentual:
                        sumVAP += v;
                        break;
                    case ModifierType.MinAbsolute:
                        sumMinA += v;
                        break;
                    case ModifierType.MinPercentual:
                        sumMinP += v;
                        break;
                    case ModifierType.MinAbsolutePercentual:
                        sumMinAP += v;
                        break;
                    case ModifierType.MaxAbsolute:
                        sumMaxA += v;
                        break;
                    case ModifierType.MaxPercentual:
                        sumMaxP += v;
                        break;
                    case ModifierType.MaxAbsolutePercentual:
                        sumMaxAP += v;
                        break;
                }
            }
            bool _changes = false;
            if (modifiersVA != sumVA) { modifiersVA = sumVA; _changes = true; }
            if (modifiersVP != sumVP) { modifiersVP = sumVP; _changes = true; }
            if (modifiersVAP != sumVAP) { modifiersVAP = sumVAP; _changes = true; }
            if (modifiersMinA != sumMinA) { modifiersMinA = sumMinA; _changes = true; }
            if (modifiersMinP != sumMinP) { modifiersMinP = sumMinP; _changes = true; }
            if (modifiersMinAP != sumMinAP) { modifiersMinAP = sumMinAP; _changes = true; }
            if (modifiersMaxA != sumMaxA) { modifiersMaxA = sumMaxA; _changes = true; }
            if (modifiersMaxP != sumMaxP) { modifiersMaxP = sumMaxP; _changes = true; }
            if (modifiersMaxAP != sumMaxAP) { modifiersMaxAP = sumMaxAP; _changes = true; }
            if (_changes) RecalculateValues();
        }

        private class ModifierEntry
        {
            public ModifierType type;
            public float value;
        }
        public enum ModifierType
        {
            ValueAbsolute,
            ValuePercentual,
            ValueAbsolutePercentual,
            MinAbsolute,
            MinPercentual,
            MinAbsolutePercentual,
            MaxAbsolute,
            MaxPercentual,
            MaxAbsolutePercentual,
        }
        #endregion

        #region Hidden
        private bool hidden { get; set; }
        public bool Hidden { get => hidden; }
        #endregion

        #region Warn
        private bool useWarnMin { get; set; }
        public bool UseWarnMin { get => useWarnMin; }
        private bool useWarnMax { get; set; }
        public bool UseWarnMax { get => useWarnMax; }
        #endregion

        private void RecalculateValues()
        {
            float minC = minValue;
            minC = minC + modifiersMinA;
            minC = minC + (minC * modifiersMinP);
            minC = minC * modifiersMinAP;
            float maxC = maxValue;
            maxC = maxC + modifiersMaxA;
            maxC = maxC + (maxC * modifiersMaxP);
            maxC = maxC * modifiersMaxAP;
            maxC = maxC < minC && useMinValue ? minC : maxC;
            float vC = currentValue;
            vC = vC + modifiersVA;
            vC = vC + (vC * modifiersVP);
            vC = vC * modifiersVAP;
            vC = vC < minC && useMinValue ? minC : vC > maxC && useMaxValue ? maxC : vC;

            RealMinValueCache = minC;
            RealMaxValueCache = maxC;
            RealValueCache = vC;
        }

        internal Stat(StatArgs args)
        {
            DisplayName = args.displayName;
            hidden = args.hidden;
            useMinValue = args.useMinValue;
            minValue = args.minValue;
            useMaxValue = args.useMaxValue;
            maxValue = useMinValue && args.maxValue < minValue ? minValue : args.maxValue;
            currentValue = useMinValue && args.value < minValue ? minValue : useMaxValue && args.value > maxValue ? maxValue : args.value;
            modifiers = new Dictionary<string, ModifierEntry>();
            CalculateModifiersSum();
        }

        public class StatArgs
        {
            public string displayName = "Undefined";
            public bool useMaxValue = true;
            public float maxValue = 100f;
            public bool useMinValue = true;
            public float minValue = 0f;
            public float value = 0f;
            public bool hidden = false;
            // public bool useWarnMin = false;
            // public float warnMin = 0f;
            // public bool useWarnMax = false;
            // public float warnMax = 100f;

            public static implicit operator StatArgs(SaveManager.SaveDataStat data)
            {
                StatArgs _args = new StatArgs()
                {
                    displayName = data.displayName ?? "Undefined",
                    hidden = data.hidden,
                    useMinValue = data.useMin,
                    useMaxValue = data.useMax,
                    minValue = data.min,
                    maxValue = data.max,
                    value = data.value,
                };
                return _args;
            }
        }

        public static implicit operator SaveManager.SaveDataStat(Stat data)
        {
            SaveManager.SaveDataStat _data = new SaveManager.SaveDataStat()
            {
                displayName = data.DisplayName,
                hidden = data.hidden,
                useMin = data.useMinValue,
                useMax = data.useMaxValue,
                min = data.minValue,
                max = data.maxValue,
                value = data.currentValue,
            };
            return _data;
        }
    }
}
