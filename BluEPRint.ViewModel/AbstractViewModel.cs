﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BluEPRint.ViewModel
{
    public abstract class AbstractViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// Gets the Value of a Property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        protected T Get<T>([CallerMemberName] string name = default) => _properties.TryGetValue(name, out object value) ? value == null ? default : (T)value : (default);

        /// <summary>
        /// Sets the Value of a Property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="name"></param>
        protected void Set<T>(T value, [CallerMemberName] string name = default)
        {
            OnPropertyChanging(name);
            if (Equals(value, Get<T>(name))) return;
            _properties[name] = value;
            OnPropertyChanged(name);
        }

        /// <summary>
        /// Declares Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Eventhandler
        /// </summary>
        /// <param name="name"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string name = default) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Eventhandler fires before the values has changed
        /// </summary>
        /// <param name="name"></param>
        protected virtual void OnPropertyChanging([CallerMemberName] string name = default) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
    }
}