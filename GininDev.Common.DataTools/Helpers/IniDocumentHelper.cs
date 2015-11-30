﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GininDev.Common.DataTools.Helpers
{
    internal class IniDocumentHelper
    {
        private static readonly Regex RegRemoveEmptyLines =
            new Regex
                (
                @"(\s*;[\d\D]*?\r?\n)+|\r?\n(\s*\r?\n)*",
                RegexOptions.Multiline | RegexOptions.Compiled
                );

        private static readonly Regex RegParseIniData =
            new Regex
                (
                @"
                (?<IsSection>
                    ^\s*\[(?<SectionName>[^\]]+)?\]\s*$
                )
                |
                (?<IsKeyValue>
                    ^\s*(?<Key>[^(\s*\=\s*)]+)?\s*\=\s*(?<Value>[\d\D]*)$
                )",
                RegexOptions.Compiled |
                RegexOptions.IgnoreCase |
                RegexOptions.IgnorePatternWhitespace
                );

        private readonly Dictionary<string, NameValueCollection> _data =
            new Dictionary<string, NameValueCollection>();

        public IniDocumentHelper()
        {
            ReadIniData(null, null);
        }

        public IniDocumentHelper(string fileName)
            : this(fileName, Encoding.UTF8)
        {
        }

        public IniDocumentHelper(string fileName, Encoding encoding)
        {
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                ReadIniData(fs, encoding);
            }
        }

        public IniDocumentHelper(Stream stream)
            : this(stream, Encoding.UTF8)
        {
        }

        public IniDocumentHelper(Stream stream, Encoding encoding)
        {
            if (stream == null || stream == Stream.Null)
                throw new ArgumentNullException("stream");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            ReadIniData(stream, encoding);
        }

        public NameValueCollection this[string section]
        {
            get
            {
                section = section.ToLowerInvariant();
                if (!_data.ContainsKey(section))
                    _data.Add(section, new NameValueCollection());
                return _data[section];
            }
        }

        public string this[string section, string key]
        {
            get { return this[section][key]; }
            set { this[section][key] = value; }
        }

        public object this[string section, string key, Type t]
        {
            get
            {
                if (t == null || t == Type.Missing)
                    return this[section][key];
                return GetValue(section, key, null, t);
            }
            set
            {
                if (t == null || t == Type.Missing)
                    this[section][key] = String.Empty;
                else
                    SetValue(section, key, value);
            }
        }

        public string[] SectionNames
        {
            get
            {
                var all = new string[_data.Count];
                _data.Keys.CopyTo(all, 0);
                return all;
            }
        }

        private void ReadIniData(Stream stream, Encoding encoding)
        {
            string lastSection = string.Empty;
            _data.Add(lastSection, new NameValueCollection());
            if (stream == null || encoding == null) return;
            string iniData;
            using
                (
                var reader =
                    new StreamReader(stream, encoding)
                )
                iniData = reader.ReadToEnd();

            iniData = RegRemoveEmptyLines.Replace(iniData, "\n");

            string[] lines =
                iniData.Split
                    (
                        new[] {'\n'},
                        StringSplitOptions.RemoveEmptyEntries
                    );

            foreach (Match m in lines.Select(s => RegParseIniData.Match(s)).Where(m => m.Success))
            {
                if (m.Groups["IsSection"].Length > 0)
                {
                    string sName =
                        m.Groups["SectionName"].Value.
                            ToLowerInvariant();

                    if (lastSection == sName) continue;
                    lastSection = sName;
                    if (!_data.ContainsKey(sName))
                    {
                        _data.Add
                            (
                                sName,
                                new NameValueCollection()
                            );
                    }
                }
                else if (m.Groups["IsKeyValue"].Length > 0)
                {
                    _data[lastSection].Add
                        (
                            m.Groups["Key"].Value,
                            m.Groups["Value"].Value
                        );
                }
            }
        }

        public string[] KeyNames(string section)
        {
            return this[section].AllKeys;
        }

        public string[] SectionValues(string section)
        {
            return this[section].GetValues(0);
        }

        public object GetValue(string section, string key, object defaultValue, Type t)
        {
            if (!_data.ContainsKey(section))
                return defaultValue;
            string v = _data[section][key];
            if (string.IsNullOrEmpty(v))
                return defaultValue;
            TypeConverter conv = TypeDescriptor.GetConverter(t);
            if (conv == null)
                return defaultValue;
            if (!conv.CanConvertFrom(typeof (string)))
                return defaultValue;
            try
            {
                return conv.ConvertFrom(v);
            }
            catch
            {
                return defaultValue;
            }
        }

        public T GetValue<T>(string section, string key, T defaultValue)
        {
            return (T) GetValue(section, key, defaultValue, typeof (T));
        }

        public T GetValue<T>(string section, string key)
        {
            return GetValue(section, key, default(T));
        }

        public Boolean GetBoolean(string section, string key, Boolean defaultValue)
        {
            return GetValue<Boolean>(section, key);
        }

        public Boolean GetBoolean(string section, string key)
        {
            return GetBoolean(section, key, default(Boolean));
        }

        public Byte GetByte(string section, string key, Byte defaultValue)
        {
            return GetValue<Byte>(section, key);
        }

        public Byte GetByte(string section, string key)
        {
            return GetByte(section, key, default(Byte));
        }

        public SByte GetSByte(string section, string key, SByte defaultValue)
        {
            return GetValue<SByte>(section, key);
        }

        public SByte GetSByte(string section, string key)
        {
            return GetSByte(section, key, default(SByte));
        }

        public Int16 GetInt16(string section, string key, Int16 defaultValue)
        {
            return GetValue<Int16>(section, key);
        }

        public Int16 GetInt16(string section, string key)
        {
            return GetInt16(section, key, default(Int16));
        }

        public UInt16 GetUInt16(string section, string key, UInt16 defaultValue)
        {
            return GetValue<UInt16>(section, key);
        }

        public UInt16 GetUInt16(string section, string key)
        {
            return GetUInt16(section, key, default(UInt16));
        }

        public Int32 GetInt32(string section, string key, Int32 defaultValue)
        {
            return GetValue<Int32>(section, key);
        }

        public Int32 GetInt32(string section, string key)
        {
            return GetInt32(section, key, default(Int32));
        }

        public UInt32 GetUInt32(string section, string key, UInt32 defaultValue)
        {
            return GetValue<UInt32>(section, key);
        }

        public UInt32 GetUInt32(string section, string key)
        {
            return GetUInt32(section, key, default(UInt32));
        }

        public Int64 GetInt64(string section, string key, Int64 defaultValue)
        {
            return GetValue<Int64>(section, key);
        }

        public Int64 GetInt64(string section, string key)
        {
            return GetInt64(section, key, default(Int64));
        }

        public UInt64 GetUInt64(string section, string key, UInt64 defaultValue)
        {
            return GetValue<UInt64>(section, key);
        }

        public UInt64 GetUInt64(string section, string key)
        {
            return GetUInt64(section, key, default(UInt64));
        }

        public Single GetSingle(string section, string key, Single defaultValue)
        {
            return GetValue<Single>(section, key);
        }

        public Single GetSingle(string section, string key)
        {
            return GetSingle(section, key, default(Single));
        }

        public Double GetDouble(string section, string key, Double defaultValue)
        {
            return GetValue<Double>(section, key);
        }

        public Double GetDouble(string section, string key)
        {
            return GetDouble(section, key, default(Double));
        }

        public Decimal GetDecimal(string section, string key, Decimal defaultValue)
        {
            return GetValue<Decimal>(section, key);
        }

        public Decimal GetDecimal(string section, string key)
        {
            return GetDecimal(section, key, default(Decimal));
        }

        public DateTime GetDateTime(string section, string key, DateTime defaultValue)
        {
            return GetValue<DateTime>(section, key);
        }

        public DateTime GetDateTime(string section, string key)
        {
            return GetDateTime(section, key, default(DateTime));
        }

        public void SetValue(string section, string key, object value)
        {
            if (value == null)
            {
                this[section][key] = String.Empty;
            }
            else
            {
                TypeConverter conv = TypeDescriptor.GetConverter(value);
                if (conv == null || !conv.CanConvertTo(typeof (string)))
                {
                    this[section][key] = value.ToString();
                }
                else
                {
                    this[section][key] = (string) conv.ConvertTo(value, typeof (string));
                }
            }
        }

        public void SetValue(string section, string key, Boolean value)
        {
            SetValueToString(section, key, value);
        }

        public void SetValue(string section, string key, Byte value)
        {
            SetValueToString(section, key, value);
        }

        public void SetValue(string section, string key, SByte value)
        {
            SetValueToString(section, key, value);
        }

        public void SetValue(string section, string key, Int16 value)
        {
            SetValueToString(section, key, value);
        }

        public void SetValue(string section, string key, Int32 value)
        {
            SetValueToString(section, key, value);
        }

        public void SetValue(string section, string key, Int64 value)
        {
            SetValueToString(section, key, value);
        }

        public void SetValue(string section, string key, UInt16 value)
        {
            SetValueToString(section, key, value);
        }

        public void SetValue(string section, string key, UInt32 value)
        {
            SetValueToString(section, key, value);
        }

        public void SetValue(string section, string key, UInt64 value)
        {
            SetValueToString(section, key, value);
        }

        public void SetValue(string section, string key, Single value)
        {
            SetValueToString(section, key, value);
        }

        public void SetValue(string section, string key, Double value)
        {
            SetValueToString(section, key, value);
        }

        public void SetValue(string section, string key, Decimal value)
        {
            SetValueToString(section, key, value);
        }

        public void SetValue(string section, string key, DateTime value)
        {
            SetValueToString(section, key, value);
        }

        public bool HasSection(string section)
        {
            return _data.ContainsKey(section.ToLowerInvariant());
        }

        public bool HasKey(string section, string key)
        {
            return
                _data.ContainsKey(section) &&
                !string.IsNullOrEmpty(_data[section][key]);
        }

        public void Save(string fileName)
        {
            Save(fileName, Encoding.UTF8);
        }

        public void Save(string fileName, Encoding encoding)
        {
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                Save(fs, encoding);
            }
        }

        public void Save(Stream stream)
        {
            Save(stream, Encoding.UTF8);
        }

        public void Save(Stream stream, Encoding encoding)
        {
            if (stream == null || stream == Stream.Null)
                throw new ArgumentNullException("stream");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            using (var sw = new StreamWriter(stream, encoding))
            {
                Dictionary<string, NameValueCollection>.Enumerator en =
                    _data.GetEnumerator();
                while (en.MoveNext())
                {
                    KeyValuePair<string, NameValueCollection> cur =
                        en.Current;
                    if (!string.IsNullOrEmpty(cur.Key))
                    {
                        sw.WriteLine("[{0}]", cur.Key);
                    }
                    NameValueCollection col = cur.Value;
                    foreach (string key in col.Keys)
                    {
                        if (string.IsNullOrEmpty(key)) continue;
                        string value = col[key];
                        if (!string.IsNullOrEmpty(value))
                            sw.WriteLine("{0}={1}", key, value);
                    }
                }
                sw.Flush();
            }
        }

        private void SetValueToString(string section, string key, object value)
        {
            this[section][key] = value.ToString();
        }
    }
}