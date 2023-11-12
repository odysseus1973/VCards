﻿using FolkerKinzel.VCards.Enums;
using FolkerKinzel.VCards.Extensions;
using FolkerKinzel.VCards.Intls.Extensions;
using FolkerKinzel.VCards.Models;
using FolkerKinzel.VCards.Models.PropertyParts;

namespace FolkerKinzel.VCards.Syncs;

/// <summary>
/// Provides methods that perform data synchronization operations on
/// the <see cref="VCard"/> instance.
/// </summary>
public sealed class SyncOperation
{
    private readonly VCard _vCard;

    internal SyncOperation(VCard vCard)
    {
        _vCard = vCard;
        RegisterAppInVCardInstance(VCard.App);
    }

    /// <summary>
    /// Gets the identifier of the executing application
    /// that is currently used within the <see cref="VCard"/>
    /// instance.
    /// </summary>
    /// <remarks>
    /// The value of this property may change when calling
    /// the methods of the <see cref="SyncOperation"/> object.
    /// </remarks>
    /// <seealso cref="AppID"/>
    public AppID? CurrentAppID { get; private set; }

    /// <summary>
    /// Sets the <see cref="PropertyID"/>s to all <see cref="VCardProperty"/> objects, which
    /// can have more than one instance inside the <see cref="VCard"/>, depending on the 
    /// value of <see cref="CurrentAppID"/>.
    /// </summary>
    /// <remarks>
    /// <note type="important">
    /// Don't forget to call <see cref="RegisterAppInInstance(Uri)"/> before calling this
    /// method.
    /// </note>
    /// <para>
    /// <see cref="PropertyID"/>s (stored in <see cref="ParameterSection.PropertyIDs"/>)
    /// enable the global data synchronization mechanism introduced with vCard&#160;4.0.
    /// The method can be called several times.
    /// </para>
    /// </remarks>
    public void SetPropertyIDs()
    {
        bool any = false;

        foreach (IEnumerable<VCardProperty?> coll in _vCard.AsProperties()
            .Where(x => x.Key != Prop.VCardClients && x.Value is IEnumerable<VCardProperty?>)
            .Select(x => x.Value)
            .Cast<IEnumerable<VCardProperty?>>())
        {
            foreach (VCardProperty? prop in coll)
            {
                if (prop != null)
                {
                    any = true;
                    SetPropertyID(prop.Parameters, coll);
                }
            }
        }

        if(any && CurrentAppID != null)
        {
            if (!(_vCard.AppIDs?.Any(x => object.ReferenceEquals(x.Value, CurrentAppID)) ?? false)) 
            {
                var newAppIDProp = new AppIDProperty(CurrentAppID);
                _vCard.AppIDs = _vCard.AppIDs?.Concat(newAppIDProp) ?? newAppIDProp;
            }
        }
    }

    /// <summary>
    /// Removes all <see cref="PropertyID"/> and <see cref="AppIDProperty"/> objects
    /// from the <see cref="VCard"/> instance.
    /// </summary>
    /// <remarks>
    /// <note type="caution">
    /// This method disables the data synchronization mechanism. Call this
    /// method only when problems occur.
    /// </note>
    /// </remarks>
    public void Reset()
    {
        _vCard.AppIDs = null;
        RegisterAppInVCardInstance(VCard.App);

        foreach (IEnumerable<VCardProperty?> coll in _vCard.AsProperties()
            .Where(x => x.Value is IEnumerable<VCardProperty?>)
            .Select(x => x.Value)
            .Cast<IEnumerable<VCardProperty?>>())
        {
            foreach (VCardProperty? prop in coll)
            {
                if (prop != null)
                {
                    prop.Parameters.PropertyIDs = null;
                }
            }
        }
    }

    /// <summary>
    /// Performs the "Global Context Simplification" as described in
    /// RFC&#160;6350, 7.2.5.
    /// </summary>
    /// <remarks>
    /// This method can be called after the data synchronization has 
    /// completed. It removes the <see cref="AppIDProperty"/> objects
    /// that reference foreign vCard clients and all the 
    /// <see cref="PropertyID"/>s set by these clients from the <see cref="VCard"/>
    /// instance and replaces them by own <see cref="PropertyID"/>s.
    /// </remarks>
    public void Simplify()
    {
        foreach (IEnumerable<VCardProperty?> coll in _vCard.AsProperties()
            .Where(x => x.Value is IEnumerable<VCardProperty?>)
            .Select(x => x.Value)
            .Cast<IEnumerable<VCardProperty?>>())
        {
            int? localAppID = CurrentAppID?.LocalID;
            bool resetAppIDs = localAppID.HasValue && localAppID.Value != 1;

            _vCard.AppIDs = null;
            RegisterAppInVCardInstance(VCard.App);

            foreach (VCardProperty? prop in coll)
            {
                if (prop != null)
                {
                    var arr =
                        prop.Parameters.PropertyIDs?
                        .Where(x => x.App == localAppID).ToArray();

                    if (arr != null && resetAppIDs)
                    {
                        for (int i = 0; i < arr.Length; i++)
                        {
                            arr[i] = new PropertyID(arr[i].ID, CurrentAppID);
                        }
                    }
                }
            }

            SetPropertyIDs();
        }
    }

    
    private void RegisterAppInVCardInstance(string? globalID)
    {
        if (globalID is null)
        {
            return;
        }

        if (_vCard.AppIDs is null)
        {
            CurrentAppID = new AppID(1, globalID);
            return;
        }

        var resident = _vCard.AppIDs.FirstOrDefault(x => StringComparer.Ordinal.Equals(globalID, x.Value.GlobalID));

        CurrentAppID = resident is null
            ? new AppID
                (
                _vCard.AppIDs.Select(static x => x.Value.LocalID).Append(0).Max() + 1,
                globalID
                )
            : resident.Value;
    }

    private void SetPropertyID(ParameterSection parameters, IEnumerable<VCardProperty?> props)
    {
        var propIDs = parameters.PropertyIDs ?? Enumerable.Empty<PropertyID>();
        int? appLocalID = CurrentAppID?.LocalID;

        if (propIDs.Any(x => x.App == appLocalID))
        {
            return;
        }

        var id = props.WhereNotNull()
            .Select(static x => x.Parameters.PropertyIDs)
            .SelectMany(static x => x ?? Enumerable.Empty<PropertyID>())
            .Where(x => x.App == appLocalID)
            .Select(static x => x.ID)
            .Append(0)
            .Max() + 1;

        var propID = new PropertyID(id, CurrentAppID);
        parameters.PropertyIDs = propIDs.Concat(propID);
    }
}
