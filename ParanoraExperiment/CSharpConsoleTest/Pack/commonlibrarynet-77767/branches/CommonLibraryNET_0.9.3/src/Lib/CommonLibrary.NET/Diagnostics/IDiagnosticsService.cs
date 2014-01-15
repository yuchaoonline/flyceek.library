using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace ComLib.Diagnostics
{
    public interface IDiagnosticsService
    {
        void FilterOn(List<string> groupNames, bool include);
        void FilterOn(string groupNamesDelimited, bool include);
        IDictionary GetData();
        string GetDataTextual();
        ReadOnlyCollection<string> GroupNames { get; }
        ReadOnlyCollection<string> GroupNamesAll { get; }
        void WriteInfo(string filePath);
        void WriteInfo(string commaDelimitedGroups, string path, string referenceMessage);
    }
}
