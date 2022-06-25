using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ReactiveUI;

namespace Theseus.Avalonia.ViewModels
{
    [DataContract]
    public class ViewModelBase : ReactiveObject
    {
        protected ViewModelBase()
        {
        }
    }
}