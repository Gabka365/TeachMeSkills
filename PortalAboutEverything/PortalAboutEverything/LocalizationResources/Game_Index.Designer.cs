﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PortalAboutEverything.LocalizationResources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Game_Index {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Game_Index() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PortalAboutEverything.LocalizationResources.Game_Index", typeof(Game_Index).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Create Game.
        /// </summary>
        public static string CreateGame {
            get {
                return ResourceManager.GetString("CreateGame", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Game.
        /// </summary>
        public static string Game {
            get {
                return ResourceManager.GetString("Game", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gamer.
        /// </summary>
        public static string Gamer {
            get {
                return ResourceManager.GetString("Gamer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field &quot;{0}&quot; invalid. Relase date must be more then {1} and less then {2}.
        /// </summary>
        public static string RelaseDate_ValidationErrorMessage {
            get {
                return ResourceManager.GetString("RelaseDate_ValidationErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Release date.
        /// </summary>
        public static string ReleaseDate_LowerCase {
            get {
                return ResourceManager.GetString("ReleaseDate_LowerCase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Just No.
        /// </summary>
        public static string RequiredName_ErrorMessage {
            get {
                return ResourceManager.GetString("RequiredName_ErrorMessage", resourceCulture);
            }
        }
    }
}
