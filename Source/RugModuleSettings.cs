// Example usings.
using Celeste;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using YamlDotNet.Serialization;

namespace Celeste.Mod.Rug
{
    // If no SettingName is applied, it defaults to
    // modoptions_[typename without settings]_title
    // The value is then used to look up the UI text in the dialog files.
    // If no dialog text can be found, Everest shows a prettified mod name instead.
    //[SettingName("Partline")]
    public class RugModuleSettings : EverestModuleSettings
    {
        //public bool ColorfulFieldTrip { get; set; } = false;
    }
}