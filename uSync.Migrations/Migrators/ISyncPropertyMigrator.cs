﻿using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Composing;
using uSync.Migrations.Context;
using uSync.Migrations.Migrators.Models;

namespace uSync.Migrations.Migrators;

public interface ISyncPropertyMigrator : IDiscoverable
{
    string[] Editors { get; }

    int[] Versions { get; }

    public string GetEditorAlias(SyncMigrationDataTypeProperty dataTypeProperty, SyncMigrationContext context);

    public string GetDatabaseType(SyncMigrationDataTypeProperty dataTypeProperty, SyncMigrationContext context);

    public object GetConfigValues(SyncMigrationDataTypeProperty dataTypeProperty, SyncMigrationContext context);

    public string GetContentValue(SyncMigrationContentProperty contentProperty, SyncMigrationContext context);
}


/// <summary>
///  interface to impliment if you want to completly replace a datatype with something else ? 
/// </summary>
/// <remarks>
///  if you impliment this then the migrator will not migrate the datatype, but it will
///  use the replacement info suppled to make sure all contenttypes and content use the 
///  replacement datatype.
/// </remarks>
public interface ISyncReplacablePropertyMigrator : ISyncPropertyMigrator
{
    public ReplacementDataTypeInfo? GetReplacementEditorId(SyncMigrationDataTypeProperty dataTypeProperty, SyncMigrationContext context);
}

/// <summary>
///  interface to impliment if your property splits up a single value into varied (e.g cultured) values
/// </summary>

public interface ISyncVariationPropertyMigrator
{
    public Attempt<CulturedPropertyValue> GetVariedElements(SyncMigrationContentProperty contentProperty, SyncMigrationContext context);
}

/// <summary>
/// interface to implemenet if your property splits up a value into separate properties
/// </summary>
public interface ISyncPropertySplittingMigrator
{
    /// <summary>
    /// gets a dictionary of alias/value pairs of new properties to map to
    /// </summary>
    IEnumerable<SplitPropertyContent> GetContentValues(SyncMigrationContentProperty migrationProperty, SyncMigrationContext context);


    IEnumerable<SplitPropertyInfo> GetSplitProperties(string contentTypeAlias, string propertyAlias, string propertyName, SyncMigrationContext context);
}
